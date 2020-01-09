using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using FireProtectionV1.Common.Helper;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Model;
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
        IRepository<FireWaterDevice> _repFireWaterDevice;
        IRepository<DetectorType> _repDetectorType;
        IRepository<AlarmToFire> _repAlarmToFire;
        IRepository<AlarmToElectric> _repAlarmToElectric;
        IRepository<AlarmToWater> _repAlarmToWater;
        IRepository<FireUnit> _repFireUnit;
        IRepository<FireAlarmDevice> _repFireAlarmDevice;
        IRepository<FireUnitArchitecture> _repFireUnitArchitecture;
        IRepository<FireUnitArchitectureFloor> _repFireUnitArchitectureFloor;
        IRepository<FireUnitUser> _repFireUnitUser;
        public AlarmManager(
            IHostingEnvironment hostingEnvironment,
            ISqlRepository sqlRepository,
            IRepository<FireAlarmDetector> repFireAlarmDetector,
            IRepository<FireElectricDevice> repFireElectricDevice,
            IRepository<FireWaterDevice> repFireWaterDevice,
            IRepository<DetectorType> repDetectorType,
            IRepository<FireUnit> repFireUnit,
            IRepository<AlarmToFire> repAlarmToFire,
            IRepository<AlarmToElectric> repAlarmToElectric,
            IRepository<AlarmToWater> repAlarmToWater,
            IRepository<FireAlarmDevice> repFireAlarmDevice,
            IRepository<FireUnitArchitecture> repFireUnitArchitecture,
            IRepository<FireUnitArchitectureFloor> repFireUnitArchitectureFloor,
            IRepository<FireUnitUser> repFireUnitUser
        )
        {
            _hostingEnvironment = hostingEnvironment;
            _sqlRepository = sqlRepository;
            _repFireAlarmDetector = repFireAlarmDetector;
            _repFireElectricDevice = repFireElectricDevice;
            _repFireWaterDevice = repFireWaterDevice;
            _repDetectorType = repDetectorType;
            _repFireUnit = repFireUnit;
            _repAlarmToElectric = repAlarmToElectric;
            _repAlarmToFire = repAlarmToFire;
            _repAlarmToWater = repAlarmToWater;
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
            int fireAlarmDetectorId = 0;
            if (fireAlarmDetector == null)  // 如果部件数据不存在，则插入一条部件数据
            {
                fireAlarmDetectorId = await _repFireAlarmDetector.InsertAndGetIdAsync(new FireAlarmDetector()
                {
                    FireUnitId = fireAlarmDevice.FireUnitId,
                    Identify = input.DetectorSn,
                    CreationTime = DateTime.Now,
                    FireAlarmDeviceId = fireAlarmDevice.Id,
                    State = FireAlarmDetectorState.Normal
                });
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
                if (input.NotifyList.Contains("通知工作人员")) fireAlarm.NotifyWorker = true;
                else fireAlarm.NotifyWorker = false;
                if (input.NotifyList.Contains("通知微型消防站")) fireAlarm.NotifyMiniStation = true;
                else fireAlarm.NotifyMiniStation = false;

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
        /// 获取数据大屏的火警联网实时达
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
            if (fireAlarm.NotifyMiniStation) lstNotify.Add("通知微型消防站");

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
                Content = fireAlarm.CheckContent,
                VioceUrl = fireAlarm.CheckVoiceUrl,
                VoiceLength = fireAlarm.CheckVoiceLength,
                NotifyWorker = fireAlarm.NotifyWorker,
                NotifyMiniStation = fireAlarm.NotifyMiniStation,
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
                        join c in detectorTypes on b.DetectorTypeId equals c.Id
                        join d in fireAlarmDevices on a.FireAlarmDeviceId equals d.Id
                        select new FireAlarmListOutput()
                        {
                            FireAlarmId = a.Id,
                            GatewaySn = d.DeviceSn,
                            DetectorSn = b.Identify,
                            DetectorTypeName = c.Name,
                            CreationTime = a.CreationTime,
                            Location = b.FullLocation,
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
        /// 获取电气火灾警情数据列表
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
                            Analog = a.Analog,
                            IsRead = a.IsRead
                        };
            var list = query.OrderByDescending(d => d.CreationTime).Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList();
            var tCount = query.Count();

            // 如果是手机端调用接口，则将所有的警情记录标记为已读
            if (input.VisitSource.Equals(VisitSource.Phone))
            {
                string sql = $"update alarmtoelectric set isread = 1 where fireunitid = {input.FireUnitId}";
                _sqlRepository.Execute(sql);
            }

            return Task.FromResult(new PagedResultDto<ElectricAlarmListOutput>(tCount, list));
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
                            Value = a.Analog,
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
            int noReadAlarmToElectricNum = await _repAlarmToElectric.CountAsync(item => item.FireUnitId.Equals(fireUnitId) && !item.IsRead);
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
    }
}
