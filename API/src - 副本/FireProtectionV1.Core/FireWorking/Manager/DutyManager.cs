﻿using Abp.Domain.Repositories;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using FireProtectionV1.Common.Helper;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Model;
using FireProtectionV1.User.Model;
using GovFire;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
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
        IRepository<BreakDown> _breakDownRep;
        private IHostingEnvironment _hostingEnv;

        public DutyManager(
            IRepository<FireUnit> fireUnitRep,
            IRepository<DataToDuty> dutyRep,
            IRepository<FireUnitUser> fireUnitAccountRepository,
            IRepository<DataToDutyProblem> dataToDutyProblemRep,
            IRepository<PhotosPathSave> photosPathSaveRep,
            IRepository<BreakDown> breakDownRep,
            IHostingEnvironment env
            )
        {
            _fireUnitRep = fireUnitRep;
            _dutyRep = dutyRep;
            _fireUnitAccountRepository = fireUnitAccountRepository;
            _dataToDutyProblemRep = dataToDutyProblemRep;
            _photosPathSave = photosPathSaveRep;
            _breakDownRep = breakDownRep;
            _hostingEnv = env;
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
        public Task<GetDataDutyPagingOutput> GetDutylist(GetDataDutyInput input)
        {
            var dutys = _dutyRep.GetAll().Where(u => u.FireUnitId == input.FireUnitId);
            var expr = ExprExtension.True<DataToDuty>()
                .IfAnd(input.DutyStatus != ProblemStatusType.alldate, item => item.DutyStatus == (byte)input.DutyStatus);
            dutys = dutys.Where(expr);

            var fireUnits = _fireUnitAccountRepository.GetAll();

            var list = from a in dutys
                       join b in fireUnits on a.FireUnitUserId equals b.Id
                       orderby a.CreationTime descending
                       select new GetDataDutyOutput
                       {
                           DutyId = a.Id,
                           CreationTime = a.CreationTime.ToString("yyyy-MM-dd HH:mm"),
                           DutyUser = b.Name,
                           DutyStatus = (ProblemStatusType)a.DutyStatus
                       };
            GetDataDutyPagingOutput output = new GetDataDutyPagingOutput()
            {
                TotalCount = list.Count(),
                DutyList = list.Skip(input.SkipCount).Take(input.MaxResultCount).ToList()
            };
            return Task.FromResult(output);
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
            output.PhotosBase64Duty = new List<string>();
            foreach (var f in output.DutyPhtosPath)
            {
                output.PhotosBase64Duty.Add(ImageHelper.ThumbImg(_hostingEnv.ContentRootPath + f.Replace("Src", "App_Data/Files")));
            }
            output.DutyRemark = duty.DutyRemark;
            output.DutyStatus = (ProblemStatusType)duty.DutyStatus;

            if(problem!=null) 
            {
                output.ProblemRemarkType = (ProblemType)problem.ProblemRemarkType;
                output.ProblemRemark = problem.ProblemRemark;
                output.VoiceLength = problem.VoiceLength;

                var lst= _photosPathSave.GetAll().Where(u => u.TableName.Equals("DataToDutyProblem") && u.DataId == problem.Id).Select(u => u.PhotoPath).ToList();
                output.ProblemPhtosPath = lst;
                output.PhotosBase64 = new List<string>();
                foreach (var f in lst)
                {
                    output.PhotosBase64.Add(ImageHelper.ThumbImg(_hostingEnv.ContentRootPath+f.Replace("Src","App_Data/Files")));
                }
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

            try
            {
                byte a = (byte)input.DutyStatus;
                DataToDuty dutyInfo = new DataToDuty()
                {
                    FireUnitId=input.FireUnitId,
                    FireUnitUserId = input.FireUnitUserId,
                    DutyRemark = input.DutyRemark,
                    DutyStatus = (byte)input.DutyStatus
                };
                int dutyId = _dutyRep.InsertAndGetId(dutyInfo);

                string tableName = "DataToDuty";
                string path = _hostingEnv.ContentRootPath + $@"/App_Data/Files/Photos/DataToDuty/";
                if (input.DutyPicture1 != null)
                    SavePhotosPath(tableName, dutyId, await SaveFiles(input.DutyPicture1, path));
                if (input.DutyPicture2 != null)
                    SavePhotosPath(tableName, dutyId, await SaveFiles(input.DutyPicture2, path));
                if (input.DutyPicture3 != null)
                    SavePhotosPath(tableName, dutyId, await SaveFiles(input.DutyPicture3, path));

                if ((int)input.DutyStatus != 1)
                {
                    DataToDutyProblem problemInfo = new DataToDutyProblem()
                    {
                        DutyId = dutyId,
                        ProblemRemarkType = (int)input.ProblemRemarkType
                    };
                    string problempath = _hostingEnv.ContentRootPath+ $@"/App_Data/Files/Voices/DataToDuty/";
                    string problemtableName = "DataToDutyProblem";
                    if ((int)input.ProblemRemarkType == 1)
                    {
                        problemInfo.ProblemRemark = input.ProblemRemark;
                        problemInfo.VoiceLength = input.VoiceLength;
                    }
                    else if ((int)input.ProblemRemarkType == 2 && input.RemarkVioce != null)
                    {
                        problemInfo.ProblemRemark = "/Src/Voices/DataToDuty/" + await SaveFiles(input.RemarkVioce, problempath);
                        problemInfo.VoiceLength = input.VoiceLength;
                    }
                    int problemId = _dataToDutyProblemRep.InsertAndGetId(problemInfo);
                    if (input.ProblemPicture1 != null)
                         SavePhotosPath(problemtableName, problemId, await SaveFiles(input.ProblemPicture1, path));
                    if (input.ProblemPicture2 != null)
                         SavePhotosPath(problemtableName, problemId, await SaveFiles(input.ProblemPicture2, path));
                    if (input.ProblemPicture3 != null)
                         SavePhotosPath(problemtableName, problemId, await SaveFiles(input.ProblemPicture3, path));

                    //每发现一个问题向故障设施插入一条数据
                    BreakDown breakdown = new BreakDown()
                    {
                        FireUnitId = input.FireUnitId,
                        UserId = input.FireUnitUserId,
                        UserFrom = "FireUnitUser",
                        Source = (byte)SourceType.Duty,
                        DataId = problemId
                    };
                    if (input.DutyStatus == ProblemStatusType.Repaired)
                    {
                        breakdown.HandleStatus = (byte)HandleStatus.Resolved;
                        breakdown.SolutionTime = DateTime.Now;
                        breakdown.SolutionWay = 1;
                    }
                    else
                    {
                        breakdown.HandleStatus = (byte)HandleStatus.UuResolve;
                    }
                    var id = await _breakDownRep.InsertAndGetIdAsync(breakdown);
                    var fireunit = await _fireUnitRep.FirstOrDefaultAsync(p => p.Id == input.FireUnitId);
                    DataApi.UpdateEvent(new GovFire.Dto.EventDto()
                    {
                        id = id.ToString(),
                        state = breakdown.HandleStatus == 3 ? "1" : "0",
                        createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        donetime = "",
                        eventcontent = problemInfo.ProblemRemarkType == 1 ? problemInfo.ProblemRemark : "",
                        eventtype = BreakDownWords.GetSource(breakdown.Source),
                        firecompany = fireunit == null ? "" : fireunit.Name,
                        lat = fireunit == null ? "" : fireunit.Lat.ToString(),
                        lon = fireunit == null ? "" : fireunit.Lng.ToString(),
                        fireUnitId = ""
                    });
                }

                return output;
            }
            catch(Exception e)
            {
                output.Success = false;
                output.FailCause = e.Message;
                return output;
            }
           
        }

        private  void SavePhotosPath(string tablename, int dataId,string fileName)
        {
            string photopath = "/Src/Photos/DataToDuty/" + fileName;
            PhotosPathSave photo = new PhotosPathSave()
            {
                TableName=tablename,
                DataId=dataId,
                PhotoPath=photopath
            };
             _photosPathSave.InsertAsync(photo);
        }
        private async Task<string> SaveFiles(IFormFile file, string path)
        {
            if (!Directory.Exists(path))//判断是否存在
            {
                Directory.CreateDirectory(path);//创建新路径
            }
            string fileName = GetTimeStamp() + Path.GetExtension(file.FileName);
            using (var stream = System.IO.File.Create(path + fileName))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }

        //获取当前时间段额时间戳
        public string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }

        /// <summary>
        /// Web获取值班记录列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<List<GetDataDutyForWebOutput>> GetDutylistForWeb(GetDataDutyForWebInput input)
        {
            var list = _dutyRep.GetAll().Where(u=>u.FireUnitId==input.FireUnitId&&u.CreationTime.Month==input.Moth.Month);
            var list2 = from a in list
                         orderby a.CreationTime
                         select new GetDataDutyForWebOutput
                         {
                             DutyId = a.Id,
                             CreationTime = a.CreationTime.ToString("yyyy-MM-dd"),
                             DutyStatus = a.DutyStatus
                         };
            var output = list2.GroupBy(u => u.CreationTime).Select(u => u.OrderByDescending(a=>a.DutyStatus).First());
            return Task.FromResult(output.ToList());
        }

        /// <summary>
        /// Web获取值班记录统计
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<GetDataDutyTotalOutput> GetDutyTotal(GetDataDutyTotalInput input)
        {
            var list = _dutyRep.GetAll();
            GetDataDutyTotalOutput output = new GetDataDutyTotalOutput()
            {
                DutyCount = list.Count(),
                ProplemCount = list.Where(u => u.DutyStatus != (byte)ProblemStatusType.noraml).Count(),
                LiveSolutionCount = list.Where(u => u.DutyStatus != (byte)ProblemStatusType.Repaired).Count()
            };
            return Task.FromResult(output);
        }

        /// <summary>
        /// Web获取值班记录详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<List<GetDutyInfoForWebOutput>> GetDutyInfoForWeb(GetDataDutyInfoForWebInput input)
        {
            var list = _dutyRep.GetAll().Where(u => u.FireUnitId == input.FireUnitId && u.CreationTime.Date == input.date.Date);
            var userlist = _fireUnitAccountRepository.GetAll();
            var photoslist = _photosPathSave.GetAll();
            var output = from a in list
                         join b in userlist on a.FireUnitUserId equals b.Id
                         select new GetDutyInfoForWebOutput
                         {
                             DutyId = a.Id,
                             CreationTime = a.CreationTime.ToString("yyyy-MM-dd HH:mm"),
                             DutyUser = b.Name,
                             DutyStatus = (ProblemStatusType)a.DutyStatus,
                             DutyRemark = a.DutyRemark,
                             DutyPhtosPath = photoslist.Where(u => u.TableName == "DataToDuty" && u.DataId == a.Id).Select(u => u.PhotoPath).ToList()
                         };
            return Task.FromResult(output.ToList());
        }
    }
}
 