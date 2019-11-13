using Abp.Domain.Repositories;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using FireProtectionV1.Common.Helper;
using FireProtectionV1.Configuration;
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
    public class PatrolManager:IPatrolManager
    {
        IRepository<EquipmentNo> _repEquipmentNo;
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
        IRepository<BreakDown> _breakDownRep;
        private IHostingEnvironment _hostingEnv;

        public PatrolManager(
            IRepository<EquipmentNo> repEquipmentNo,
            IRepository<FireUnit> fireUnitRep,
            //IRepository<DataToDuty> dutyRep,
            IRepository<DataToPatrol> patrolRep,
            IRepository<DataToPatrolDetail> patrolDetailRep,
            IRepository<FireUnitUser> fireUnitAccountRepository,
            IRepository<FireSystem> fireSystemRep,
            IRepository<FireUntiSystem> fireUnitSystemRep,           
            IRepository<DataToPatrolDetailProblem> patrolDetailProblemRep,
            IRepository<PhotosPathSave> photosPathSaveRep,
            IRepository<DataToPatrolDetailFireSystem> patrolDetailFireSystemRep,
            IRepository<BreakDown> breakDownRep,
            IHostingEnvironment env
            )
        {
            _repEquipmentNo = repEquipmentNo;
            _fireUnitRep = fireUnitRep;
            _patrolRep = patrolRep;
            _fireUnitAccountRepository = fireUnitAccountRepository;
            _patrolDetailRep = patrolDetailRep;
            _fireSystemRep = fireSystemRep;
            _fireUnitSystemRep = fireUnitSystemRep;
            _patrolDetailFireSystem = patrolDetailFireSystemRep;
            _patrolDetailProblem = patrolDetailProblemRep;
            _photosPathSave = photosPathSaveRep;
            _breakDownRep = breakDownRep;
            _hostingEnv = env;
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
        public async Task<GetDataPatrolPagingOutput> GetPatrollist(GetDataPatrolInput input)
        {
            var dutys = _patrolRep.GetAll().Where(u => u.FireUnitId == input.FireUnitId);
            var expr = ExprExtension.True<DataToPatrol>()
                .IfAnd(input.PatrolStatus != ProblemStatusType.alldate, item => item.PatrolStatus == (byte)input.PatrolStatus);
            dutys = dutys.Where(expr);

            var fireUnits = await _fireUnitAccountRepository.GetAllListAsync();

            var list = from a in dutys
                         join b in fireUnits on a.FireUnitUserId equals b.Id
                         orderby a.CreationTime descending
                         select new GetDataPatrolOutput
                         {
                             PatrolId = a.Id,
                             CreationTime = a.CreationTime.ToString("yyyy-MM-dd HH:mm"),
                             PatrolUser = b.Name,
                             PatrolStatus = (ProblemStatusType)a.PatrolStatus
                         };
            GetDataPatrolPagingOutput output = new GetDataPatrolPagingOutput()
            {
                TotalCount = list.Count(),
                PatrolList = list.Skip(input.SkipCount).Take(input.MaxResultCount).ToList()
            };
            return output;
        }

        /// <summary>
        /// 获取巡查记录轨迹
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<List<GetPatrolTrackOutput>> GetPatrolTrackList(GetPatrolTrackInput input)
        {
            var detaillist = _patrolDetailRep.GetAll().Where(u=>u.PatrolId==input.PatrolId);
            List<GetPatrolTrackOutput> output;
            try
            {
                var outp = from a in detaillist
                           join b in _patrolDetailProblem.GetAll() on a.Id equals b.PatrolDetailId into JoinedEmpDept
                           from dept in JoinedEmpDept.DefaultIfEmpty()
                           select new GetPatrolTrackOutput
                           {
                               PatrolId = a.PatrolId,
                               PatrolType = a.PatrolType,
                               CreationTime = a.CreationTime.ToString("yyyy-MM-dd HH:mm"),
                               PatrolStatus = (ProblemStatusType)a.PatrolStatus,
                               FireSystemNames = (from c in _patrolDetailFireSystem.GetAll().Where(u => u.PatrolDetailId == a.Id)
                                                  join d in _fireSystemRep.GetAll() on c.FireSystemID equals d.Id
                                                  select d.SystemName).ToList(),
                               FireSystemCount = _patrolDetailFireSystem.GetAll().Where(u => u.PatrolDetailId == a.Id).Count(),
                               PatrolAddress = a.PatrolAddress,
                               ProblemRemakeType = dept == null ? 0 : dept.ProblemRemarkType,
                               RemakeText = dept == null ? "" : dept.ProblemRemark,
                               VoiceLength = dept == null ? 0: dept.VoiceLength,
                               PatrolPhotosPath = (from x in _photosPathSave.GetAll()
                                                   where x.TableName == "DataToPatrolDetail" && x.DataId == a.Id
                                                   select x.PhotoPath).ToList()
                               //_photosPathSave.GetAll().Where(u => u.TableName.Equals("DataToPatrolDetail") && u.DataId == dept.Id).DefaultIfEmpty().Select(u => u.PhotoPath).ToList()
                           };
                 output = outp.ToList();
            }catch(Exception e)
            {
                throw e;
            }
            foreach(var o in output)
            {
                o.PhotosBase64 = new List<string>();
                foreach (var f in o.PatrolPhotosPath)
                {
                    o.PhotosBase64.Add(ImageHelper.ThumbImg(_hostingEnv.ContentRootPath + f.Replace("Src", "App_Data/Files")));
                }
            }
            foreach (var o in output)
            {
                o.FireSystemName = o.FireSystemNames.Count() > 0 ? o.FireSystemNames[0] : "";
            }
            return Task.FromResult(output.ToList());
        }

        /// <summary>
        /// 获取防火单位消防系统
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<List<GetPatrolFireUnitSystemOutput>> GetFireUnitlSystem(GetPatrolFireUnitSystemInput input)
        {
            var output = from a in _fireUnitSystemRep.GetAll()
                         join b in _fireSystemRep.GetAll() on a.FireSystemId equals b.Id
                         where a.FireUnitId == input.FireUnitId
                         select new GetPatrolFireUnitSystemOutput
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
        public async Task<AddPatrolOutput> AddPatrolTrack(AddPatrolInput input)
        {

            AddPatrolOutput output = new AddPatrolOutput();
            DataToPatrol patrol = new DataToPatrol()
            {
                FireUnitId = input.FireUnitId,
                FireUnitUserId = input.UserId,
                PatrolStatus = (byte)ProblemStatusType.noraml
            };
            output.PatrolId = await _patrolRep.InsertAndGetIdAsync(patrol);
            return output;

        }
        /// <summary>
        /// 添加巡查记录轨迹(所有)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> AddPatrolTrackDetailAll(AddPatrolTrackAllInput input)
        {
            //Console.WriteLine($"PatrolId = {input.PatrolId}");
            //if(input.ProblemRemarkType!=null)
            //for(int i=0;i< input.ProblemRemarkType.Count();i++)
            //    Console.WriteLine($"ProblemRemarkType[{i}] = {input.ProblemRemarkType[i]}");
            //if (input.SystemIdList != null)
            //    for (int i = 0; i < input.SystemIdList.Count(); i++)
            //    Console.WriteLine($"SystemIdList[{i}] = {input.SystemIdList[i]}");
            //if (input.DeviceSn != null)
            //    for (int i = 0; i < input.DeviceSn.Count(); i++)
            //    Console.WriteLine($"DeviceSn[{i}] = {input.DeviceSn[i]}");
            //if (input.ProblemStatus != null)
            //    for (int i = 0; i < input.ProblemStatus.Count(); i++)
            //    Console.WriteLine($"ProblemStatus[{i}] = {input.ProblemStatus[i]}");
            //if (input.LivePicture1 != null)
            //    for (int i = 0; i < input.LivePicture1.Count(); i++)
            //    Console.WriteLine($"LivePicture1[{i}] = {input.LivePicture1[i] == null}");
            //if (input.LivePicture2 != null)
            //    for (int i = 0; i < input.LivePicture2.Count(); i++)
            //    Console.WriteLine($"LivePicture2[{i}] = {input.LivePicture2[i] == null}");
            //if (input.LivePicture3 != null)
            //    for (int i = 0; i < input.LivePicture3.Count(); i++)
            //    Console.WriteLine($"LivePicture3[{i}] = {input.LivePicture3[i] == null}");
            //if (input.PatrolAddress != null)
            //    for (int i = 0; i < input.PatrolAddress.Count(); i++)
            //    Console.WriteLine($"PatrolAddress[{i}] = {input.PatrolAddress[i]}");
            //if (input.ProblemRemark != null)
            //    for (int i = 0; i < input.ProblemRemark.Count(); i++)
            //    Console.WriteLine($"ProblemRemark[{i}] = {input.ProblemRemark[i]}");
            //if (input.RemarkVioce != null)
            //    for (int i = 0; i < input.RemarkVioce.Count(); i++)
            //    Console.WriteLine($"RemarkVioce[{i}] = {input.RemarkVioce[i]==null}");
            //if (input.VoiceLength != null)
            //    for (int i = 0; i < input.VoiceLength.Count(); i++)
            //    Console.WriteLine($"VoiceLength[{i}] = {input.VoiceLength[i]}");
            var count = input.ProblemStatus.Count();
            for (int i = 0; i < count; i++)
            {
                var track = new AddPatrolTrackInput();
                track.PatrolId = input.PatrolId;
                if (input.SystemIdList != null && input.SystemIdList.Count() >i)
                    track.SystemIdList = input.SystemIdList[i];
                if (input.DeviceSn != null && input.DeviceSn.Count() == count)
                    track.DeviceSn = input.DeviceSn[i];
                if (input.ProblemStatus != null && input.ProblemStatus.Count() > i)
                    track.ProblemStatus = input.ProblemStatus[i];
                if (input.LivePicture1 != null && input.LivePicture1.Count() > i)
                    track.LivePicture1 = input.LivePicture1[i];
                if (input.LivePicture2 != null && input.LivePicture2.Count() > i)
                    track.LivePicture2 = input.LivePicture2[i];
                if (input.LivePicture3 != null && input.LivePicture3.Count() > i)
                    track.LivePicture3 = input.LivePicture3[i];
                if (input.PatrolAddress != null && input.PatrolAddress.Count() > i)
                    track.PatrolAddress = input.PatrolAddress[i];
                if (input.ProblemRemark != null && input.ProblemRemark.Count() > i)
                    track.ProblemRemark = input.ProblemRemark[i];
                if (input.ProblemRemarkType != null && input.ProblemRemarkType.Count() > i)
                    track.ProblemRemarkType = input.ProblemRemarkType[i];
                if (input.RemarkVioce != null && input.RemarkVioce.Count() > i)
                    track.RemarkVioce = input.RemarkVioce[i];
                if (input.VoiceLength != null && input.VoiceLength.Count() > i)
                    track.VoiceLength = input.VoiceLength[i];
                var res = await AddPatrolTrackDetail(track);
                if (!res.Success)
                    return res;
            }
            return new SuccessOutput() { Success = true };
        }
        /// <summary>
        /// 添加巡查记录轨迹
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> AddPatrolTrackDetail(AddPatrolTrackInput input)
        {
            SuccessOutput output = new SuccessOutput { Success = true };
            //List<AddPatrolTrackInput> TrackList = new List<AddPatrolTrackInput>();
            //TrackList.Add(input.TrackList);
            try
            {             
                string voicepath = _hostingEnv.ContentRootPath + $@"/App_Data/Files/Voices/DataToPatrol/";
                string problemtableName = "DataToPatrolDetail";
                string photopath = _hostingEnv.ContentRootPath + $@"/App_Data/Files/Photos/DataToPatrol/";
                var patrolAddress = input.PatrolAddress;
                if(string.IsNullOrEmpty(patrolAddress))
                {
                    if (!string.IsNullOrEmpty(input.DeviceSn))
                    {
                        var equip = await _repEquipmentNo.FirstOrDefaultAsync(p => p.EquiNo.Equals(input.DeviceSn));
                        if (equip != null)
                            patrolAddress = equip.Address;
                    }
                }
                DataToPatrolDetail detail = new DataToPatrolDetail()
                {
                    PatrolId = input.PatrolId,
                    PatrolAddress = patrolAddress,
                    PatrolStatus = (byte)input.ProblemStatus,
                    DeviceSn=input.DeviceSn
                };
                var pat = await _patrolRep.FirstOrDefaultAsync(p => p.Id == input.PatrolId);
                if (pat != null)
                {
                    var fireunit = await _fireUnitRep.FirstOrDefaultAsync(p => p.Id == pat.FireUnitId);
                    if (fireunit != null)
                        detail.PatrolType = fireunit.Patrol;
                }
                int detailId = await _patrolDetailRep.InsertAndGetIdAsync(detail);
                if(!string.IsNullOrEmpty(input.SystemIdList))
                {
                    String[] systemlist = input.SystemIdList.Split(",");
                    foreach (var a in systemlist)
                    {
                        DataToPatrolDetailFireSystem patrolsystem = new DataToPatrolDetailFireSystem()
                        {
                            PatrolDetailId = detailId,
                            FireSystemID = int.Parse(a)
                        };
                        await _patrolDetailFireSystem.InsertAsync(patrolsystem);
                    };
                }

                //存储照片
                if (input.LivePicture1 != null)
                    SavePhotosPath(problemtableName, detailId, await SaveFiles(input.LivePicture1, photopath));
                if (input.LivePicture2 != null)
                    SavePhotosPath(problemtableName, detailId, await SaveFiles(input.LivePicture2, photopath));
                if (input.LivePicture3 != null)
                    SavePhotosPath(problemtableName, detailId, await SaveFiles(input.LivePicture3, photopath));

                //发现问题处理
                if (detail.PatrolStatus != (byte)ProblemStatusType.noraml && detail.PatrolStatus != (byte)ProblemStatusType.alldate)
                {
                    DataToPatrolDetailProblem problem = new DataToPatrolDetailProblem()
                    {
                        PatrolDetailId = detailId,
                        ProblemRemarkType = (byte)input.ProblemRemarkType
                    };
                    if ((int)input.ProblemRemarkType == 1)
                    {
                        problem.ProblemRemark = input.ProblemRemark;
                    }
                    else if ((int)input.ProblemRemarkType == 2 && input.RemarkVioce != null)
                    {
                        problem.ProblemRemark = "/Src/Voices/DataToPatrol/" + await SaveFiles(input.RemarkVioce, voicepath);
                        problem.VoiceLength = input.VoiceLength;
                    }
                    int problemId = _patrolDetailProblem.InsertAndGetId(problem);
                   

                    //如果发生故障更改巡查最终结果的显示
                    var tracklist = _patrolDetailRep.GetAll().Where(u => u.PatrolId == input.PatrolId);
                    var patrol = _patrolRep.Single(u => u.Id == input.PatrolId);
                    if (tracklist.Where(u => u.PatrolStatus == (byte)ProblemStatusType.DisRepaired).Count() > 0)
                        patrol.PatrolStatus = (byte)ProblemStatusType.DisRepaired;
                    else if (tracklist.Where(u => u.PatrolStatus == (byte)ProblemStatusType.Repaired).Count() > 0)
                        patrol.PatrolStatus = (byte)ProblemStatusType.Repaired;
                    else
                        patrol.PatrolStatus = (byte)ProblemStatusType.noraml;


                    //每发现一个问题向故障设施插入一条数据
                    BreakDown breakdown = new BreakDown()
                    {
                        FireUnitId = patrol.FireUnitId,
                        UserId = patrol.FireUnitUserId,
                        UserFrom="FireUnitUser",
                        Source = (byte)SourceType.Patrol,
                        DataId = problemId
                    };
                    if (detail.PatrolStatus == (byte)ProblemStatusType.Repaired)
                    {
                        breakdown.HandleStatus = (byte)HandleStatus.Resolved;
                        breakdown.SolutionTime = DateTime.Now;
                        breakdown.SolutionWay = 1;
                    }
                    else
                    {
                        breakdown.HandleStatus = (byte)HandleStatus.UuResolve;
                    }
                    var id=await _breakDownRep.InsertAndGetIdAsync(breakdown);
                    var fireunit =await _fireUnitRep.FirstOrDefaultAsync(p => p.Id == patrol.FireUnitId);
                    DataApi.UpdateEvent(new GovFire.Dto.EventDto()
                    {
                        id = id.ToString(),
                        state = breakdown.HandleStatus==3?"1":"0",
                        createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        donetime = "",
                        eventcontent = problem.ProblemRemarkType==1? problem.ProblemRemark:"",
                        eventtype = BreakDownWords.GetSource(breakdown.Source),
                        firecompany = fireunit==null?"":fireunit.Name,
                        lat = fireunit == null ? "" : fireunit.Lat.ToString(),
                        lon = fireunit == null ? "" : fireunit.Lng.ToString(),
                        fireUnitId = ""
                    });

                }
                return output;
            }
            catch (Exception e)
            {
                output.Success = false;
                output.FailCause = e.Message;
                return output;
            }
        }

        private void SavePhotosPath(string tablename, int dataId, string fileName)
        {
            string photopath = "/Src/Photos/DataToPatrol/" + fileName;
            PhotosPathSave photo = new PhotosPathSave()
            {
                TableName = tablename,
                DataId = dataId,
                PhotoPath = photopath
            };
            _photosPathSave.InsertAsync(photo);
        }
        private async Task<string> SaveFiles(IFormFile file, string path)
        {
            if (!Directory.Exists(path))//判断是否存在
            {
                Directory.CreateDirectory(path);//创建新路径
            }
            string fileName = GetTimeStamp()+ Path.GetExtension(file.FileName);
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
        /// Web获取巡查记录列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<List<GetDataPatrolForWebOutput>> GetPatrollistForWeb(GetDataPatrolForWebInput input)
        {
            var list = _patrolRep.GetAll().Where(u => u.FireUnitId == input.FireUnitId && u.CreationTime.Month == input.Moth.Month);
            var list2 = from a in list
                         orderby a.CreationTime
                         select new GetDataPatrolForWebOutput
                         {
                             PatrolId = a.Id,
                             CreationTime = a.CreationTime.ToString("yyyy-MM-dd"),
                             PatrolStatus = a.PatrolStatus
                         };
            var output = list2.GroupBy(u => u.CreationTime).Select(u => u.OrderByDescending(a => a.PatrolStatus).First());
            return Task.FromResult(output.ToList());
        }

        /// <summary>
        /// Web获取巡查记录统计
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<GetDataPatrolTotalOutput> GetPatrolTotal(GetDataPatrolTotalInput input)
        {
            var list = _patrolRep.GetAll();
            var detaillist = _patrolDetailRep.GetAll();
            GetDataPatrolTotalOutput output = new GetDataPatrolTotalOutput()
            {
                PatrolCount = list.Count(),
                ProplemCount=detaillist.Where(u=>u.PatrolStatus!=(byte)ProblemStatusType.noraml).Count(),
                LiveSolutionCount=detaillist.Where(u => u.PatrolStatus != (byte)ProblemStatusType.Repaired).Count()
            };
            
            return Task.FromResult(output);
        }

        /// <summary>
        /// Web获取巡查记录详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<GetPatrolInfoForWebOutput> GetPatrolInfoForWeb(GetPatrolInfoForWebInput input)
        {
            var list = _patrolRep.GetAll().Where(u => u.FireUnitId == input.FireUnitId && u.CreationTime.Date == input.date.Date).ToList();
            var userlist = _fireUnitAccountRepository.GetAll().ToList();
            //var photoslist = _photosPathSave.GetAll();
            var detaillist = _patrolDetailRep.GetAll().ToList();
            var untilist = _fireUnitRep.GetAll().ToList();
            var output = from a in list
                         join b in userlist on a.FireUnitUserId equals b.Id
                         join c in untilist on input.FireUnitId equals c.Id
                         select new GetPatrolInfoForWebOutput
                         {
                             PatrolId = a.Id,
                             CreationTime = a.CreationTime.ToString("yyyy-MM-dd HH:mm"),
                             PatrolUser = b.Name,
                             PatrolType = (byte)c.Patrol == 1 ? "一般巡查" : "扫码巡查",
                             TrackCount = detaillist.Where(u => u.PatrolId == a.Id).Count(),
                             ProblemCount = detaillist.Where(u => u.PatrolId == a.Id && u.PatrolStatus != (byte)ProblemStatusType.noraml).Count(),
                             ResolvedConut = detaillist.Where(u => u.PatrolId == a.Id && u.PatrolStatus == (byte)ProblemStatusType.Repaired).Count(),
                             TrackList = (from d in detaillist
                                          join e in _patrolDetailProblem.GetAll() 
                                          on d.Id equals e.PatrolDetailId into JoinedEmpDept
                                          from dept in JoinedEmpDept.DefaultIfEmpty()
                                          where d.PatrolId==a.Id
                                          select new GetPatrolTrackOutput
                                          {
                                              PatrolId = d.PatrolId,
                                              TrackId=d.Id,
                                              PatrolType = d.PatrolType,
                                              CreationTime = d.CreationTime.ToString("yyyy-MM-dd HH:mm"),
                                              PatrolStatus = (ProblemStatusType)d.PatrolStatus,
                                              FireSystemName = (from e in _patrolDetailFireSystem.GetAll().Where(u => u.PatrolDetailId == d.Id)
                                                                join f in _fireSystemRep.GetAll() on e.FireSystemID equals f.Id
                                                                select f.SystemName).FirstOrDefault(),
                                              FireSystemCount = _patrolDetailFireSystem.GetAll().Where(u => u.PatrolDetailId == d.Id).Count(),
                                              PatrolAddress = d.PatrolAddress,
                                              ProblemRemakeType = dept == null ? 0 : dept.ProblemRemarkType,
                                              RemakeText = dept == null ? "":dept.ProblemRemark,
                                              VoiceLength = dept == null ? 0:dept.VoiceLength,
                                              PatrolPhotosPath = _photosPathSave.GetAll().Where(u => u.TableName.Equals("DataToPatrolDetail")&&u.DataId== d.Id).Select(u => u.PhotoPath).ToList()
                                          }).ToList()
                         };
            return Task.FromResult(output.FirstOrDefault());
        }
        /// <summary>
        /// 新增时获取巡查记录类别
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<GetPatrolTypeOutput> GetPatrolType(GetPatrolFireUnitSystemInput input)
        {
            GetPatrolTypeOutput output = new GetPatrolTypeOutput();
            output.PatrolType = (Patrol)_fireUnitRep.FirstOrDefault(u=>u.Id==input.FireUnitId).Patrol;
            return Task.FromResult(output);
        }

        /// <summary>
        /// 新增时查询今日是否已添加
        /// </summary>
        /// <returns></returns>
        public async Task<SuccessOutput> GetAddAllow(int FireUnitId)
        {
            SuccessOutput output = new SuccessOutput() { Success=true};
            if (bool.Parse(ConfigHelper.Configuration["Debug"]))
                return output;
            var date = DateTime.Now.Date;
            var count = _patrolRep.GetAll().Where(u => u.CreationTime.Date == date&&u.FireUnitId==FireUnitId).Count();
            if(count>=1)
            {
                output.Success = false;
                output.FailCause = "今日已添加过记录";
            }
            return output;
        }
    }
}
