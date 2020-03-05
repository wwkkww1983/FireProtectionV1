using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using FireProtectionV1.Common.Helper;
using FireProtectionV1.Configuration;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Model;
using FireProtectionV1.Infrastructure.Model;
using FireProtectionV1.User.Manager;
using FireProtectionV1.User.Model;
using GovFire;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.FireWorking.Manager
{
    public class AlarmManager : DomainService, IAlarmManager
    {
        IHostingEnvironment _hostingEnvironment;
        ISqlRepository _sqlRepository;
        IRepository<FireAlarmDetector> _repFireAlarmDetector;
        IRepository<FireElectricDevice> _repFireElectricDevice;
        IRepository<EngineerUser> _repEngineerUser;
        IRepository<Area> _repArea;
        IRepository<FireWaterDevice> _repFireWaterDevice;
        IRepository<DetectorType> _repDetectorType;
        IRepository<AlarmToFire> _repAlarmToFire;
        IRepository<AlarmToElectric> _repAlarmToElectric;
        IRepository<AlarmToWater> _repAlarmToWater;
        IRepository<AlarmToVision> _repAlarmToVision;
        IRepository<ShortMessageLog> _repShortMessageLog;
        IRepository<FireUnit> _repFireUnit;
        IRepository<FireAlarmDevice> _repFireAlarmDevice;
        IRepository<VisionDevice> _repVisionDevice;
        IRepository<VisionDetector> _repVisionDetector;
        IRepository<FireUnitArchitecture> _repFireUnitArchitecture;
        IRepository<FireUnitArchitectureFloor> _repFireUnitArchitectureFloor;
        IRepository<FireUnitUser> _repFireUnitUser;
        public AlarmManager(
            IHostingEnvironment hostingEnvironment,
            ISqlRepository sqlRepository,
            IRepository<Area> repArea,
            IRepository<FireAlarmDetector> repFireAlarmDetector,
            IRepository<FireElectricDevice> repFireElectricDevice,
            IRepository<FireWaterDevice> repFireWaterDevice,
            IRepository<DetectorType> repDetectorType,
            IRepository<EngineerUser> repEngineerUser,
            IRepository<ShortMessageLog> repShortMessageLog,
            IRepository<FireUnit> repFireUnit,
            IRepository<AlarmToFire> repAlarmToFire,
            IRepository<AlarmToElectric> repAlarmToElectric,
            IRepository<AlarmToWater> repAlarmToWater,
            IRepository<AlarmToVision> repAlarmToVision,
            IRepository<VisionDevice> repVisionDevice,
            IRepository<VisionDetector> repVisionDetector,
            IRepository<FireAlarmDevice> repFireAlarmDevice,
            IRepository<FireUnitArchitecture> repFireUnitArchitecture,
            IRepository<FireUnitArchitectureFloor> repFireUnitArchitectureFloor,
            IRepository<FireUnitUser> repFireUnitUser
        )
        {
            _hostingEnvironment = hostingEnvironment;
            _sqlRepository = sqlRepository;
            _repArea = repArea;
            _repFireAlarmDetector = repFireAlarmDetector;
            _repFireElectricDevice = repFireElectricDevice;
            _repFireWaterDevice = repFireWaterDevice;
            _repDetectorType = repDetectorType;
            _repEngineerUser = repEngineerUser;
            _repShortMessageLog = repShortMessageLog;
            _repFireUnit = repFireUnit;
            _repAlarmToElectric = repAlarmToElectric;
            _repAlarmToFire = repAlarmToFire;
            _repAlarmToWater = repAlarmToWater;
            _repAlarmToVision = repAlarmToVision;
            _repVisionDevice = repVisionDevice;
            _repVisionDetector = repVisionDetector;
            _repFireAlarmDevice = repFireAlarmDevice;
            _repFireUnitArchitecture = repFireUnitArchitecture;
            _repFireUnitArchitectureFloor = repFireUnitArchitectureFloor;
            _repFireUnitUser = repFireUnitUser;
        }
        public IQueryable<AlarmToElectric> GetNewElecAlarm(DateTime startTime)
        {
            return _repAlarmToElectric.GetAll().Where(p => p.CreationTime > startTime && p.CreationTime <= DateTime.Now);
        }
        /// <summary>
        /// 新增火警联网数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddAlarmFire(AddAlarmFireInput input)
        {
            FireAlarmDevice fireAlarmDevice = await _repFireAlarmDevice.FirstOrDefaultAsync(item => item.DeviceSn.Equals(input.FireAlarmDeviceSn));
            Valid.Exception(fireAlarmDevice == null, $"没有找到编号为{input.FireAlarmDeviceSn}的火警联网设施");

            FireAlarmDetector fireAlarmDetector = await _repFireAlarmDetector.FirstOrDefaultAsync(item => item.Identify.Equals(input.DetectorSn) && item.FireAlarmDeviceId.Equals(fireAlarmDevice.Id));

            // 发送报警短信
            if (fireAlarmDevice.EnableAlarmSMS)
            {
                var fireUnit = await _repFireUnit.GetAsync(fireAlarmDevice.FireUnitId);
                if (fireUnit != null && !string.IsNullOrEmpty(fireAlarmDevice.SMSPhones))
                {
                    string contents = "火警联网报警：";

                    try
                    {
                        if (fireAlarmDetector != null)
                        {
                            var detectorType = await _repDetectorType.GetAsync(fireAlarmDetector.DetectorTypeId);
                            string typeName = detectorType != null ? detectorType.Name : "火警联网探测器";
                            contents += $"位于“{fireUnit.Name}{fireAlarmDetector.FullLocation}”，编号为“{fireAlarmDetector.Identify}”的“{typeName}”发出报警，时间为“{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}”";
                        }
                        else
                        {
                            var device = await _repFireAlarmDevice.FirstOrDefaultAsync(item => item.DeviceSn.Equals(input.FireAlarmDeviceSn));
                            var ArchitectureName = _repFireUnitArchitecture.Get(device.FireUnitArchitectureId).Name;
                            contents += $"位于“{fireUnit.Name}{ArchitectureName}”，编号为“{input.DetectorSn}”的“火警联网探测器”发出报警，时间为“{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}”";
                        }
                        contents += "，请立即核警！【天树聚火警联网】";
                        int result = await ShotMessageHelper.SendMessage(new Common.Helper.ShortMessage()
                        {
                            Phones = fireAlarmDevice.SMSPhones,
                            Contents = contents
                        });

                        await _repShortMessageLog.InsertAsync(new ShortMessageLog()
                        {
                            AlarmType = AlarmType.Fire,
                            FireUnitId = fireAlarmDevice.FireUnitId,
                            Phones = fireAlarmDevice.SMSPhones,
                            Contents = contents,
                            Result = result
                        });
                    }
                    catch { }
                }
            }

            int fireAlarmDetectorId = 0;
            if (fireAlarmDetector == null)  // 如果部件数据不存在，则插入一条部件数据
            {
                var architecture = await _repFireUnitArchitecture.GetAsync(fireAlarmDevice.FireUnitArchitectureId);
                string architectureName = architecture != null ? architecture.Name : "";
                var detector = new FireAlarmDetector()
                {
                    FireUnitId = fireAlarmDevice.FireUnitId,
                    Identify = input.DetectorSn,
                    CreationTime = DateTime.Now,
                    FireAlarmDeviceId = fireAlarmDevice.Id,
                    DetectorTypeId = 13,
                    FullLocation = architectureName,
                    State = FireAlarmDetectorState.Normal
                };
                // 如果部件SN号与消防火警联网设施SN号一致，则说明是从消防火警联网设施上直接按的手动报警
                if (input.FireAlarmDeviceSn.Equals(input.DetectorSn))
                {
                    detector.DetectorTypeId = 11;
                    detector.Location = "消防控制室";
                    detector.FullLocation = architectureName + "消防控制室";
                }
                fireAlarmDetectorId = await _repFireAlarmDetector.InsertAndGetIdAsync(detector);
            }
            else
            {
                fireAlarmDetectorId = fireAlarmDetector.Id;
            }

            await _repAlarmToFire.InsertAsync(new AlarmToFire()
            {
                CreationTime = DateTime.Now,
                FireAlarmDetectorId = fireAlarmDetectorId,
                FireAlarmDeviceId = fireAlarmDevice.Id,
                FireUnitId = fireAlarmDevice.FireUnitId,
                CheckState = FireAlarmCheckState.UnCheck
            });
        }
        /// <summary>
        /// 核警处理
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task CheckFirmAlarm(AlarmCheckInput input)
        {
            var fireAlarm = await _repAlarmToFire.GetAsync(input.FireAlarmId);
            Valid.Exception(fireAlarm == null, "未找到对应的火警联网数据");

            if (input.CheckState == FireAlarmCheckState.False || input.CheckState == FireAlarmCheckState.Test || input.CheckState == FireAlarmCheckState.True)
            {
                if (input.NotifyList != null && input.NotifyList.Count > 0 && input.NotifyList[0].Contains("通知工作人员"))
                {
                    fireAlarm.NotifyWorker = true;
                    var fireUnit = await _repFireUnit.GetAsync(fireAlarm.FireUnitId);
                    if (fireUnit != null && !string.IsNullOrEmpty(fireUnit.ContractPhone))
                    {
                        string contents = "真实火警联网报警：";
                        var fireAlarmDetector = await _repFireAlarmDetector.GetAsync(fireAlarm.FireAlarmDetectorId);
                        try
                        {
                            if (fireAlarmDetector != null)
                            {
                                var detectorType = await _repDetectorType.GetAsync(fireAlarmDetector.DetectorTypeId);
                                string typeName = detectorType != null ? detectorType.Name : "火警联网探测器";
                                contents += $"位于“{fireUnit.Name}{fireAlarmDetector.FullLocation}”，编号为“{fireAlarmDetector.Identify}”的“{typeName}”发出报警，报警时间为{fireAlarm.CreationTime.ToString("yyyy-MM-dd HH:mm:ss")}，核警时间为“{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}”";
                            }
                            else
                            {
                                var device = await _repFireAlarmDevice.GetAsync(fireAlarm.FireAlarmDeviceId);
                                var ArchitectureName = _repFireUnitArchitecture.Get(device.FireUnitArchitectureId).Name;
                                contents += $"位于“{fireUnit.Name}{ArchitectureName}的“火警联网探测器”发出报警，报警时间为{fireAlarm.CreationTime.ToString("yyyy-MM-dd HH:mm:ss")}，核警时间为“{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}”";
                            }
                            var otherContent = !string.IsNullOrEmpty(input.CheckContent) ? input.CheckContent : "请立即处置！";
                            contents += $"，{otherContent}【天树聚火警联网】";
                            int result = await ShotMessageHelper.SendMessage(new Common.Helper.ShortMessage()
                            {
                                Phones = fireUnit.ContractPhone,
                                Contents = contents
                            });

                            await _repShortMessageLog.InsertAsync(new ShortMessageLog()
                            {
                                AlarmType = AlarmType.Fire,
                                FireUnitId = fireAlarm.FireUnitId,
                                Phones = fireUnit.ContractPhone,
                                Contents = contents,
                                Result = result
                            });
                        }
                        catch { }
                    }
                }
                else fireAlarm.NotifyWorker = false;
                if (input.NotifyList != null && input.NotifyList.Count > 0 && input.NotifyList[0].Contains("通知119")) fireAlarm.Notify119 = true;
                else fireAlarm.Notify119 = false;

                if (input.CheckVoice != null)
                {
                    string pathVoice = _hostingEnvironment.ContentRootPath + "/App_Data/Files/Voices/AlarmCheck/";
                    fireAlarm.CheckVoiceUrl = "/Src/Voices/AlarmCheck/" + await SaveFileHelper.SaveFile(input.CheckVoice, pathVoice);
                    fireAlarm.CheckVoiceLength = input.CheckVoiceLength;
                }
                fireAlarm.CheckState = input.CheckState;
                fireAlarm.CheckContent = input.CheckContent;
                fireAlarm.CheckTime = DateTime.Now;
                fireAlarm.CheckUserId = input.CheckUserId;

                await _repAlarmToFire.UpdateAsync(fireAlarm);
            }

            // 大联动接口
            //var fireunit = await _repFireUnit.FirstOrDefaultAsync(p => p.Id == alarmCheck.FireUnitId);
            //DataApi.UpdateEvent(new GovFire.Dto.EventDto()
            //{
            //    id = alarmCheck.AlarmDataId.ToString(),
            //    state = "1",
            //    createtime = alarmCheck.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
            //    donetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            //    eventcontent = alarmCheck.Content,
            //    eventtype = "消防火警系统报警数据",
            //    firecompany = fireunit == null ? "" : fireunit.Name,
            //    lat = fireunit == null ? "" : fireunit.Lat.ToString(),
            //    lon = fireunit == null ? "" : fireunit.Lng.ToString(),
            //    fireUnitId = ""
            //});
        }
        /// <summary>
        /// 获取防火单位数据大屏的火警联网实时达
        /// </summary>
        /// <param name="fireUnitId">防火单位Id</param>
        /// <param name="dataNum">需要的数据条数，不传的话默认为5条</param>
        /// <returns></returns>
        public Task<List<FireAlarmForDataScreenOutput>> GetFireAlarmForDataScreen(int fireUnitId, int dataNum)
        {
            var fireAlarms = _repAlarmToFire.GetAll().Where(d => fireUnitId.Equals(d.FireUnitId));
            var detectors = _repFireAlarmDetector.GetAll().Where(d => fireUnitId.Equals(d.FireUnitId));
            var detectorTypes = _repDetectorType.GetAll();

            var query = from a in fireAlarms
                        join b in detectors on a.FireAlarmDetectorId equals b.Id
                        join c in detectorTypes on b.DetectorTypeId equals c.Id
                        select new FireAlarmForDataScreenOutput()
                        {
                            FireAlarmId = a.Id,
                            CreationTime = a.CreationTime,
                            DetectorSn = b.Identify,
                            DetectorTypeName = c.Name,
                            Location = b.FullLocation,
                            ExistBitMap = b.CoordinateX != 0 ? true : false,
                            CheckState = a.CheckState
                        };

            return Task.FromResult(query.OrderByDescending(d => d.CreationTime).Take(dataNum).ToList());
        }
        /// <summary>
        /// 根据fireAlarmId获取单条火警数据详情
        /// </summary>
        /// <param name="fireAlarmId"></param>
        /// <returns></returns>
        public async Task<FireAlarmDetailOutput> GetFireAlarmById(int fireAlarmId)
        {
            var fireAlarm = await _repAlarmToFire.GetAsync(fireAlarmId);
            var fireAlarmDevice = await _repFireAlarmDevice.FirstOrDefaultAsync(d => d.Id.Equals(fireAlarm.FireAlarmDeviceId));
            var fireUnitArchitecture = fireAlarmDevice != null ? await _repFireUnitArchitecture.FirstOrDefaultAsync(fireAlarmDevice.FireUnitArchitectureId) : null;
            var fireContractUser = fireUnitArchitecture != null ? await _repFireUnitUser.FirstOrDefaultAsync(fireUnitArchitecture.FireUnitUserId) : null;
            var detector = await _repFireAlarmDetector.FirstOrDefaultAsync(fireAlarm.FireAlarmDetectorId);
            var detectorType = detector != null ? await _repDetectorType.FirstOrDefaultAsync(detector.DetectorTypeId) : null;
            var fireCheckUser = await _repFireUnitUser.FirstOrDefaultAsync(fireAlarm.CheckUserId);

            List<string> lstNotify = new List<string>();
            if (fireAlarm.NotifyWorker) lstNotify.Add("通知工作人员");
            if (fireAlarm.Notify119) lstNotify.Add("通知119");

            return new FireAlarmDetailOutput()
            {
                FireAlarmId = fireAlarm.Id,
                CreationTime = fireAlarm.CreationTime,
                GatewaySn = fireAlarmDevice?.DeviceSn,
                DetectorSn = detector?.Identify,
                DetectorTypeName = detectorType?.Name,
                Location = detector?.FullLocation,
                FireContractUser = fireContractUser != null ? $"{fireContractUser.Name}（{fireContractUser.Account}）" : "",
                CheckState = fireAlarm.CheckState,
                CheckTime = fireAlarm.CheckTime,
                Content = fireAlarm.CheckContent != null ? fireAlarm.CheckContent : "无",
                VioceUrl = fireAlarm.CheckVoiceUrl,
                VoiceLength = fireAlarm.CheckVoiceLength,
                NotifyWorker = fireAlarm.NotifyWorker,
                Notify119 = fireAlarm.Notify119,
                FireCheckUser = fireCheckUser != null ? $"{fireCheckUser.Name}（{fireCheckUser.Account}）" : "",
                NotifyList = lstNotify
            };
        }
        /// <summary>
        /// 获取火警联网数据列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<PagedResultDto<FireAlarmListOutput>> GetFireAlarmList(FireAlarmListInput input, PagedResultRequestDto dto)
        {
            if (string.IsNullOrEmpty(input.CheckStates))
            {
                return Task.FromResult(new PagedResultDto<FireAlarmListOutput>(0, new List<FireAlarmListOutput>()));
            }

            var fireAlarms = _repAlarmToFire.GetAll();
            var expr = ExprExtension.True<AlarmToFire>()
                .IfAnd(input.FireUnitId != 0, item => item.FireUnitId == input.FireUnitId)
                .IfAnd(!input.CheckStates.Contains("未核警"), item => item.CheckState != FireAlarmCheckState.UnCheck)
                .IfAnd(!input.CheckStates.Contains("误报"), item => item.CheckState != FireAlarmCheckState.False)
                .IfAnd(!input.CheckStates.Contains("测试"), item => item.CheckState != FireAlarmCheckState.Test)
                .IfAnd(!input.CheckStates.Contains("真实火警"), item => item.CheckState != FireAlarmCheckState.True)
                .IfAnd(!input.CheckStates.Contains("已过期"), item => item.CheckState != FireAlarmCheckState.Expire);
            fireAlarms = fireAlarms.Where(expr);

            var fireAlarmDevices = _repFireAlarmDevice.GetAll();
            var detectors = _repFireAlarmDetector.GetAll();
            var detectorTypes = _repDetectorType.GetAll();

            var query = from a in fireAlarms
                        join b in detectors on a.FireAlarmDetectorId equals b.Id
                        join cc in detectorTypes on b.DetectorTypeId equals cc.Id into c0
                        from c in c0.DefaultIfEmpty()
                        join d in fireAlarmDevices on a.FireAlarmDeviceId equals d.Id
                        select new FireAlarmListOutput()
                        {
                            FireAlarmId = a.Id,
                            GatewaySn = d.DeviceSn,
                            DetectorSn = b.Identify,
                            DetectorTypeName = c == null ? "" : c.Name,
                            CreationTime = a.CreationTime,
                            Location = b.FullLocation,
                            ExistBitMap = b.CoordinateX != 0 ? true : false,
                            CheckState = a.CheckState,
                            IsRead = a.IsRead
                        };

            var list = query.OrderByDescending(d => d.CreationTime).Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList();
            var tCount = query.Count();

            // 如果是手机端调用接口，则将所有的警情记录标记为已读
            if (input.VisitSource.Equals(VisitSource.Phone))
            {
                string sql = $"update alarmtofire set isread = 1 where fireunitid = {input.FireUnitId}";
                _sqlRepository.Execute(sql);
            }

            return Task.FromResult(new PagedResultDto<FireAlarmListOutput>(tCount, list));
        }
        /// <summary>
        /// 添加消防分析仪报警数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddAlarmVision(AddAlarmVisionInput input)
        {
            var device = await _repVisionDevice.FirstOrDefaultAsync(item => item.Sn.Equals(input.VisionDeviceSn));
            Valid.Exception(device == null, $"没有找到编号为{input.VisionDeviceSn}的消防分析仪设备");

            var detector = await _repVisionDetector.FirstOrDefaultAsync(item => item.VisionDeviceId.Equals(device.Id) && item.Sn.Equals(input.VisionDetectorSn));
            Valid.Exception(detector == null, $"没有找到编号为{input.VisionDetectorSn}的监控通道");

            string photopath = "";
            // 保存现场照片
            if (input.AlarmPicture != null)
            {
                string path = _hostingEnvironment.ContentRootPath + $@"/App_Data/Files/Photos/DataToVision/";

                string picture_name = await SaveFileHelper.SaveFile(input.AlarmPicture, path);
                photopath = "/Src/Photos/DataToVision/" + picture_name;
            }

            _repAlarmToVision.InsertAsync(new AlarmToVision()
            {
                VisionDeviceId = device.Id,
                VisionDetectorId = detector.Id,
                VisionAlarmType = input.VisionAlarmType,
                PhotoPath = photopath,
                FireUnitId = device.FireUnitId
            });
        }
        /// <summary>
        /// 获取防火单位消防分析仪报警列表数据
        /// </summary>
        /// <param name="input"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<PagedResultDto<AlarmVisionListOutput>> GetVisionAlarmList(AlarmVisionListInput input, PagedResultRequestDto dto)
        {
            var visionAlarms = _repAlarmToVision.GetAll().Where(item => item.FireUnitId.Equals(input.FireUnitId));
            if (input.VisionAlarmType != null && (input.VisionAlarmType.Equals(VisionAlarmType.Fire) || input.VisionAlarmType.Equals(VisionAlarmType.Passageway)))
            {
                visionAlarms = visionAlarms.Where(item => item.VisionAlarmType.Equals(input.VisionAlarmType));
            }
            var visionDevice = _repVisionDevice.GetAll();
            var visionDetector = _repVisionDetector.GetAll();

            var query = from a in visionAlarms
                        join b in visionDevice on a.VisionDeviceId equals b.Id into result1
                        from a_b in result1.DefaultIfEmpty()
                        join c in visionDetector on a.VisionDetectorId equals c.Id into result2
                        from a_c in result2.DefaultIfEmpty()
                        select new AlarmVisionListOutput()
                        {
                            VisionAlarmId = a.Id,
                            CreationTime = a.CreationTime,
                            VisionAlarmType = a.VisionAlarmType,
                            VisionDevice = a_b == null ? "" : (a_b.Sn + (a_c == null ? "" : ("-" + a_c.Sn))),
                            Location = a_c == null ? "" : a_c.Location,
                        };

            var list = query.OrderByDescending(d => d.CreationTime).Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList();
            var tCount = query.Count();

            return Task.FromResult(new PagedResultDto<AlarmVisionListOutput>(tCount, list));
        }
        /// <summary>
        /// 获取某条消防分析仪报警数据的照片
        /// </summary>
        /// <param name="visionAlarmId"></param>
        /// <returns></returns>
        public async Task<string> GetVisionAlarmPhotoPath(int visionAlarmId)
        {
            var visionAlarm = await _repAlarmToVision.GetAsync(visionAlarmId);
            return visionAlarm.PhotoPath;
        }
        /// <summary>
        /// 获取防火单位电气火灾警情数据列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<PagedResultDto<ElectricAlarmListOutput>> GetElectricAlarmList(GetElectricAlarmListInput input, PagedResultRequestDto dto)
        {
            var alarmToElectrics = _repAlarmToElectric.GetAll().Where(item => item.FireUnitId.Equals(input.FireUnitId));
            if (input.State.Equals(FireElectricDeviceState.Danger) || input.State.Equals(FireElectricDeviceState.Transfinite))
            {
                alarmToElectrics = alarmToElectrics.Where(item => item.State.Equals(input.State));
            }
            var fireElectricDevices = _repFireElectricDevice.GetAll();
            var fireUnitArchitectures = _repFireUnitArchitecture.GetAll();
            var fireUnitArchitectureFloors = _repFireUnitArchitectureFloor.GetAll();

            var query = from a in alarmToElectrics
                        join b in fireElectricDevices on a.FireElectricDeviceId equals b.Id
                        join c in fireUnitArchitectures on b.FireUnitArchitectureId equals c.Id into result1
                        from b_c in result1.DefaultIfEmpty()
                        join d in fireUnitArchitectureFloors on b.FireUnitArchitectureFloorId equals d.Id into result2
                        from b_d in result2.DefaultIfEmpty()
                        select new ElectricAlarmListOutput()
                        {
                            CreationTime = a.CreationTime,
                            FireElectricDeviceId = a.FireElectricDeviceId,
                            FireElectricDeviceSn = b.DeviceSn,
                            FireUnitArchitectureName = b_c != null ? b_c.Name : "",
                            FireUnitArchitectureFloorName = b_d != null ? b_d.Name : "",
                            Location = b.Location,
                            Sign = a.Sign,
                            State = a.State,
                            Analog = a.Analog + (a.Sign.Equals("A") ? "mA" : "℃"),
                            IsRead = a.IsFireUnitRead
                        };
            var list = query.OrderByDescending(d => d.CreationTime).Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList();
            var tCount = query.Count();

            // 如果是手机端调用接口，则将所有的警情记录标记为已读
            if (input.VisitSource.Equals(VisitSource.Phone))
            {
                string sql = $"update alarmtoelectric set IsFireUnitRead = 1 where fireunitid = {input.FireUnitId}";
                _sqlRepository.Execute(sql);
            }

            return Task.FromResult(new PagedResultDto<ElectricAlarmListOutput>(tCount, list));
        }
        /// <summary>
        /// 获取工程端电气火灾警情数据列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<ElectricAlarmListOutput>> GetElectricAlarmListForEngineer(GetElectricAlarmListForEngineerInput input, PagedResultRequestDto dto)
        {
            var alarmToElectrics = _repAlarmToElectric.GetAll();
            if (input.State.Equals(FireElectricDeviceState.Danger) || input.State.Equals(FireElectricDeviceState.Transfinite))
            {
                alarmToElectrics = alarmToElectrics.Where(item => item.State.Equals(input.State));
            }
            var engineer = await _repEngineerUser.GetAsync(input.EngineerId);
            var area = await _repArea.GetAsync(engineer.AreaId);
            var areas = _repArea.GetAll().Where(item => item.AreaPath.StartsWith(area.AreaPath));
            var fireunits = _repFireUnit.GetAll();
            var fireElectricDevices = _repFireElectricDevice.GetAll();

            var query = from a in alarmToElectrics
                        join b in fireElectricDevices on a.FireElectricDeviceId equals b.Id
                        join c in fireunits on a.FireUnitId equals c.Id
                        join d in areas on c.AreaId equals d.Id
                        select new ElectricAlarmListOutput()
                        {
                            CreationTime = a.CreationTime,
                            FireElectricDeviceId = a.FireElectricDeviceId,
                            FireElectricDeviceSn = b.DeviceSn,
                            Location = b.Location,
                            FireUnitName = c.Name,
                            Sign = a.Sign,
                            State = a.State,
                            Analog = a.Analog + (a.Sign.Equals("A") ? "mA" : "℃"),
                            IsRead = a.IsFireUnitRead
                        };
            var list = query.OrderByDescending(d => d.CreationTime).Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList();
            var tCount = query.Count();

            // 如果是手机端调用接口，则将所有的警情记录标记为已读
            if (input.VisitSource.Equals(VisitSource.Phone))
            {
                string sql = $"UPDATE alarmtoelectric SET IsEngineerRead = 1 WHERE fireunitid IN(SELECT id FROM fireunit WHERE areaid IN(SELECT id FROM AREA WHERE areapath LIKE '{area.AreaPath}%'))";
                _sqlRepository.Execute(sql);
            }

            return new PagedResultDto<ElectricAlarmListOutput>(tCount, list);
        }
        /// <summary>
        /// 获取消防管网警情数据列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<PagedResultDto<WaterAlarmListOutput>> GetWaterAlarmList(GetWaterAlarmListInput input, PagedResultRequestDto dto)
        {
            var alarmToWaters = _repAlarmToWater.GetAll().Where(item => item.FireUnitId.Equals(input.FireUnitId));
            var waterDevices = _repFireWaterDevice.GetAll();

            var query = from a in alarmToWaters
                        join b in waterDevices on a.FireWaterDeviceId equals b.Id
                        select new WaterAlarmListOutput()
                        {
                            CreationTime = a.CreationTime,
                            DeviceAddress = b.DeviceAddress,
                            MonitorType = b.MonitorType,
                            Location = b.Location,
                            Value = a.Analog + (b.MonitorType.Equals(MonitorType.Height) ? "m" : "MPa"),
                            IsRead = a.IsRead
                        };
            var list = query.OrderByDescending(d => d.CreationTime).Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList();
            var tCount = query.Count();

            // 如果是手机端调用接口，则将所有的警情记录标记为已读
            if (input.VisitSource.Equals(VisitSource.Phone))
            {
                string sql = $"update alarmtowater set isread = 1 where fireunitid = {input.FireUnitId}";
                _sqlRepository.Execute(sql);
            }

            return Task.FromResult(new PagedResultDto<WaterAlarmListOutput>(tCount, list));
        }
        /// <summary>
        /// 获取防火单位未读警情类型及数量
        /// </summary>
        /// <param name="fireUnitId"></param>
        /// <returns></returns>
        public async Task<List<GetNoReadAlarmNumOutput>> GetNoReadAlarmNumList(int fireUnitId)
        {
            List<GetNoReadAlarmNumOutput> lstOutput = new List<GetNoReadAlarmNumOutput>();
            int noReadAlarmToFireNum = await _repAlarmToFire.CountAsync(item => item.FireUnitId.Equals(fireUnitId) && !item.IsRead);
            int noReadAlarmToElectricNum = await _repAlarmToElectric.CountAsync(item => item.FireUnitId.Equals(fireUnitId) && !item.IsFireUnitRead);
            int noReadAlarmToWaterNum = await _repAlarmToWater.CountAsync(item => item.FireUnitId.Equals(fireUnitId) && !item.IsRead);
            lstOutput.Add(new GetNoReadAlarmNumOutput()
            {
                AlarmType = AlarmType.Fire,
                NoReadAlarmNum = noReadAlarmToFireNum
            });
            lstOutput.Add(new GetNoReadAlarmNumOutput()
            {
                AlarmType = AlarmType.Electric,
                NoReadAlarmNum = noReadAlarmToElectricNum
            });
            lstOutput.Add(new GetNoReadAlarmNumOutput()
            {
                AlarmType = AlarmType.Water,
                NoReadAlarmNum = noReadAlarmToWaterNum
            });
            return lstOutput;
        }
        /// <summary>
        /// 获取工程端未读警情类型及数量
        /// </summary>
        /// <param name="engineerId"></param>
        /// <returns></returns>
        public async Task<List<GetNoReadAlarmNumOutput>> GetNoReadAlarmNumListForEngineer(int engineerId)
        {
            List<GetNoReadAlarmNumOutput> lstOutput = new List<GetNoReadAlarmNumOutput>();

            var engineer = await _repEngineerUser.GetAsync(engineerId);
            var area = await _repArea.GetAsync(engineer.AreaId);
            var areas = _repArea.GetAll().Where(item => item.AreaPath.StartsWith(area.AreaPath));
            var fireunits = _repFireUnit.GetAll();
            var alarmToElectrics = _repAlarmToElectric.GetAll().Where(item => !item.IsEngineerRead);

            var query = from a in alarmToElectrics
                        join b in fireunits on a.FireUnitId equals b.Id
                        join c in areas on b.AreaId equals c.Id
                        select new
                        {
                            Id = a.Id
                        };

            lstOutput.Add(new GetNoReadAlarmNumOutput()
            {
                AlarmType = AlarmType.Electric,
                NoReadAlarmNum = query.Count()
            });
            return lstOutput;
        }
        /// <summary>
        /// 获取区域内各防火单位火警联网在某个时间段内的真实火警报119数据，如果不传year、month，则取全部时间的数据
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public Task<List<GetTrueFireAlarmListOutput>> GetAlarmTo119List(int fireDeptId, int year, int month)
        {
            var alarmToFires = _repAlarmToFire.GetAll().Where(item => item.CheckState.Equals(FireAlarmCheckState.True) && item.Notify119);
            if (year > 0)
            {
                alarmToFires = alarmToFires.Where(item => item.CheckTime.Value.Year.Equals(year));
            }
            if (month > 0)
            {
                alarmToFires = alarmToFires.Where(item => item.CheckTime.Value.Month.Equals(month));
            }
            var fireUnits = _repFireUnit.GetAll().Where(item => item.FireDeptId.Equals(fireDeptId));
            var detectors = _repFireAlarmDetector.GetAll();
            var detectorTypes = _repDetectorType.GetAll();

            var query = from a in alarmToFires
                        join b in fireUnits on a.FireUnitId equals b.Id
                        join c in detectors on a.FireAlarmDetectorId equals c.Id
                        join d in detectorTypes on c.DetectorTypeId equals d.Id
                        select new GetTrueFireAlarmListOutput()
                        {
                            FireAlarmTime = a.CreationTime,
                            FireCheckTime = (DateTime)a.CheckTime,
                            FireUnitId = a.FireUnitId,
                            FireUnitName = b.Name,
                            FireUnitAddress = b.Address,
                            ContractName = b.ContractName,
                            ContractPhone = b.ContractPhone,
                            AlarmDetectorTypeName = d.Name,
                            AlarmDetectorAddress = c.FullLocation
                        };
            return Task.FromResult(query.OrderByDescending(d => d.FireCheckTime).ToList());
        }
    }
}
