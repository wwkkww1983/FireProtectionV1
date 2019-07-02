using Abp.Domain.Repositories;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Model;
using FireProtectionV1.User.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.FireWorking.Manager
{
    public class DutyManager: IDutyManager
    {
        IRepository<FireUnit> _fireUnitRep;
        IRepository<DataToDuty> _dutyRep;
        IRepository<DataToDutyProblem> _dataToDutyProblemRep;
        IRepository<FireUnitUser> _fireUnitAccountRepository;
        IRepository<PhotosPathSave> _photosPathSave;

        public DutyManager(
            IRepository<FireUnit> fireUnitRep,
            IRepository<DataToDuty> dutyRep,
            IRepository<FireUnitUser> fireUnitAccountRepository,
            IRepository<DataToDutyProblem> dataToDutyProblemRep,
            IRepository<PhotosPathSave> photosPathSaveRep

            )
        {
            _fireUnitRep = fireUnitRep;
            _dutyRep = dutyRep;
            _fireUnitAccountRepository = fireUnitAccountRepository;
            _dataToDutyProblemRep = dataToDutyProblemRep;
            _photosPathSave = photosPathSaveRep;

        }
        public async Task AddNewDuty(AddNewDutyInput input)
        {
            await _dutyRep.InsertAsync(new DataToDuty()
            {
                DutyStatus = input.DutyStatus,
                DutyPicture = input.DutyPicture,
                FireUnitId = input.FireUnitId,
                FireUnitUserId = input.FireUnitUserId
            });
        }
        public IQueryable<DataToDuty> GetDutyDataAll()
        {
            return _dutyRep.GetAll();
        }
        public IQueryable<DataToDuty> GetDutyDataMonth(int year, int month)
        {
            return _dutyRep.GetAll().Where(p => p.CreationTime.Year == year && p.CreationTime.Month == month);
        }
        public IQueryable<DataToDuty> GetDutyDataDuration(DateTime start, DateTime end)
        {
            return _dutyRep.GetAll().Where(p => p.CreationTime >= start && p.CreationTime <= end);
        }
        public IQueryable<FireUnitManualOuput> GetDutyFireUnitsAll(string filterName = null)
        {
            return from a in _fireUnitRep.GetAll().Where(p => string.IsNullOrEmpty(filterName) ? true : p.Name.Contains(filterName))
                   join b in _dutyRep.GetAll().GroupBy(p => p.FireUnitId).Select(p => new
                   {
                       FireUnitId = p.Key,
                       LastTime = p.Max(p1 => p1.CreationTime),
                       Day30Count = p.Where(p1 => p1.CreationTime >= DateTime.Now.Date.AddDays(-30)).Count()
                   })
                   on a.Id equals b.FireUnitId into g
                   from b2 in g.DefaultIfEmpty()
                   select new FireUnitManualOuput()
                   {
                       FireUnitId = a.Id,
                       FireUnitName = a.Name,
                       LastTime = b2 == null ? "" : b2.LastTime.ToString("yyyy-MM-dd"),
                       Recent30DayCount = b2 == null ? 0 : b2.Day30Count
                   };
        }
        public IQueryable<FireUnitManualOuput> GetNoDuty1DayFireUnits()
        {
            DateTime now = DateTime.Now;
            var workFireUnits = from a in _dutyRep.GetAll().Where(p => p.CreationTime > now.Date.AddDays(-1))
                                    .GroupBy(p => p.FireUnitId).Select(p => p.Key).ToList()
                                join b in _fireUnitRep.GetAll()
                                on a equals b.Id
                                select b;

            var noWorkFireUnits = _fireUnitRep.GetAll().Except(workFireUnits.ToList());
            var query = from a in noWorkFireUnits
                        join b in _dutyRep.GetAll().GroupBy(p => p.FireUnitId).Select(p => new
                        {
                            FireUnitId = p.Key,
                            LastTime = p.Max(p1 => p1.CreationTime),
                            Day30Count = p.Where(p1 => p1.CreationTime >= now.Date.AddDays(-30)).Count()
                        })
                        on a.Id equals b.FireUnitId into g
                        from b2 in g.DefaultIfEmpty()
                        select new FireUnitManualOuput()
                        {
                            FireUnitId = a.Id,
                            FireUnitName = a.Name,
                            LastTime = b2 == null ? "" : b2.LastTime.ToString("yyyy-MM-dd"),
                            Recent30DayCount = b2 == null ? 0 : b2.Day30Count
                        };
            return query;
        }

        /// <summary>
        /// 获取值班记录列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<List<GetDataDutyOutput>> GetDutylist(GetDataDutyInput input)
        {
            var dutys = _dutyRep.GetAll().Where(u=>u.FireUnitId==input.FireUnitId);
            var expr = ExprExtension.True<DataToDuty>()
                .IfAnd(input.DutyStatus != ProblemStatusType.alldate, item => item.DutyStatus==(byte)input.DutyStatus);
            dutys = dutys.Where(expr);

            var fireUnits = _fireUnitAccountRepository.GetAll();

            var output = from a in dutys
                         join b in fireUnits on a.FireUnitUserId equals b.Id
                         select new GetDataDutyOutput
                         {
                             DutyId = a.Id,
                             CreationTime = a.CreationTime.ToString("yyyy-MM-dd hh:mm"),
                             DutyUser = b.Name,
                             DutyStatus = (ProblemStatusType)a.DutyStatus
                         };
            return Task.FromResult(output.OrderByDescending(u=>u.CreationTime).ToList());
        }
        /// <summary>
        /// 获取值班记录详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<GetDataDutyInfoOutput> GetDutyInfo(GetDataDutyInfoInput input)
        {
            var duty = _dutyRep.FirstOrDefault(u => u.Id == input.DutyId);
            var problem = _dataToDutyProblemRep.FirstOrDefault(u => u.DutyId == duty.Id);
            GetDataDutyInfoOutput output = new GetDataDutyInfoOutput();
            output.DutyId = duty.Id;
            output.DutyUser = _fireUnitAccountRepository.Single(u => u.Id == duty.FireUnitUserId).Name;
            output.DutyPhtosPath = _photosPathSave.GetAll().Where(u =>u.TableName.Equals("DataToDuty") && u.DataId == duty.Id).Select(u => u.PhotoPath).ToList();
            output.DutyRemark = duty.DutyRemark;
            if(problem!=null) 
            {
                output.ProblemRemarkType = (ProblemType)problem.ProblemRemarkType;
                output.ProblemRemark = problem.ProblemRemark;
                output.ProblemPhtosPath = _photosPathSave.GetAll().Where(u => u.TableName.Equals("DataToDutyProblem") && u.DataId == problem.Id).Select(u => u.PhotoPath).ToList();
            }
            return Task.FromResult(output);
        }
        /// <summary>
        /// 新增值班记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> AddDutyInfo(AddDataDutyInfoInput input)
        {
            SuccessOutput output = new SuccessOutput() { Success = true };
            DataToDuty dutyInfo = new DataToDuty()
            {
                FireUnitUserId = input.FireUnitUserId,
                DutyRemark = input.DutyRemark,
                DutyStatus=(byte)input.DutyStatus
            };
            List<IFormFile> filelist = new List<IFormFile>();
            //if(input.DutyPicture1!=null)

            int dutyId = _dutyRep.InsertAndGetId(dutyInfo);

            if ((int)input.DutyStatus!=1)
            {
                DataToDutyProblem problemInfo = new DataToDutyProblem()
                {
                    DutyId= dutyId,
                    ProblemRemarkType=(int)input.ProblemRemarkType
                };

            }

            return output;
        }

        //private async Task<string> SavePhotos(string tableName,IFormFile file,string path)
        //{

        //}
    }
}
 