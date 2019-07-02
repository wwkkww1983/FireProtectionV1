using Abp.Domain.Repositories;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Model;
using FireProtectionV1.User.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.FireWorking.Manager
{
    public class PatrolManager:IPatrolManager
    {
        IRepository<FireUnit> _fireUnitRep;
        //IRepository<DataToDuty> _dutyRep;
        IRepository<DataToPatrol> _patrolRep;
        IRepository<DataToPatrolDetail> _patrolDetailRep;
        IRepository<DataToPatrolDetailProblem> _patrolDetailProblem;
        IRepository<FireUnitUser> _fireUnitAccountRepository;
        IRepository<FireSystem> _fireSystemRep;
        IRepository<FireUntiSystem> _fireUnitSystemRep;        
        IRepository<PhotosPathSave> _photosPathSave;
        IRepository<DataToPatrolDetailFireSystem> _patrolDetailFireSystem;

        public PatrolManager(
            IRepository<FireUnit> fireUnitRep,
            //IRepository<DataToDuty> dutyRep,
            IRepository<DataToPatrol> patrolRep,
            IRepository<DataToPatrolDetail> patrolDetailRep,
            IRepository<FireUnitUser> fireUnitAccountRepository,
            IRepository<FireSystem> fireSystemRep,
            IRepository<FireUntiSystem> fireUnitSystemRep,           
            IRepository<DataToPatrolDetailProblem> patrolDetailProblemRep,
            IRepository<PhotosPathSave> photosPathSaveRep,
            IRepository<DataToPatrolDetailFireSystem> patrolDetailFireSystemRep
            )
        {
            _fireUnitRep = fireUnitRep;
            _patrolRep = patrolRep;
            _fireUnitAccountRepository = fireUnitAccountRepository;
            _patrolDetailRep = patrolDetailRep;
            _fireSystemRep = fireSystemRep;
            _fireUnitSystemRep = fireUnitSystemRep;
            _patrolDetailFireSystem = patrolDetailFireSystemRep;
            _patrolDetailProblem = patrolDetailProblemRep;
            _photosPathSave = photosPathSaveRep;
        }
        public async Task AddNewPatrol(AddNewPatrolInput input)
        {
            await _patrolRep.InsertAsync(new DataToPatrol()
            {
                FireUnitId=input.FireUnitId,
                FireUnitUserId=input.FireUnitUserId
            });
        }
        public IQueryable<DataToPatrol> GetPatrolDataAll()
        {
            return _patrolRep.GetAll();
        }
        public IQueryable<DataToPatrol> GetPatrolDataMonth(int year, int month)
        {
            return _patrolRep.GetAll().Where(p => p.CreationTime.Year == year && p.CreationTime.Month == month);
        }
        public IQueryable<DataToPatrol> GetPatrolDataDuration(DateTime start, DateTime end)
        {
            return _patrolRep.GetAll().Where(p => p.CreationTime >= start && p.CreationTime <= end);
        }
        public IQueryable<FireUnitManualOuput> GetPatrolFireUnitsAll(string filterName = null)
        {
            return from a in _fireUnitRep.GetAll().Where(p => string.IsNullOrEmpty(filterName) ? true : p.Name.Contains(filterName))
                   join b in _patrolRep.GetAll().GroupBy(p => p.FireUnitId).Select(p => new
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
        public IQueryable<FireUnitManualOuput> GetNoPatrol7DayFireUnits()
        {
            DateTime now = DateTime.Now;
            var workFireUnits = from a in _patrolRep.GetAll().Where(p => p.CreationTime > now.Date.AddDays(-7))
                                    .GroupBy(p => p.FireUnitId).Select(p => p.Key).ToList()
                                join b in _fireUnitRep.GetAll()
                                on a equals b.Id
                                select b;

            var noWorkFireUnits = _fireUnitRep.GetAll().Except(workFireUnits.ToList());
            var query = from a in noWorkFireUnits
                        join b in _patrolRep.GetAll().GroupBy(p => p.FireUnitId).Select(p => new
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
        /// 获取巡查记录列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<GetDataPatrolOutput>> GetPatrollist(GetDataPatrolInput input)
        {
            var dutys = _patrolRep.GetAll().Where(u => u.FireUnitId == input.FireUnitId);
            var expr = ExprExtension.True<DataToPatrol>()
                .IfAnd(input.PatrolStatus != ProblemStatusType.alldate, item => item.PatrolStatus == (byte)input.PatrolStatus);
            dutys = dutys.Where(expr);

            var fireUnits = await _fireUnitAccountRepository.GetAllListAsync();

            var output = from a in dutys
                         join b in fireUnits on a.FireUnitUserId equals b.Id
                         select new GetDataPatrolOutput
                         {
                             PatrolId = a.Id,
                             CreationTime = a.CreationTime.ToString("yyyy-MM-dd hh:mm"),
                             PatrolUser = b.Name,
                             PatrolStatus = (ProblemStatusType)a.PatrolStatus
                         };
            return output.OrderByDescending(u => u.CreationTime).ToList();
        }

        /// <summary>
        /// 获取巡查记录轨迹
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<List<GetPatrolTrackOutput>> GetPatrolTrackList(GetPatrolTrackInput input)
        {
            var detaillist = _patrolDetailRep.GetAll().Where(u=>u.PatrolId==input.PatrolId);


            var output = from a in detaillist
                         join b in _patrolDetailProblem.GetAll() on a.Id equals b.PatrolDetailId
                         select new GetPatrolTrackOutput
                         {
                             PatrolId = a.PatrolId,
                             PatrolType = a.PatrolType,
                             CreationTime = a.CreationTime.ToString("yyyy-mm-dd hh:MM"),
                             PatrolStatus = (ProblemStatusType)a.PatrolStatus,
                             FireSystemName = _fireSystemRep.FirstOrDefault(u => u.Id == _patrolDetailFireSystem.FirstOrDefault(z => z.PatrolDetailId == a.PatrolId).Id).SystemName,
                             FireSystemCount = _patrolDetailFireSystem.GetAll().Where(u => u.PatrolDetailId == a.PatrolId).Count(),
                             PatrolAddress=a.PatrolAddress,
                             ProblemRemakeType=(ProblemType)b.ProblemRemarkType,
                             RemakeText=b.ProblemRemark,
                             PatrolPhotosPath= _photosPathSave.GetAll().Where(u=>u.TableName.Equals("DataToPatrolDetail")).Select(u=>u.PhotoPath).ToList()
                         };
            return Task.FromResult(output.ToList());
        }

        /// <summary>
        /// 获取防火单位消防系统
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<List<GetFireUnitSystemOutput>> GetFireUnitlSystem(GetFireUnitSystemInput input)
        {
            var output = from a in _fireUnitSystemRep.GetAll()
                         join b in _fireSystemRep.GetAll() on a.FireUnitId equals b.Id
                         where a.FireUnitId == input.FireUnitId
                         select new GetFireUnitSystemOutput
                         {
                             FireSystemId = b.Id,
                             SystemName = b.SystemName
                         };
            return Task.FromResult(output.ToList());
        }

        /// <summary>
        /// 添加巡查记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> AddPatrolTrack(AddPatrolInput input)
        {
            SuccessOutput output = new SuccessOutput { Success = true };
            DataToPatrol patrol = new DataToPatrol()
            {
                FireUnitId = input.FireUnitId,
                FireUnitUserId = input.UserId
            };
            if (input.TrackList.Where(u => u.ProblemStatus == ProblemStatusType.DisRepaired).Count() > 0)
                patrol.PatrolStatus = (byte)ProblemStatusType.DisRepaired;
            else if (input.TrackList.Where(u => u.ProblemStatus == ProblemStatusType.Repaired).Count() > 0)
                patrol.PatrolStatus = (byte)ProblemStatusType.Repaired;
            else
                patrol.PatrolStatus = (byte)ProblemStatusType.noraml;
            int patrolId = await _patrolRep.InsertAndGetIdAsync(patrol);

            return output;
        }


    }
}
