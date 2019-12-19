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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.FireWorking.Manager
{
    public class AlarmManager : DomainService, IAlarmManager
    {
        IRepository<FireAlarmDetector> _repFireAlarmDetector;
        IRepository<DetectorType> _repDetectorType;
        //IRepository<PhotosPathSave> _photosPathSaveRep;
        IFireUnitUserManager _fireUnitUserManager;
        IDeviceManager _deviceManager;
        IRepository<AlarmToFire> _alarmToFireRep;
        IRepository<AlarmToElectric> _alarmToElectricRep;
        IRepository<FireUnit> _repFireUnit;
        IRepository<FireAlarmDevice> _repFireAlarmDevice;
        IRepository<FireUnitArchitecture> _repFireUnitArchitecture;
        IRepository<FireUnitUser> _repFireUnitUser;
        public AlarmManager(
            IRepository<FireAlarmDetector> repFireAlarmDetector,
            IRepository<DetectorType> repDetectorType,
            IRepository<FireUnit> repFireUnit,
            //IRepository<PhotosPathSave> photosPathSaveRep,
            IFireUnitUserManager fireUnitUserManager,
            IDeviceManager deviceManager,
            IRepository<AlarmToFire> alarmToFireRep,
            IRepository<AlarmToElectric> alarmToElectricRep,
            IRepository<FireAlarmDevice> repFireAlarmDevice,
            IRepository<FireUnitArchitecture> repFireUnitArchitecture,
            IRepository<FireUnitUser> repFireUnitUser
        )
        {
            _repFireAlarmDetector = repFireAlarmDetector;
            _repDetectorType = repDetectorType;
            _repFireUnit = repFireUnit;
            //_photosPathSaveRep = photosPathSaveRep;
            _fireUnitUserManager = fireUnitUserManager;
            _deviceManager = deviceManager;
            _alarmToElectricRep = alarmToElectricRep;
            _alarmToFireRep = alarmToFireRep;
            _repFireAlarmDevice = repFireAlarmDevice;
            _repFireUnitArchitecture = repFireUnitArchitecture;
            _repFireUnitUser = repFireUnitUser;
        }
        public IQueryable<AlarmToElectric> GetNewElecAlarm(DateTime startTime)
        {
            return _alarmToElectricRep.GetAll().Where(p => p.CreationTime > startTime && p.CreationTime <= DateTime.Now);
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

            await _alarmToFireRep.InsertAsync(new AlarmToFire()
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
        public async Task CheckFirmAlarm(AlarmCheckDetailDto dto)
        {
            var fireAlarm = await _alarmToFireRep.GetAsync(dto.FireAlarmId);
            Valid.Exception(fireAlarm == null, "未找到对应的火警联网数据");

            if (dto.CheckState == FireAlarmCheckState.False || dto.CheckState == FireAlarmCheckState.Test || dto.CheckState == FireAlarmCheckState.True)
            {
                fireAlarm.CheckState = dto.CheckState;
                fireAlarm.CheckContent = dto.CheckContent;
                fireAlarm.CheckTime = DateTime.Now;
                fireAlarm.CheckUserId = dto.CheckUserId;
                fireAlarm.CheckVoiceUrl = dto.CheckVoiceUrl;
                fireAlarm.CheckVoiceLength = dto.CheckVoiceLength;

                await _alarmToFireRep.UpdateAsync(fireAlarm);
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
            var fireAlarms = _alarmToFireRep.GetAll().Where(d => fireUnitId.Equals(d.FireUnitId));
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
            var fireAlarm = await _alarmToFireRep.GetAsync(fireAlarmId);
            var fireAlarmDevice = await _repFireAlarmDevice.FirstOrDefaultAsync(d => d.Id.Equals(fireAlarm.FireAlarmDeviceId));
            var fireUnitArchitecture = fireAlarmDevice != null ? await _repFireUnitArchitecture.FirstOrDefaultAsync(fireAlarmDevice.FireUnitArchitectureId) : null;
            var fireContractUser = fireUnitArchitecture != null ? await _repFireUnitUser.FirstOrDefaultAsync(fireUnitArchitecture.FireUnitUserId) : null;
            var detector = await _repFireAlarmDetector.FirstOrDefaultAsync(fireAlarm.FireAlarmDetectorId);
            var detectorType = detector != null ? await _repDetectorType.FirstOrDefaultAsync(detector.DetectorTypeId) : null;
            var fireCheckUser = await _repFireUnitUser.FirstOrDefaultAsync(fireAlarm.CheckUserId);

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

            var fireAlarms = _alarmToFireRep.GetAll();
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
                            CheckState = a.CheckState
                        };

            var list = query.OrderByDescending(d => d.CreationTime).Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList();
            var tCount = query.Count();

            return Task.FromResult(new PagedResultDto<FireAlarmListOutput>(tCount, list));
        }
    }
}
