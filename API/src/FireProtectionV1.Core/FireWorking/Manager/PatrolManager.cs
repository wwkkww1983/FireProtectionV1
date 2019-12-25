using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using FireProtectionV1.Common.Helper;
using FireProtectionV1.Configuration;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Dto.Patrol;
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
    public class PatrolManager : IPatrolManager
    {
        IRepository<EquipmentNo> _repEquipmentNo;
        IRepository<FireOrtherDevice> _repFireOrtherDevice;
        IRepository<FireUnit> _repFireUnit;
        IRepository<FireUnitArchitecture> _repFireUnitArchitecture;
        IRepository<FireUnitArchitectureFloor> _repFireUnitArchitectureFloor;
        IRepository<DataToPatrolDetailFireSystem> _repDataToPatrolDetailFireSystem;
        IRepository<DataToPatrol> _repDataToPatrol;
        IRepository<DataToPatrolDetail> _repDataToPatrolDetail;
        IRepository<DataToPatrolDetailProblem> _repDataToPatrolDetailProblem;
        IRepository<FireUnitUser> _repFireUnitUser;
        IRepository<SafeUnitUser> _repSafeUnitUser;
        IRepository<FireSystem> _repFireSystem;
        IRepository<FireUnitSystem> _repFireUnitSystem;
        IRepository<PhotosPathSave> _repPhotosPathSave;
        IRepository<BreakDown> _repBreakDown;
        private IHostingEnvironment _hostingEnv;

        public PatrolManager(
            IRepository<EquipmentNo> repEquipmentNo,
            IRepository<FireOrtherDevice> repFireOrtherDevice,
            IRepository<FireUnit> repFireUnit,
            IRepository<FireUnitArchitecture> repFireUnitArchitecture,
            IRepository<FireUnitArchitectureFloor> repFireUnitArchitectureFloor,
            IRepository<DataToPatrolDetailFireSystem> repDataToPatrolDetailFireSystem,
            IRepository<DataToPatrol> repDataToPatrol,
            IRepository<DataToPatrolDetail> repDataToPatrolDetail,
            IRepository<FireUnitUser> repFireUnitUser,
            IRepository<SafeUnitUser> repSafeUnitUser,
            IRepository<FireSystem> repFireSystem,
            IRepository<FireUnitSystem> repFireUnitSystem,
            IRepository<DataToPatrolDetailProblem> repDataToPatrolDetailProblem,
            IRepository<PhotosPathSave> repPhotosPathSave,
            IRepository<BreakDown> repBreakDown,
            IHostingEnvironment env
            )
        {
            _repEquipmentNo = repEquipmentNo;
            _repFireOrtherDevice = repFireOrtherDevice;
            _repFireUnit = repFireUnit;
            _repFireUnitArchitecture = repFireUnitArchitecture;
            _repFireUnitArchitectureFloor = repFireUnitArchitectureFloor;
            _repDataToPatrol = repDataToPatrol;
            _repDataToPatrolDetailFireSystem = repDataToPatrolDetailFireSystem;
            _repFireUnitUser = repFireUnitUser;
            _repSafeUnitUser = repSafeUnitUser;
            _repDataToPatrolDetail = repDataToPatrolDetail;
            _repFireSystem = repFireSystem;
            _repFireUnitSystem = repFireUnitSystem;
            _repDataToPatrolDetailProblem = repDataToPatrolDetailProblem;
            _repPhotosPathSave = repPhotosPathSave;
            _repBreakDown = repBreakDown;
            _hostingEnv = env;
        }

        public IQueryable<DataToPatrol> GetPatrolDataAll()
        {
            return _repDataToPatrol.GetAll();
        }
        public IQueryable<DataToPatrol> GetPatrolDataMonth(int year, int month)
        {
            return _repDataToPatrol.GetAll().Where(p => p.CreationTime.Year == year && p.CreationTime.Month == month);
        }
        public IQueryable<DataToPatrol> GetPatrolDataDuration(DateTime start, DateTime end)
        {
            return _repDataToPatrol.GetAll().Where(p => p.CreationTime >= start && p.CreationTime <= end);
        }
        public IQueryable<FireUnitManualOuput> GetPatrolFireUnitsAll(string filterName = null)
        {
            return from a in _repFireUnit.GetAll().Where(p => string.IsNullOrEmpty(filterName) ? true : p.Name.Contains(filterName))
                   join b in _repDataToPatrol.GetAll().GroupBy(p => p.FireUnitId).Select(p => new
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
            var workFireUnits = from a in _repDataToPatrol.GetAll().Where(p => p.CreationTime > now.Date.AddDays(-7))
                                    .GroupBy(p => p.FireUnitId).Select(p => p.Key).ToList()
                                join b in _repFireUnit.GetAll()
                                on a equals b.Id
                                select b;

            var noWorkFireUnits = _repFireUnit.GetAll().Except(workFireUnits.ToList());
            var query = from a in noWorkFireUnits
                        join b in _repDataToPatrol.GetAll().GroupBy(p => p.FireUnitId).Select(p => new
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
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetPatrolListOutput>> GetPatrolList(GetDataPatrolInput input, PagedResultRequestDto dto)
        {
            var patrols = _repDataToPatrol.GetAll().Where(item => item.FireUnitId.Equals(input.FireUnitId));
            if (input.PatrolStatus > 0)
            {
                patrols = patrols.Where(item => item.PatrolStatus.Equals(input.PatrolStatus));
            }
            var users = _repFireUnitUser.GetAll().Where(item => item.FireUnitID.Equals(input.FireUnitId));

            var query = from a in patrols
                        select new GetPatrolListOutput()
                        {
                            CreationTime = a.CreationTime,
                            PatrolId = a.Id,
                            PatrolStatus = a.PatrolStatus,
                            FireUnitId = a.FireUnitId,
                            UserId = a.UserId,
                            UserBelongUnitId = a.UserBelongUnitId
                        };

            var lst = query.OrderByDescending(item => item.CreationTime).Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList();
            foreach (var item in lst)
            {
                if (item.FireUnitId.Equals(item.UserBelongUnitId))
                {
                    var user = await _repFireUnitUser.GetAsync(item.UserId);
                    item.UserName = user != null ? user.Name : "";
                    item.UserPhone = user != null ? user.Account : "";
                }
                else
                {
                    var user = await _repSafeUnitUser.GetAsync(item.UserId);
                    item.UserName = user != null ? user.Name : "";
                    item.UserPhone = user != null ? user.Account : "";
                }
            }
            return new PagedResultDto<GetPatrolListOutput>()
            {
                Items = lst,
                TotalCount = query.Count()
            };
        }
        /// <summary>
        /// 获取巡查记录状态统计
        /// </summary>
        /// <param name="fireunitId"></param>
        /// <returns></returns>
        public Task<GetPatrolStatusTotalOutput> GetPatrolStatusTotal(int fireunitId)
        {
            var patrols = _repDataToPatrol.GetAll().Where(item => item.FireUnitId.Equals(fireunitId));
            GetPatrolStatusTotalOutput output = new GetPatrolStatusTotalOutput()
            {
                NormalCount = patrols.Count(item => item.PatrolStatus.Equals(DutyOrPatrolStatus.Normal)),
                GreenFaultCount = patrols.Count(item => item.PatrolStatus.Equals(DutyOrPatrolStatus.Repaired)),
                RedFaultCount = patrols.Count(item => item.PatrolStatus.Equals(DutyOrPatrolStatus.DisRepaired))
            };

            return Task.FromResult(output);
        }
        /// <summary>
        /// 获取防火单位的巡查类别
        /// </summary>
        /// <param name="fireUnitId"></param>
        /// <returns></returns>
        public Task<PatrolType> GetPatrolType(int fireUnitId)
        {
            PatrolType patrolType = _repFireUnit.Get(fireUnitId).Patrol;
            return Task.FromResult(patrolType);
        }
        /// <summary>
        /// 是否允许新增巡查记录（如果存在未提交的巡查记录，则不允许新增）
        /// </summary>
        /// <param name="fireUnitId"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> GetAddPatrolAllow(int fireUnitId)
        {
            SuccessOutput output = new SuccessOutput() { Success = true };
            var cnt = await _repDataToPatrol.CountAsync(item => item.FireUnitId.Equals(fireUnitId) && item.PatrolStatus.Equals(DutyOrPatrolStatus.NoSubmit));
            if (cnt > 0)
            {
                output.Success = false;
                output.FailCause = "不允许新增，因为当前存在未提交的巡查记录";
            }
            return output;
        }
        /// <summary>
        /// 提交巡查轨迹点
        /// </summary>
        /// <param name="input"></param>
        /// <returns>返回巡查主记录Id</returns>
        public async Task<int> SubmitPatrolDetail(AddPatrolDetailInput input)
        {
            int patrolId = input.PatrolId;
            int fireUnitId = input.FireUnitId;
            int userBelongUnitId = input.UserBelongUnitId;
            int userId = input.UserId;
            int deviceId = 0;


            if (!string.IsNullOrEmpty(input.DeviceSn))
            {
                var device = await _repFireOrtherDevice.FirstOrDefaultAsync(item => item.DeviceSn.Equals(input.DeviceSn));
                Valid.Exception(device == null, $"未找到编号为{input.DeviceSn}的消防设施，需先在工作台的其它消防设施模块中录入该消防设施信息");
                deviceId = device.Id;
            }
            if (patrolId == 0)
            {
                Valid.Exception(input.FireUnitId == 0 || input.PatrolType == 0 || input.UserId == 0 || input.UserBelongUnitId == 0, "巡查记录Id为空时，必须传入巡查记录所需的信息");
                Valid.Exception(!string.IsNullOrEmpty(input.DeviceSn) && input.PatrolType.Equals(PatrolType.NormalPatrol), "保存失败，只有扫码巡查才能传入设施编码");
                // 插入巡查主表记录
                patrolId = await _repDataToPatrol.InsertAndGetIdAsync(new DataToPatrol()
                {
                    FireUnitId = input.FireUnitId,
                    PatrolType = input.PatrolType,
                    UserId = input.UserId,
                    UserBelongUnitId = input.UserBelongUnitId,
                    PatrolStatus = DutyOrPatrolStatus.NoSubmit
                });
            }
            else
            {
                var patrol = await _repDataToPatrol.GetAsync(patrolId);
                Valid.Exception(patrol == null, "保存失败，巡查主记录不存在");
                Valid.Exception(!string.IsNullOrEmpty(input.DeviceSn) && patrol.PatrolType.Equals(PatrolType.NormalPatrol), "保存失败，只有扫码巡查才能传入设施编码");
                fireUnitId = patrol.FireUnitId;
                userBelongUnitId = patrol.UserBelongUnitId;
                userId = patrol.UserId;
            }

            // 插入巡查轨迹表记录
            int patrolDetailId = await _repDataToPatrolDetail.InsertAndGetIdAsync(new DataToPatrolDetail()
            {
                PatrolId = patrolId,
                ArchitectureId = input.ArchitectureId,
                FloorId = input.FloorId,
                PatrolAddress = input.PatrolAddress,
                DeviceId = deviceId,
                PatrolStatus = input.PatrolStatus
            });

            // 保存巡查轨迹照片
            string tableName = "DataToPatrolDetail";
            string path = _hostingEnv.ContentRootPath + $@"/App_Data/Files/Photos/DataToPatrol/";
            if (input.LivePicture1 != null)
            {
                string picture1_Name = await SaveFileHelper.SaveFile(input.LivePicture1, path);
                string photopath = "/Src/Photos/DataToPatrol/" + picture1_Name;

                await _repPhotosPathSave.InsertAsync(new PhotosPathSave()
                {
                    TableName = tableName,
                    DataId = patrolDetailId,
                    PhotoPath = photopath
                });
            }
            if (input.LivePicture2 != null)
            {
                string picture2_Name = await SaveFileHelper.SaveFile(input.LivePicture2, path);
                string photopath = "/Src/Photos/DataToPatrol/" + picture2_Name;

                await _repPhotosPathSave.InsertAsync(new PhotosPathSave()
                {
                    TableName = tableName,
                    DataId = patrolDetailId,
                    PhotoPath = photopath
                });
            }
            if (input.LivePicture3 != null)
            {
                string picture3_Name = await SaveFileHelper.SaveFile(input.LivePicture3, path);
                string photopath = "/Src/Photos/DataToPatrol/" + picture3_Name;

                await _repPhotosPathSave.InsertAsync(new PhotosPathSave()
                {
                    TableName = tableName,
                    DataId = patrolDetailId,
                    PhotoPath = photopath
                });
            }

            // 如果存在问题，则向设施故障表中写入数据
            if (input.PatrolStatus != DutyOrPatrolStatus.Normal)
            {
                BreakDown breakDown = new BreakDown()
                {
                    DataId = patrolDetailId,
                    FireUnitId = fireUnitId,
                    ProblemRemark = input.ProblemRemark,
                    Source = FaultSource.Patrol,
                    UserId = userId,
                    UserBelongUnitId = userBelongUnitId
                };

                if (input.ProblemVoice != null)
                {
                    string voicePath = _hostingEnv.ContentRootPath + $@"/App_Data/Files/Voices/DataToPatrol/";
                    string voiceName = await SaveFileHelper.SaveFile(input.ProblemVoice, voicePath);

                    breakDown.ProblemVoiceUrl = "/Src/Voices/DataToPatrol/" + voiceName;
                    breakDown.VoiceLength = input.VoiceLength;
                }

                if (input.PatrolStatus == DutyOrPatrolStatus.Repaired)
                {
                    breakDown.HandleStatus = HandleStatus.Resolved;
                    breakDown.SolutionTime = DateTime.Now;
                    breakDown.SolutionWay = HandleChannel.Self;
                }
                else
                {
                    breakDown.HandleStatus = HandleStatus.UnResolve;
                }

                await _repBreakDown.InsertAsync(breakDown);
            }
            return patrolId;
        }
        /// <summary>
        /// 提交巡查主记录
        /// </summary>
        /// <param name="patrolId"></param>
        /// <returns></returns>
        public async Task SubmitPatrol(int patrolId)
        {
            var patrol = await _repDataToPatrol.GetAsync(patrolId);
            Valid.Exception(patrol == null, "未找到巡查主表数据");
            var patrolDetail = _repDataToPatrolDetail.GetAll().Where(item => item.PatrolId.Equals(patrolId)).OrderByDescending(item => item.PatrolStatus).FirstOrDefault();
            Valid.Exception(patrolDetail == null, "未找到巡查轨迹数据");

            patrol.PatrolStatus = patrolDetail.PatrolStatus;
            await _repDataToPatrol.UpdateAsync(patrol);
        }
        /// <summary>
        /// 获取巡查记录详情
        /// </summary>
        /// <param name="patrolId"></param>
        /// <returns></returns>
        public async Task<GetPatrolInfoOutput> GetPatrolInfo(int patrolId)
        {
            var patrol = await _repDataToPatrol.GetAsync(patrolId);
            string userName = "";
            string userPhone = "";
            if (patrol.FireUnitId.Equals(patrol.UserBelongUnitId))
            {
                var user = await _repFireUnitUser.GetAsync(patrol.UserId);
                if (user != null)
                {
                    userName = user.Name;
                    userPhone = user.Account;
                }
            }
            else
            {
                var user = await _repSafeUnitUser.GetAsync(patrol.UserId);
                if (user != null)
                {
                    userName = user.Name;
                    userPhone = user.Account;
                }
            }

            var patrolDetails = _repDataToPatrolDetail.GetAll().Where(item => item.PatrolId.Equals(patrolId));
            var fireUnitArchitectures = _repFireUnitArchitecture.GetAll();
            var fireUnitArchitectureFloors = _repFireUnitArchitectureFloor.GetAll();
            var breakDowns = _repBreakDown.GetAll().Where(item => item.FireUnitId.Equals(patrol.FireUnitId));
            var photosPathSave = _repPhotosPathSave.GetAll().Where(item => item.TableName.Equals("DataToPatrolDetail"));

            IQueryable<PatrolDetail> query;
            if (patrol.PatrolType.Equals(PatrolType.NormalPatrol))
            {
                query = from a in patrolDetails
                            join b in fireUnitArchitectures on a.ArchitectureId equals b.Id into result1
                            from a_b in result1.DefaultIfEmpty()
                            join c in fireUnitArchitectureFloors on a.FloorId equals c.Id into result2
                            from a_c in result2.DefaultIfEmpty()
                            join d in breakDowns on a.Id equals d.DataId into result3
                            from a_d in result3.DefaultIfEmpty()
                            select new PatrolDetail()
                            {
                                CreationTime = a.CreationTime,
                                PatrolAddress = (a.ArchitectureId > 0 ? ((a_b != null ? a_b.Name : "") + (a_c != null ? a_c.Name : "")) : "") + a.PatrolAddress,
                                Status = a.PatrolStatus,
                                PatrolPhtosPath = photosPathSave.Where(item => item.DataId.Equals(a.Id)).Select(item => item.PhotoPath).ToList(),
                                ProblemRemark = a_d != null ? a_d.ProblemRemark : "",
                                ProblemVoiceUrl = a_d != null ? a_d.ProblemVoiceUrl : "",
                                VoiceLength = a_d != null ? a_d.VoiceLength : 0
                            };
            }
            else
            {
                var devices = _repFireOrtherDevice.GetAll().Where(item => item.FireUnitId.Equals(patrol.FireUnitId));
                query = from a in patrolDetails
                        join b in fireUnitArchitectures on a.ArchitectureId equals b.Id into result1
                        from a_b in result1.DefaultIfEmpty()
                        join c in fireUnitArchitectureFloors on a.FloorId equals c.Id into result2
                        from a_c in result2.DefaultIfEmpty()
                        join d in breakDowns on a.Id equals d.DataId into result3
                        from a_d in result3.DefaultIfEmpty()
                        join e in devices on a.DeviceId equals e.Id into result4
                        from a_e in result4.DefaultIfEmpty()
                        select new PatrolDetail()
                        {
                            CreationTime = a.CreationTime,
                            PatrolAddress = (a.ArchitectureId > 0 ? ((a_b != null ? a_b.Name : "") + (a_c != null ? a_c.Name : "")) : "") + a.PatrolAddress,
                            Status = a.PatrolStatus,
                            PatrolPhtosPath = photosPathSave.Where(item => item.DataId.Equals(a.Id)).Select(item => item.PhotoPath).ToList(),
                            ProblemRemark = a_d != null ? a_d.ProblemRemark : "",
                            ProblemVoiceUrl = a_d != null ? a_d.ProblemVoiceUrl : "",
                            VoiceLength = a_d != null ? a_d.VoiceLength : 0,
                            DeviceSn = a_e != null ? a_e.DeviceSn : "",
                            DeviceName = a_e != null ? a_e.DeviceName : "",
                            DeviceModel = a_e != null ? a_e.DeviceModel : ""
                        };
            }
            var lst = query.OrderBy(item => item.CreationTime).ToList();
            foreach (var item in lst)
            {
                foreach (var f in item.PatrolPhtosPath)
                {
                    item.PatrolPhotosBase64.Add(ImageHelper.ThumbImg(_hostingEnv.ContentRootPath + f.Replace("Src", "App_Data/Files")));
                }
            }

            return new GetPatrolInfoOutput()
            {
                CreationTime = patrol.CreationTime,
                PatrolType = patrol.PatrolType,
                UserName = userName,
                UserPhone = userPhone,
                PatrolDetailList = lst
            };
        }
        /// <summary>
        /// 获取巡查记录日历列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<List<GetDataForCalendarOutput>> GetPatrollistForCalendar(GetDataForCalendarInput input)
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            if (input.CalendarDate.HasValue)
            {
                year = input.CalendarDate.Value.Year;
                month = input.CalendarDate.Value.Month;
            }

            var dataPatrols = _repDataToPatrol.GetAll().Where(item => item.FireUnitId.Equals(input.FireUnitId) && !item.PatrolStatus.Equals(DutyOrPatrolStatus.NoSubmit) && item.CreationTime.Year.Equals(year) && item.CreationTime.Month.Equals(month));

            var query = from a in dataPatrols
                        select new GetDataForCalendarOutput()
                        {
                            Id = a.Id,
                            CreationTime = a.CreationTime.ToString("yyyy-MM-dd"),
                            Status = a.PatrolStatus
                        };

            // 一天可能有多条数据，同一天中只取Status最大的那一条
            return Task.FromResult(query.GroupBy(item => item.CreationTime).Select(item => item.OrderByDescending(d => d.Status)).FirstOrDefault().ToList());
        }
    }
}
