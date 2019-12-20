using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using DeviceServer.Tcp.Protocol;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using FireProtectionV1.Common.Helper;
using FireProtectionV1.Configuration;
using FireProtectionV1.Enterprise.Dto;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Dto.FireDevice;
using FireProtectionV1.FireWorking.Model;
using FireProtectionV1.SettingCore.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.FireWorking.Manager
{
    public class DeviceManager : IDeviceManager
    {
        IRepository<FireAlarmDeviceProtocol> _repFireAlarmDeviceProtocol;
        IRepository<FireElectricDeviceModel> _repFireElectricDeviceModel;
        IRepository<FireAlarmDeviceModel> _repFireAlarmDeviceModel;
        IRepository<FireUnitArchitectureFloor> _repFireUnitArchitectureFloor;
        IRepository<FireUnitArchitecture> _repFireUnitArchitecture;
        IRepository<FireAlarmDevice> _repFireAlarmDevice;
        IRepository<FireElectricDevice> _repFireElectricDevice;
        IRepository<FireOrtherDevice> _repFireOrtherDevice;
        IRepository<FireWaterDevice> _repFireWaterDevice;
        IRepository<FireWaterDeviceType> _repFireWaterDeviceType;
        IRepository<FireUnitSystem> _fireUnitSystemRep;
        IRepository<FireSystem> _fireSystemRep;
        IRepository<Fault> _faultRep;
        IRepository<AlarmToFire> _alarmToFireRep;
        IRepository<AlarmToElectric> _repAlarmToElectric;
        IRepository<RecordOnline> _recordOnlineRep;
        IRepository<FireElectricRecord> _repFireElectricRecord;
        IFireSettingManager _fireSettingManager;
        IRepository<DetectorType> _detectorTypeRep;
        IRepository<FireAlarmDetector> _repFireAlarmDetector;
        public DeviceManager(
            IRepository<FireAlarmDeviceProtocol> repFireAlarmDeviceProtocol,
            IRepository<FireElectricDeviceModel> repFireElectricDeviceModel,
            IRepository<FireAlarmDeviceModel> repFireAlarmDeviceModel,
            IRepository<FireUnitArchitectureFloor> repFireUnitArchitectureFloor,
            IRepository<FireUnitArchitecture> repFireUnitArchitecture,
            IRepository<FireAlarmDevice> repFireAlarmDevice,
            IRepository<FireElectricDevice> repFireElectricDevice,
            IRepository<FireOrtherDevice> repFireOrtherDevice,
            IRepository<FireWaterDevice> repFireWaterDevice,
            IRepository<FireWaterDeviceType> repFireWaterDeviceType,
            IRepository<FireUnitSystem> fireUnitSystemRep,
            IRepository<FireSystem> fireSystemRep,
            IRepository<Fault> faultRep,
            IRepository<AlarmToFire> alarmToFireRep,
            IRepository<AlarmToElectric> repAlarmToElectric,
            IRepository<RecordOnline> recordOnlineRep,
            IRepository<FireElectricRecord> repFireElectricRecord,
            IFireSettingManager fireSettingManager,
            IRepository<DetectorType> detectorTypeRep,
            IRepository<FireAlarmDetector> repFireAlarmDetector)
        {
            _repFireAlarmDeviceProtocol = repFireAlarmDeviceProtocol;
            _repFireElectricDeviceModel = repFireElectricDeviceModel;
            _repFireAlarmDeviceModel = repFireAlarmDeviceModel;
            _repFireUnitArchitectureFloor = repFireUnitArchitectureFloor;
            _repFireUnitArchitecture = repFireUnitArchitecture;
            _repFireAlarmDevice = repFireAlarmDevice;
            _repFireElectricDevice = repFireElectricDevice;
            _repFireOrtherDevice = repFireOrtherDevice;
            _repFireWaterDevice = repFireWaterDevice;
            _repFireWaterDeviceType = repFireWaterDeviceType;
            _fireUnitSystemRep = fireUnitSystemRep;
            _fireSystemRep = fireSystemRep;
            _faultRep = faultRep;
            _alarmToFireRep = alarmToFireRep;
            _repAlarmToElectric = repAlarmToElectric;
            _recordOnlineRep = recordOnlineRep;
            _repFireElectricRecord = repFireElectricRecord;
            _fireSettingManager = fireSettingManager;
            _detectorTypeRep = detectorTypeRep;
            _repFireAlarmDetector = repFireAlarmDetector;
        }
        /// <summary>
        /// 修改火警联网设施的联网部件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateFireAlarmDetector(UpdateDetectorDto input)
        {
            var fireAlarmDetector = await _repFireAlarmDetector.GetAsync(input.DetectorId);
            Valid.Exception(_repFireAlarmDetector.Count(item => item.FireAlarmDeviceId.Equals(fireAlarmDetector.FireAlarmDeviceId) && item.Identify.Equals(input.Identify) && !item.Id.Equals(input.DetectorId)) > 0,
                $"部件地址{fireAlarmDetector.Identify}已存在");

            var fireAlarmDevice = await _repFireAlarmDevice.GetAsync(fireAlarmDetector.FireAlarmDeviceId);
            var architectureName = _repFireUnitArchitecture.Get(fireAlarmDevice.FireUnitArchitectureId)?.Name;
            var floorName = _repFireUnitArchitectureFloor.Get(input.FireUnitArchitectureFloorId)?.Name;
            var detectortype = !string.IsNullOrEmpty(input.DetectorTypeName) ? await _detectorTypeRep.FirstOrDefaultAsync(p => p.Name.Equals(input.DetectorTypeName)) : null;

            fireAlarmDetector.Identify = input.Identify;
            fireAlarmDetector.DetectorTypeId = detectortype == null ? 13 : detectortype.Id;
            fireAlarmDetector.FireUnitArchitectureFloorId = input.FireUnitArchitectureFloorId;
            fireAlarmDetector.Location = input.Location;
            fireAlarmDetector.FullLocation = architectureName + floorName + input.Location;

            await _repFireAlarmDetector.UpdateAsync(fireAlarmDetector);
        }
        /// <summary>
        /// 删除其它消防设施
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public async Task DeleteFireOrtherDevice(int deviceId)
        {
            await _repFireOrtherDevice.DeleteAsync(deviceId);
        }
        /// <summary>
        /// 删除电气火灾设施
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public async Task DeleteFireElectricDevice(int deviceId)
        {
            Valid.Exception(_repFireElectricRecord.Count(item => item.FireElectricDeviceId.Equals(deviceId)) > 0, "该设施已存在运行记录，不允许被删除");

            await _repFireElectricDevice.DeleteAsync(deviceId);
        }
        /// <summary>
        /// 删除火警联网设施
        /// </summary>
        /// <param name="DeviceId"></param>
        /// <returns></returns>
        public async Task DeleteFireAlarmDevice(int deviceId)
        {
            Valid.Exception(_alarmToFireRep.Count(item => item.FireAlarmDeviceId.Equals(deviceId)) > 0, "该火警联网设施存在历史火警记录，不允许删除");
            Valid.Exception(_repFireAlarmDetector.Count(item => item.FireAlarmDeviceId.Equals(deviceId)) > 0, "该火警联网设施存在联网部件数据，不允许删除");

            await _repFireAlarmDevice.DeleteAsync(deviceId);
        }
        /// <summary>
        /// 删除火警联网部件
        /// </summary>
        /// <param name="detectorId"></param>
        /// <returns></returns>
        public async Task DeleteDetector(int detectorId)
        {
            await _repFireAlarmDetector.DeleteAsync(detectorId);
        }
        /// <summary>
        /// 添加火警联网部件
        /// </summary>
        /// <param name="detectorDto"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        public async Task<AddDeviceDetectorOutput> AddFireAlarmDetector(AddDetectorDto detectorDto, string origin)
        {
            var device = await _repFireAlarmDevice.FirstOrDefaultAsync(p => p.DeviceSn.Equals(detectorDto.DeviceSn));
            var detectortype = await _detectorTypeRep.FirstOrDefaultAsync(p => p.Name.Equals(detectorDto.DetectorTypeName));
            var fireunitArchitecture = await _repFireUnitArchitecture.FirstOrDefaultAsync(d => d.Id.Equals(device.FireUnitArchitectureId));
            var fireunitArchitectureFloor = await _repFireUnitArchitectureFloor.FirstOrDefaultAsync(d => d.Id.Equals(detectorDto.FireUnitArchitectureFloorId));
            string architectureName = fireunitArchitecture == null ? "" : fireunitArchitecture.Name;
            string architectureFloorName = fireunitArchitectureFloor == null ? "" : fireunitArchitectureFloor.Name;

            var id = await _repFireAlarmDetector.InsertAndGetIdAsync(new FireAlarmDetector()
            {
                DetectorTypeId = detectortype == null ? 13 : detectortype.Id,
                Location = detectorDto.Location,
                FaultNum = 0,
                FireUnitArchitectureFloorId = detectorDto.FireUnitArchitectureFloorId,
                FireUnitId = device.FireUnitId,
                Identify = detectorDto.Identify,
                FullLocation = architectureName + architectureFloorName + detectorDto.Location,
                FireAlarmDeviceId = device.Id,
                State = FireAlarmDetectorState.Normal,
                CreationTime = DateTime.Now
            });
            return new AddDeviceDetectorOutput() { Success = true, DetectorId = id };
        }
        /// <summary>
        /// 根据Id获取火警联网设施详情
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public async Task<GetFireAlarmDeviceDto> GetFireAlarmDevice(int deviceId)
        {
            var device = await _repFireAlarmDevice.FirstOrDefaultAsync(p => p.Id == deviceId);

            var output = new GetFireAlarmDeviceDto()
            {
                DataRate = "2小时",
                NetComms = new List<string>() { "以太网", "WIFI", "NB-IoT" },
                State = device.State.Equals(GatewayStatus.Offline) ? "离线" : "在线",
                Brand = device.Brand,
                DeviceId = device.Id,
                DeviceSn = device.DeviceSn,
                DeviceModel = device.DeviceModel,
                FireUnitArchitectureId = device.FireUnitArchitectureId,
                FireUnitId = device.FireUnitId,
                NetDetectorNum = device.NetDetectorNum,
                Protocol = device.Protocol,
                NetComm = device.NetComm,
                EnableAlarm = new List<string>(),
                EnableFault = new List<string>()
            };

            if (device.EnableAlarmCloud)
                output.EnableAlarm.Add("云端报警");
            if (device.EnableAlarmSwitch)
                output.EnableAlarm.Add("发送开关量信号");
            if (device.EnableFaultCloud)
                output.EnableFault.Add("云端报警");
            if (device.EnableFaultSwitch)
                output.EnableFault.Add("发送开关量信号");

            return output;
        }
        /// <summary>
        /// 修改火警联网设施
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateFireAlarmDevice(UpdateFireAlarmDeviceDto input)
        {
            var device = await _repFireAlarmDevice.FirstOrDefaultAsync(p => p.Id == input.DeviceId);

            device.Brand = input.Brand;
            device.DeviceSn = input.DeviceSn;
            device.DeviceModel = input.DeviceModel;
            device.EnableAlarmCloud = input.EnableAlarm.Contains("云端报警");
            device.EnableAlarmSwitch = input.EnableAlarm.Contains("发送开关量信号");
            device.EnableFaultCloud = input.EnableFault.Contains("云端报警");
            device.EnableFaultSwitch = input.EnableFault.Contains("发送开关量信号");
            device.FireUnitArchitectureId = input.FireUnitArchitectureId;
            device.NetDetectorNum = input.NetDetectorNum;
            device.Protocol = input.Protocol;
            device.NetComm = input.NetComm;

            await _repFireAlarmDevice.UpdateAsync(device);
        }
        /// <summary>
        /// 根据Id获取电气火灾设施
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public async Task<FireElectricDevice> GetFireElectricDevice(int deviceId)
        {
            return await _repFireElectricDevice.GetAsync(deviceId);
        }
        /// <summary>
        /// 获取电气火灾设备各种状态的数量
        /// </summary>
        /// <param name="fireUnitId"></param>
        /// <returns></returns>
        public Task<GetFireElectricDeviceStateOutput> GetFireElectricDeviceState(int fireUnitId)
        {
            var fireElectricDevice = _repFireElectricDevice.GetAll().Where(d => d.FireUnitId.Equals(fireUnitId));
            var output = new GetFireElectricDeviceStateOutput()
            {
                BadNum = fireElectricDevice.Count(p => p.State.Equals("隐患")),
                GoodNum = fireElectricDevice.Count(p => p.State.Equals("良好")),
                OfflineNum = fireElectricDevice.Count(p => p.State.Equals("离线")),
                WarnNum = fireElectricDevice.Count(p => p.State.Equals("超限"))
            };
            output.OnlineNum = output.BadNum + output.GoodNum + output.WarnNum;
            return Task.FromResult(output);
        }
        /// <summary>
        /// 用于数据大屏：获取各类消防物联网设施的各种状态及数量
        /// </summary>
        /// <param name="fireUnitId"></param>
        /// <returns></returns>
        public Task<List<GetDeviceStatusForDataScreenOutput>> GetDeviceStatusForDataScreen(int fireUnitId)
        {
            List<GetDeviceStatusForDataScreenOutput> lstOutput = new List<GetDeviceStatusForDataScreenOutput>();

            var fireElectricDevice = _repFireElectricDevice.GetAll().Where(d => d.FireUnitId.Equals(fireUnitId));
            int badNum = fireElectricDevice.Count(p => p.State.Equals("隐患"));
            int goodNum = fireElectricDevice.Count(p => p.State.Equals("良好"));
            int offlineNum = fireElectricDevice.Count(p => p.State.Equals("离线"));
            int warnNum = fireElectricDevice.Count(p => p.State.Equals("超限"));
            int onlineNum = badNum + goodNum + warnNum;

            var fireElectricDeviceStatusForDataScreen = new GetDeviceStatusForDataScreenOutput()
            {
                DeviceType = "电气火灾设施",
                DeviceStatusList = new List<NumOfStatus>()
            };
            fireElectricDeviceStatusForDataScreen.DeviceStatusList.Add(new NumOfStatus()
            {
                Status = "离线",
                Num = offlineNum
            });
            fireElectricDeviceStatusForDataScreen.DeviceStatusList.Add(new NumOfStatus()
            {
                Status = "在线",
                Num = onlineNum
            });
            fireElectricDeviceStatusForDataScreen.DeviceStatusList.Add(new NumOfStatus()
            {
                Status = "良好",
                Num = goodNum
            });
            fireElectricDeviceStatusForDataScreen.DeviceStatusList.Add(new NumOfStatus()
            {
                Status = "隐患",
                Num = badNum
            });
            fireElectricDeviceStatusForDataScreen.DeviceStatusList.Add(new NumOfStatus()
            {
                Status = "超限",
                Num = warnNum
            });

            var fireAlarmDevice = _repFireAlarmDevice.GetAll().Where(item => item.FireUnitId.Equals(fireUnitId));
            offlineNum = fireAlarmDevice.Count(p => p.State.Equals(GatewayStatus.Offline));
            onlineNum = fireAlarmDevice.Count(p => p.State.Equals(GatewayStatus.Online));
            var fireAlarmDeviceStatusForDataScreen = new GetDeviceStatusForDataScreenOutput()
            {
                DeviceType = "火警联网设施",
                DeviceStatusList = new List<NumOfStatus>()
            };
            fireAlarmDeviceStatusForDataScreen.DeviceStatusList.Add(new NumOfStatus()
            {
                Status = "在线",
                Num = onlineNum
            });
            fireAlarmDeviceStatusForDataScreen.DeviceStatusList.Add(new NumOfStatus()
            {
                Status = "离线",
                Num = offlineNum
            });

            var fireWaterDevice = _repFireWaterDevice.GetAll().Where(d => d.FireUnitId.Equals(fireUnitId));
            goodNum = fireWaterDevice.Count(p => p.State.Equals("良好"));
            warnNum = fireWaterDevice.Count(p => p.State.Equals("超限"));
            offlineNum = fireWaterDevice.Count(p => p.State.Equals("离线"));
            onlineNum = goodNum + warnNum;
            var fireWaterDeviceStatusForDataScreen = new GetDeviceStatusForDataScreenOutput()
            {
                DeviceType = "消防管网设施",
                DeviceStatusList = new List<NumOfStatus>()
            };
            fireWaterDeviceStatusForDataScreen.DeviceStatusList.Add(new NumOfStatus()
            {
                Status = "离线",
                Num = offlineNum
            });
            fireWaterDeviceStatusForDataScreen.DeviceStatusList.Add(new NumOfStatus()
            {
                Status = "在线",
                Num = onlineNum
            });
            fireWaterDeviceStatusForDataScreen.DeviceStatusList.Add(new NumOfStatus()
            {
                Status = "良好",
                Num = goodNum
            });
            fireWaterDeviceStatusForDataScreen.DeviceStatusList.Add(new NumOfStatus()
            {
                Status = "超限",
                Num = warnNum
            });

            lstOutput.Add(fireAlarmDeviceStatusForDataScreen);
            lstOutput.Add(fireElectricDeviceStatusForDataScreen);
            lstOutput.Add(fireWaterDeviceStatusForDataScreen);

            return Task.FromResult(lstOutput);
        }
        /// <summary>
        /// 获取指定防火单位的电气火灾设施列表
        /// </summary>
        /// <param name="fireUnitId">防火单位ID</param>
        /// <param name="state">设备状态：null/""、在线、离线、良好、隐患、超限</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<PagedResultDto<FireElectricDeviceItemDto>> GetFireElectricDeviceList(int fireUnitId, string state, PagedResultRequestDto dto)
        {
            var fireElectricDevices = _repFireElectricDevice.GetAll();
            var expr = ExprExtension.True<FireElectricDevice>()
                .IfAnd(fireUnitId != 0, item => item.FireUnitId.Equals(fireUnitId));
            if (!string.IsNullOrEmpty(state))
            {
                expr = expr.IfAnd(state.Equals("在线"), item => !item.State.Equals(FireElectricDeviceState.Offline))
                .IfAnd(state.Equals("离线"), item => item.State.Equals(FireElectricDeviceState.Offline))
                .IfAnd(state.Equals("良好"), item => item.State.Equals(FireElectricDeviceState.Good))
                .IfAnd(state.Equals("隐患"), item => item.State.Equals(FireElectricDeviceState.Danger))
                .IfAnd(state.Equals("超限"), item => item.State.Equals(FireElectricDeviceState.Transfinite));
            }
            fireElectricDevices = fireElectricDevices.Where(expr);

            var fireUnitArchitectures = _repFireUnitArchitecture.GetAll();
            var fireUnitArchitectureFloors = _repFireUnitArchitectureFloor.GetAll();
            var fireElectricRecords = _repFireElectricRecord.GetAll().Where(item => item.FireUnitId.Equals(fireUnitId)).OrderByDescending(item => item.CreationTime);

            var query = from a in fireElectricDevices
                        join b in fireUnitArchitectures on a.FireUnitArchitectureId equals b.Id into result1
                        from a_b in result1.DefaultIfEmpty()
                        join c in fireUnitArchitectureFloors on a.FireUnitArchitectureFloorId equals c.Id into result2
                        from a_c in result2.DefaultIfEmpty()
                        let tA = fireElectricRecords.FirstOrDefault(item => item.FireElectricDeviceId.Equals(a.Id) && item.Sign.Equals("A"))
                        let tL = fireElectricRecords.FirstOrDefault(item => item.FireElectricDeviceId.Equals(a.Id) && item.Sign.Equals("L"))
                        let tN = fireElectricRecords.FirstOrDefault(item => item.FireElectricDeviceId.Equals(a.Id) && item.Sign.Equals("N"))
                        let tL1 = fireElectricRecords.FirstOrDefault(item => item.FireElectricDeviceId.Equals(a.Id) && item.Sign.Equals("L1"))
                        let tL2 = fireElectricRecords.FirstOrDefault(item => item.FireElectricDeviceId.Equals(a.Id) && item.Sign.Equals("L2"))
                        let tL3 = fireElectricRecords.FirstOrDefault(item => item.FireElectricDeviceId.Equals(a.Id) && item.Sign.Equals("L3"))
                        select new FireElectricDeviceItemDto()
                        {
                            DeviceId = a.Id,
                            DeviceSn = a.DeviceSn,
                            FireUnitArchitectureId = a_b != null ? a_b.Id : 0,
                            FireUnitArchitectureName = a_b != null ? a_b.Name : "",
                            FireUnitArchitectureFloorId = a_c != null ? a_c.Id : 0,
                            FireUnitArchitectureFloorName = a_c != null ? a_c.Name : "",
                            Location = a.Location,
                            ExistAmpere = a.ExistAmpere,
                            ExistTemperature = a.ExistTemperature,
                            PhaseType = a.PhaseType,
                            State = a.State,
                            A = a.State.Equals(FireElectricDeviceState.Offline) ? "未知" : tA.Analog + "mA",
                            N = a.State.Equals(FireElectricDeviceState.Offline) ? "未知" : tN.Analog + "℃",
                            L = a.State.Equals(FireElectricDeviceState.Offline) ? "未知" : tL.Analog + "℃",
                            L1 = a.State.Equals(FireElectricDeviceState.Offline) ? "未知" : tL1.Analog + "℃",
                            L2 = a.State.Equals(FireElectricDeviceState.Offline) ? "未知" : tL2.Analog + "℃",
                            L3 = a.State.Equals(FireElectricDeviceState.Offline) ? "未知" : tL3.Analog + "℃",
                            CreationTime = a.CreationTime
                        };
            var test = query.ToList();
            return Task.FromResult(new PagedResultDto<FireElectricDeviceItemDto>()
            {
                Items = query.OrderByDescending(item => item.CreationTime).Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList(),
                TotalCount = query.Count()
            });
        }
        /// <summary>
        /// 获取某个火警联网设施下的故障部件列表
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<FaultDetectorOutput>> GetFireAlarmFaultDetectorList(int deviceId, PagedResultRequestDto dto)
        {
            var device = await _repFireAlarmDevice.FirstOrDefaultAsync(p => p.Id == deviceId);
            var fireUnitArchitecture = await _repFireUnitArchitecture.GetAsync(device.FireUnitArchitectureId);

            var fireAlarmFaultDetectors = _repFireAlarmDetector.GetAll().Where(item => item.FireAlarmDeviceId.Equals(deviceId) && item.State.Equals(FireAlarmDetectorState.Fault));
            var fireAlarmDetectorFaults = _faultRep.GetAll().Where(item => item.FireAlarmDeviceId.Equals(deviceId));
            var detectorTypes = _detectorTypeRep.GetAll();
            var fireUnitArchitectureFloors = _repFireUnitArchitectureFloor.GetAll();

            var query = from a in fireAlarmFaultDetectors
                        join b in detectorTypes on a.DetectorTypeId equals b.Id into result1
                        from a_b in result1.DefaultIfEmpty()
                        join c in fireUnitArchitectureFloors on a.FireUnitArchitectureFloorId equals c.Id into result2
                        from a_c in result2.DefaultIfEmpty()
                        select new FaultDetectorOutput()
                        {
                            DetectorId = a.Id,
                            Identify = a.Identify,
                            CreationTime = a.CreationTime,
                            DetectorTypeName = a_b != null ? a_b.Name : "",
                            Location = a.Location,
                            FireUnitArchitectureId = fireUnitArchitecture != null ? fireUnitArchitecture.Id : 0,
                            FireUnitArchitectureName = fireUnitArchitecture != null ? fireUnitArchitecture.Name : "",
                            FireUnitArchitectureFloorId = a_c != null ? a_c.Id : 0,
                            FireUnitArchitectureFloorName = a_c != null ? a_c.Name : "",
                            State = "故障",
                            FaultContent = a.LastFaultId > 0 ? fireAlarmDetectorFaults.FirstOrDefault(item => item.Id.Equals(a.LastFaultId)).FaultRemark : "",
                            FaultTime = a.LastFaultId > 0 ? fireAlarmDetectorFaults.FirstOrDefault(item => item.Id.Equals(a.LastFaultId)).CreationTime.ToString() : "",
                        };

            return new PagedResultDto<FaultDetectorOutput>()
            {
                Items = query.OrderByDescending(item => item.FaultTime).Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList(),
                TotalCount = query.Count()
            };
        }
        /// <summary>
        /// 获取火警联网设施最近30天的火警列表数据
        /// </summary>
        /// <param name="fireAlarmDeviceId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<PagedResultDto<GetFireAlarm30DayDto>> GetFireAlarm30DayList(int fireAlarmDeviceId, PagedResultRequestDto dto)
        {
            var alarmToFires = _alarmToFireRep.GetAll().Where(item => item.FireAlarmDeviceId == fireAlarmDeviceId && item.CreationTime >= DateTime.Now.AddDays(-30));
            var fireAlarmDetectors = _repFireAlarmDetector.GetAll().Where(item => item.FireAlarmDeviceId == fireAlarmDeviceId);
            var detectorTypes = _detectorTypeRep.GetAll();
            var fireUnitArchitectureFloors = _repFireUnitArchitectureFloor.GetAll();

            var query = from a in alarmToFires
                        join b in fireAlarmDetectors on a.FireAlarmDeviceId equals b.Id
                        join c in fireUnitArchitectureFloors on b.FireUnitArchitectureFloorId equals c.Id into result1
                        from b_c in result1.DefaultIfEmpty()
                        join d in detectorTypes on b.DetectorTypeId equals d.Id into result2
                        from b_d in result2.DefaultIfEmpty()
                        select new GetFireAlarm30DayDto()
                        {
                            AlarmTime = a.CreationTime,
                            DetectorTypeName = b_d != null ? b_d.Name : "",
                            FireUnitArchitectureFloorName = b_c != null ? b_c.Name : "",
                            Identify = b.Identify,
                            Location = b.Location
                        };

            return Task.FromResult(new PagedResultDto<GetFireAlarm30DayDto>()
            {
                Items = query.OrderByDescending(item => item.AlarmTime).Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList(),
                TotalCount = query.Count()
            });
        }
        /// <summary>
        /// 获取指定火警联网设施ID的高频报警部件列表
        /// </summary>
        /// <param name="fireAlarmDeviceId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<PagedResultDto<GetFireAlarmHighDto>> GetFireAlarmHighList(int fireAlarmDeviceId, PagedResultRequestDto dto)
        {
            int highFreq = int.Parse(ConfigHelper.Configuration["FireDomain:HighFreqAlarm"]);

            var lstDetectorHigh = _alarmToFireRep.GetAll().Where(item => item.FireAlarmDeviceId == fireAlarmDeviceId && item.CreationTime >= DateTime.Now.AddDays(-30))
                .GroupBy(item => item.FireAlarmDetectorId).Where(item => item.Count() >= highFreq).Select(item => new
                {
                    FireAlarmDetectorId = item.Key,
                    AlarmNum = item.Count()
                }).ToList();
            var fireAlarmDetectors = _repFireAlarmDetector.GetAll().Where(item => item.FireAlarmDeviceId == fireAlarmDeviceId);
            var detectorTypes = _detectorTypeRep.GetAll();
            var fireUnitArchitectureFloors = _repFireUnitArchitectureFloor.GetAll();

            var query = from a in lstDetectorHigh
                        join b in fireAlarmDetectors on a.FireAlarmDetectorId equals b.Id
                        join c in detectorTypes on b.DetectorTypeId equals c.Id into result1
                        from b_c in result1.DefaultIfEmpty()
                        join d in fireUnitArchitectureFloors on b.FireUnitArchitectureFloorId equals d.Id into result2
                        from b_d in result2.DefaultIfEmpty()
                        select new GetFireAlarmHighDto()
                        {
                            Identify = b.Identify,
                            DetectorTypeName = b_c != null ? b_c.Name : "",
                            FireUnitArchitectureFloorName = b_d != null ? b_d.Name : "",
                            Location = b.Location,
                            AlarmNum = a.AlarmNum
                        };

            return Task.FromResult(new PagedResultDto<GetFireAlarmHighDto>()
            {
                Items = query.OrderByDescending(item => item.AlarmNum).Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList(),
                TotalCount = query.Count()
            });
        }
        /// <summary>
        /// 获取指定防火单位ID的火警联网设施列表
        /// </summary>
        /// <param name="fireUnitId">防火单位ID</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<PagedResultDto<FireAlarmDeviceItemDto>> GetFireAlarmDeviceList(int fireUnitId, PagedResultRequestDto dto)
        {
            int highFreq = int.Parse(ConfigHelper.Configuration["FireDomain:HighFreqAlarm"]);

            var fireAlarmDevices = _repFireAlarmDevice.GetAll().Where(item => item.FireUnitId.Equals(fireUnitId));
            var fireUnitArchitectures = _repFireUnitArchitecture.GetAll();

            // 30天火警数量及高频报警部件数量
            var groupFireAlarm = _alarmToFireRep.GetAll().Where(item => item.FireUnitId == fireUnitId && item.CreationTime >= DateTime.Now.AddDays(-30))
                .GroupBy(item => item.FireAlarmDeviceId).Select(p => new
                {
                    FireAlarmDeviceId = p.Key,
                    AlarmNum = p.Count(),
                    HighDeviceNum = p.GroupBy(p1 => p1.FireAlarmDetectorId).Where(p1 => p1.Count() > highFreq).Count()
                }).ToList();
            // 故障部件数量及部件故障率
            var groupGtFault = _repFireAlarmDetector.GetAll().Where(p => p.FireUnitId == fireUnitId).GroupBy(p => p.FireAlarmDeviceId).Select(p => new
            {
                FireAlarmDeviceId = p.Key,
                FaultNum = p.Where(p1 => p1.FaultNum > 0).Count(),
                FaultRate = p.Count() == 0 ? "0%" : (p.Where(p1 => p1.FaultNum > 0).Count() / (double)p.Count()).ToString("P")
            }).ToList();

            var query = from a in fireAlarmDevices
                        join b in groupGtFault on a.Id equals b.FireAlarmDeviceId into result1
                        from a_b in result1.DefaultIfEmpty()
                        join c in groupFireAlarm on a.Id equals c.FireAlarmDeviceId into result2
                        from a_c in result2.DefaultIfEmpty()
                        join d in fireUnitArchitectures on a.FireUnitArchitectureId equals d.Id into result3
                        from a_d in result3.DefaultIfEmpty()
                        select new FireAlarmDeviceItemDto()
                        {
                            DeviceId = a.Id,
                            DeviceSn = a.DeviceSn,
                            State = a.State.Equals(GatewayStatus.Offline) ? "离线" : "在线",
                            FireUnitArchitectureId = a_d != null ? a_d.Id : 0,
                            FireUnitArchitectureName = a_d != null ? a_d.Name : "",
                            NetDetectorNum = a.NetDetectorNum,
                            FaultDetectorNum = a_b != null ? a_b.FaultNum : 0,
                            DetectorFaultRate = a_b != null ? a_b.FaultRate : "0%",
                            AlarmNum30Day = a_c != null ? a_c.AlarmNum : 0,
                            HighAlarmDetectorNum = a_c != null ? a_c.HighDeviceNum : 0,
                            CreationTime = a.CreationTime
                        };

            return Task.FromResult(new PagedResultDto<FireAlarmDeviceItemDto>()
            {
                Items = query.OrderByDescending(item => item.CreationTime).Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList(),
                TotalCount = query.Count()
            });
        }
        /// <summary>
        /// 根据火警联网设备Id获取其下的部件列表
        /// </summary>
        /// <param name="fireAlarmDeviceId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<FireAlarmDetectorDto>> GetDeviceDetectorList(int fireAlarmDeviceId, PagedResultRequestDto dto)
        {
            var fireAlarmDevice = await _repFireAlarmDevice.GetAsync(fireAlarmDeviceId);
            Valid.Exception(fireAlarmDevice == null, "未找到对应的火警联网设施");
            var fireUnitArchitecture = await _repFireUnitArchitecture.GetAsync(fireAlarmDevice.FireUnitArchitectureId);

            var fireAlarmDetectors = _repFireAlarmDetector.GetAll().Where(item => item.FireAlarmDeviceId == fireAlarmDeviceId);
            var detectorTypes = _detectorTypeRep.GetAll();
            var fireUnitArchitectureFloors = _repFireUnitArchitectureFloor.GetAll();

            var query = from a in fireAlarmDetectors
                        join b in detectorTypes on a.DetectorTypeId equals b.Id into result1
                        from a_b in result1.DefaultIfEmpty()
                        join c in fireUnitArchitectureFloors on a.FireUnitArchitectureFloorId equals c.Id into result2
                        from a_c in result2.DefaultIfEmpty()
                        select new FireAlarmDetectorDto()
                        {
                            DetectorId = a.Id,
                            DetectorTypeName = a_b != null ? a_b.Name : "",
                            FireUnitArchitectureId = fireUnitArchitecture != null ? fireUnitArchitecture.Id : 0,
                            FireUnitArchitectureName = fireUnitArchitecture != null ? fireUnitArchitecture.Name : "",
                            FireUnitArchitectureFloorId = a_c != null ? a_c.Id : 0,
                            FireUnitArchitectureFloorName = a_c != null ? a_c.Name : "",
                            Identify = a.Identify,
                            Location = a.Location,
                            State = a.State.Equals(FireAlarmDetectorState.Fault) ? "故障" : "正常",
                            CreationTime = a.CreationTime
                        };

            return new PagedResultDto<FireAlarmDetectorDto>()
            {
                Items = query.OrderByDescending(item => item.CreationTime).Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList(),
                TotalCount = query.Count()
            };
        }
        /// <summary>
        /// 查询防火单位网关状态列表
        /// </summary>
        /// <param name="fireSysType"></param>
        /// <param name="fireUnitId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        //public async Task<PagedResultDto<GatewayStatusOutput>> GetFireUnitGatewaysStatus(int fireSysType, int fireUnitId, PagedResultRequestDto dto)
        //{
        //    var status = _gatewayRep.GetAll().Where(p => p.FireSysType == fireSysType && p.FireUnitId == fireUnitId).Select(p => new GatewayStatusOutput()
        //    {
        //        Gateway = p.Identify,
        //        Location = p.Location,
        //        Status = GatewayStatusNames.GetName(p.Status)
        //    });
        //    return await Task.Run<PagedResultDto<GatewayStatusOutput>>(() =>
        //    {
        //        return new PagedResultDto<GatewayStatusOutput>()
        //        {
        //            TotalCount = status.Count(),
        //            Items = status.Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList()
        //        };
        //    });
        //}
        /// <summary>
        /// 非模拟量探测器历史记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //public async Task<RecordUnAnalogOutput> GetRecordUnAnalog(GetRecordDetectorInput input)
        //{
        //    var output = new RecordUnAnalogOutput();
        //    var detector = await _repFireAlarmDetector.SingleAsync(p => p.Id == input.DetectorId);
        //    var detectorType = await _detectorTypeRep.SingleAsync(p => p.Id == detector.DetectorTypeId);
        //    output.Name = detectorType.Name;
        //    output.Location = detector.Location;
        //    var state = _recordOnlineRep.GetAll().Where(p => p.DetectorId == input.DetectorId).OrderByDescending(p => p.CreationTime).FirstOrDefault();
        //    if (state != null)
        //    {
        //        output.State = GatewayStatusNames.GetName((GatewayStatus)state.State);
        //        output.LastTimeStateChange = state.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
        //    }
        //    List<string> dates = new List<string>();
        //    for (DateTime dt = input.Start; dt <= input.End; dt = dt.AddDays(1))
        //    {
        //        dates.Add(dt.ToString("yyyy-MM-dd"));
        //    }
        //    var onlines = from a in dates
        //                  join b in _recordOnlineRep.GetAll().Where(p => p.DetectorId == input.DetectorId && p.State == (sbyte)GatewayStatus.Offline && p.CreationTime >= input.Start && p.CreationTime <= input.End)
        //                  .GroupBy(p => p.CreationTime.ToString("yyyy-MM-dd"))
        //                  on a equals b.Key into u
        //                  from c in u.DefaultIfEmpty()
        //                  select new
        //                  {
        //                      Time = a,
        //                      Count = c == null ? 0 : c.Count()
        //                  };
        //    var gatawayDetector = _repFireAlarmDetector.Single(p => p.Id == input.DetectorId);
        //    var gatway = _gatewayRep.Single(p => p.Identify == gatawayDetector.Identify && p.Origin == gatawayDetector.Origin);
        //    IQueryable<Detector> detectors = _repFireAlarmDetector.GetAll().Where(p => p.FireAlarmDeviceId == gatway.Id);
        //    //报警包括UITD和下属部件的报警
        //    var alarmTofires = from a in detectors
        //                       join b in _alarmToFireRep.GetAll().Where(p => p.CreationTime >= input.Start && p.CreationTime <= input.End)
        //                       on a.Id equals b.FireAlarmDetectorId
        //                       select b;
        //    var alarms = from a in dates
        //                 join b in alarmTofires.GroupBy(p => p.CreationTime.ToString("yyyy-MM-dd"))
        //                 on a equals b.Key into u
        //                 from c in u.DefaultIfEmpty()
        //                 select new
        //                 {
        //                     Time = a,
        //                     Count = c == null ? 0 : c.Count()
        //                 };
        //    var faultdatas = from a in detectors
        //                     join b in _faultRep.GetAll().Where(p => p.CreationTime >= input.Start && p.CreationTime <= input.End)
        //                     on a.Id equals b.DetectorId
        //                     select b;
        //    var faults = from a in dates
        //                 join b in faultdatas.GroupBy(p => p.CreationTime.ToString("yyyy-MM-dd"))
        //                 on a equals b.Key into u
        //                 from c in u.DefaultIfEmpty()
        //                 select new
        //                 {
        //                     Time = a,
        //                     Count = c == null ? 0 : c.Count()
        //                 };
        //    int m = faults.Count();
        //    int m2 = alarms.Count();
        //    output.UnAnalogTimes = (from a in onlines
        //                            join b in alarms
        //                            on a.Time equals b.Time
        //                            join c in faults
        //                            on b.Time equals c.Time
        //                            select new UnAnalogTime()
        //                            {
        //                                Time = a.Time,
        //                                OfflineCount = a.Count,
        //                                AlarmCount = b.Count,
        //                                FaultCount = c.Count
        //                            }).ToList();
        //    return output;
        //}
        /// <summary>
        /// 获取电气火灾监测单个项目的模拟量趋势
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetRecordElectricOutput> GetRecordElectric(GetRecordElectricInput input)
        {
            DateTime end = DateTime.Now;
            if (input.End != null && input.End.Ticks != 0)
            {
                end = input.End;
            }
            DateTime start = end.Date.AddDays(-1);
            if (input.Start != null && input.Start.Ticks != 0)
            {
                start = input.Start;
            }

            var fireElectricDevice = await _repFireElectricDevice.GetAsync(input.DeviceId);
            var records = _repFireElectricRecord.GetAll().Where(item => item.FireElectricDeviceId.Equals(input.DeviceId) && item.Sign.Equals(input.Sign) && item.CreationTime >= start && item.CreationTime <= end);

            var output = new GetRecordElectricOutput();
            if (input.Sign.Equals("A"))
            {
                output.MonitorItemName = "剩余电流";
                output.Unit = "mA";
                output.Min = fireElectricDevice.MinAmpere;
                output.Max = fireElectricDevice.MaxAmpere;
            }
            else
            {
                output.MonitorItemName = "电缆温度";
                output.Unit = "℃";
                switch (input.Sign)
                {
                    case "N":
                        output.Min = fireElectricDevice.MinN;
                        output.Max = fireElectricDevice.MaxN;
                        break;
                    case "L":
                        output.Min = fireElectricDevice.MinL;
                        output.Max = fireElectricDevice.MaxL;
                        break;
                    case "L1":
                        output.Min = fireElectricDevice.MinL1;
                        output.Max = fireElectricDevice.MaxL1;
                        break;
                    case "L2":
                        output.Min = fireElectricDevice.MinL2;
                        output.Max = fireElectricDevice.MaxL2;
                        break;
                    case "L3":
                        output.Min = fireElectricDevice.MinL3;
                        output.Max = fireElectricDevice.MaxL3;
                        break;
                }
            }
            output.lstAnalogData = records.OrderByDescending(item => item.CreationTime)
                .Select(item => new AnalogData()
                {
                    Time = item.CreationTime,
                    Value = item.Analog
                }).ToList();

            return output;
        }
        /// <summary>
        /// 模拟量探测器历史记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //public async Task<RecordAnalogOutput> GetRecordAnalog(GetRecordDetectorInput input)
        //{
        //    var output = new RecordAnalogOutput();
        //    var detector = await _detectorRep.SingleAsync(p => p.Id == input.DetectorId);
        //    var detectorType = await _detectorTypeRep.SingleAsync(p => p.Id == detector.DetectorTypeId);
        //    output.Name = detectorType.Name;
        //    output.Location = detector.Location;
        //    var state = _recordOnlineRep.GetAll().Where(p => p.DetectorId == input.DetectorId).OrderByDescending(p => p.CreationTime).FirstOrDefault();
        //    if (state != null)
        //    {
        //        output.State = GatewayStatusNames.GetName((GatewayStatus)state.State);
        //        output.LastTimeStateChange = state.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
        //    }
        //    //output.AnalogTimes = _recordAnalogRep.GetAll().Where(p => p.DetectorId == input.DetectorId).OrderByDescending(p => p.CreationTime).Take(10).Select(p =>
        //    //       new AnalogTime() { Time = p.CreationTime.ToString("HH:mm:ss"), Value = p.Analog }).ToList();
        //    output.AnalogTimes = _recordAnalogRep.GetAll().Where(p => p.DetectorId == input.DetectorId).OrderByDescending(p => p.CreationTime).Select(p =>
        //           new AnalogTime() { Time = p.CreationTime.ToString("HH:mm:ss"), Value = p.Analog }).ToList();
        //    return output;
        //}
        /// <summary>
        /// 获取防火单位的终端状态
        /// </summary>
        /// <param name="fireUnitId"></param>
        /// <returns></returns>
        //public async Task<PagedResultDeviceDto<EndDeviceStateOutput>> GetFireUnitEndDeviceState(int fireUnitId, int option, PagedResultRequestDto dto)
        //{
        //    var uitd = (from a in _detectorRep.GetAll().Where(p => p.FireUnitId == fireUnitId && p.DetectorTypeId == GetDetectorType((byte)UnitType.UITD).Id
        //               && (option == 0 ? true : (option == -1 ? p.State.Equals("离线") : !p.State.Equals("离线"))))
        //                join b in _detectorTypeRep.GetAll()
        //                on a.DetectorTypeId equals b.Id
        //                select new EndDeviceStateOutput()
        //                {
        //                    DetectorId = a.Id,
        //                    IsAnalog = false,
        //                    Name = b.Name,
        //                    Location = a.Location,
        //                    StateName = a.State,
        //                    Analog = "-",
        //                    Standard = "-"
        //                }).ToList();
        //    var tem = (from a in _detectorRep.GetAll().Where(p => p.FireUnitId == fireUnitId && p.DetectorTypeId == GetDetectorType((byte)UnitType.ElectricTemperature).Id
        //                 && (option == 0 ? true : (option == -1 ? p.State.Equals("离线") : !p.State.Equals("离线"))))
        //               join b in _detectorTypeRep.GetAll()
        //                on a.DetectorTypeId equals b.Id
        //               select new EndDeviceStateOutput()
        //               {
        //                   IsAnalog = true,
        //                   DetectorId = a.Id,
        //                   Name = b.Name,
        //                   Location = a.Location,
        //                   StateName = a.State.Equals("离线") ? "离线" : "在线",
        //                   Analog = a.State.Equals("离线") ? "-" : a.State
        //               }).ToList();
        //    var setTem = await _fireSettingManager.GetByName("CableTemperature");
        //    foreach (var v in tem)
        //    {
        //        v.Standard = $"<={setTem.MaxValue}℃";
        //        double an;
        //        v.IsOverRange = double.TryParse(v.Analog, out an) ? an > setTem.MaxValue : false;
        //    }
        //    var ele = (from a in _detectorRep.GetAll().Where(p => p.FireUnitId == fireUnitId && p.DetectorTypeId == GetDetectorType((byte)UnitType.ElectricResidual).Id
        //               && (option == 0 ? true : (option == -1 ? p.State.Equals("离线") : !p.State.Equals("离线"))))
        //               join b in _detectorTypeRep.GetAll()
        //               on a.DetectorTypeId equals b.Id
        //               select new EndDeviceStateOutput()
        //               {
        //                   IsAnalog = true,
        //                   DetectorId = a.Id,
        //                   Name = b.Name,
        //                   Location = a.Location,
        //                   StateName = a.State.Equals("离线") ? "离线" : "在线",
        //                   Analog = a.State.Equals("离线") ? "-" : a.State
        //               }).ToList();
        //    var setEle = await _fireSettingManager.GetByName("ResidualCurrent");
        //    foreach (var v in ele)
        //    {
        //        v.Standard = $"<={setEle.MaxValue}mA";
        //        double an;
        //        v.IsOverRange = double.TryParse(v.Analog, out an) ? an > setEle.MaxValue : false;
        //    }
        //    var lst = uitd.Union(tem).Union(ele).ToList();
        //    return new PagedResultDeviceDto<EndDeviceStateOutput>()
        //    {
        //        OfflineCount = lst.Count(p => string.IsNullOrEmpty(p.StateName) || p.StateName.Equals("离线")),
        //        TotalCount = lst.Count,
        //        Items = lst.Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList()
        //    };
        //}

        /// <summary>
        /// 新增探测器部件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //public async Task AddDetector(AddDetectorInput input)
        //{
        //    var gateway = _gatewayRep.GetAll().Where(p => p.Identify == input.GatewayIdentify).FirstOrDefault();
        //    Valid.Exception(gateway == null, $"网关地址{input.GatewayIdentify}不存在");

        //    var type = _detectorTypeRep.GetAll().Where(p => p.GBType == input.DetectorGBType).FirstOrDefault();
        //    if (type == null)
        //        return;
        //    await _detectorRep.InsertAsync(new Detector()
        //    {
        //        DetectorTypeId = type.Id,
        //        FireSysType = gateway.FireSysType,
        //        Identify = input.Identify,
        //        Location = input.Location,
        //        GatewayId = gateway.Id,
        //        FireUnitId = gateway.FireUnitId,
        //        Origin = input.Origin
        //    });
        //}

        /// <summary>
        /// 导入火警联网网关对应消防主机的联网部件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> ImportFireAlarmDetector(FireAlarmDetectorImportDto input)
        {
            SuccessOutput output = new SuccessOutput();
            IFormFile file = input.file;
            if (file.Length > 0)
            {
                DataTable dt = new DataTable();
                string strMsg;
                dt = ExcelHelper.ExcelToDatatable(file.OpenReadStream(), Path.GetExtension(file.FileName), out strMsg);
                if (!string.IsNullOrEmpty(strMsg))
                {
                    output.Success = false;
                    output.FailCause = strMsg;
                    return output;
                }
                if (dt.Rows.Count > 0)
                {
                    if (dt.Columns.Count != 4 || dt.Columns[0].ColumnName != "部件地址" || dt.Columns[1].ColumnName != "部件类型" || dt.Columns[2].ColumnName != "楼层"
                        || dt.Columns[3].ColumnName != "安装位置")
                    {
                        output.Success = false;
                        output.FailCause = "表格的字段不正确，请使用数据导入表格模板";
                        return output;
                    }

                    var fireAlarmDevice = await _repFireAlarmDevice.FirstOrDefaultAsync(p => p.DeviceSn.Equals(input.FireAlarmDeviceSn));
                    if (fireAlarmDevice == null)
                    {
                        output.Success = false;
                        output.FailCause = $"火警联网设施编号（{input.FireAlarmDeviceSn}）错误，未找到对应的火警联网设施";
                        return output;
                    }
                    // 火警联网部件类型
                    List<string> lstDeteType = new List<string>()
                    {
                        "火灾报警控制器","感烟式火灾探测器","感温式火灾探测器",
                        "感光式火灾探测器","可燃气体火灾探测器","复合式火灾探测器","手动火灾报警按钮","其它"
                    };

                    // 获得楼层数据
                    List<FireUnitArchitectureFloor> lstFireUnitArchitectureFloors = await _repFireUnitArchitectureFloor.GetAllListAsync(d => d.ArchitectureId.Equals(fireAlarmDevice.FireUnitArchitectureId));

                    List<string> lstDeviceNo = new List<string>();
                    // 验证表格数据的正确性
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow row = dt.Rows[i];
                        if (string.IsNullOrEmpty(row["部件地址"].ToString()))
                        {
                            strMsg = $"第{i + 1}行部件地址不能为空";
                            break;
                        }
                        else if (lstDeviceNo.Find(d => row["部件地址"].ToString().Trim().Equals(d)) != null)
                        {
                            strMsg = $"第{i + 1}行部件地址存在重复";
                            break;
                        }
                        else if (!string.IsNullOrEmpty(row["部件类型"].ToString()) && lstDeteType.Find(d => d.Equals(row["部件类型"].ToString().Trim())) == null)
                        {
                            strMsg = $"第{i + 1}行部件类型在后台数据中不存在";
                            break;
                        }
                        else if (!string.IsNullOrEmpty(row["楼层"].ToString()) && lstFireUnitArchitectureFloors.Find(d => d.Name.Equals(row["楼层"].ToString().Trim())) == null)
                        {
                            strMsg = $"第{i + 1}行楼层在后台数据中不存在";
                            break;
                        }

                        lstDeviceNo.Add(row["部件地址"].ToString().Trim());
                    }
                    if (!string.IsNullOrEmpty(strMsg))
                    {
                        output.Success = false;
                        output.FailCause = strMsg;
                        return output;
                    }

                    await _repFireAlarmDetector.DeleteAsync(d => fireAlarmDevice.Id.Equals(d.FireAlarmDeviceId));

                    List<DetectorType> lstDetectorType = await _detectorTypeRep.GetAllListAsync();
                    string architectureName = fireAlarmDevice.FireUnitArchitectureId > 0 ? _repFireUnitArchitecture.Get(fireAlarmDevice.FireUnitArchitectureId).Name : "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow row = dt.Rows[i];
                        await _repFireAlarmDetector.InsertAsync(new FireAlarmDetector()
                        {
                            DetectorTypeId = !string.IsNullOrEmpty(row["部件类型"].ToString()) ? lstDetectorType.Find(d => row["部件类型"].ToString().Trim().Equals(d.Name)).Id : 0,
                            Location = row["安装位置"].ToString().Trim(),
                            FaultNum = 0,
                            FireUnitArchitectureFloorId = !string.IsNullOrEmpty(row["楼层"].ToString()) ? lstFireUnitArchitectureFloors.Find(d => row["楼层"].ToString().Trim().Equals(d.Name)).Id : 0,
                            FireUnitId = fireAlarmDevice.FireUnitId,
                            Identify = row["部件地址"].ToString().Trim(),
                            FireAlarmDeviceId = fireAlarmDevice.Id,
                            State = FireAlarmDetectorState.Normal,
                            FullLocation = architectureName + row["楼层"].ToString().Trim() + row["安装位置"].ToString().Trim()
                        });
                    }
                }
                output.Success = true;
            }
            else
            {
                output.Success = false;
                output.FailCause = "未读取到指定文件的数据";
                return output;
            }
            return output;
        }

        /// <summary>
        /// 新增火警联网设施
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddFireAlarmDevice(FireAlarmDeviceDto input)
        {
            Valid.Exception(_repFireAlarmDevice.Count(item => item.DeviceSn.Equals(input.DeviceSn)) > 0, "火警联网设施编号已存在");

            await _repFireAlarmDevice.InsertAsync(new FireAlarmDevice()
            {
                Brand = input.Brand,
                DeviceSn = input.DeviceSn,
                DeviceModel = input.DeviceModel,
                EnableAlarmCloud = input.EnableAlarm.Contains("云端报警"),
                EnableAlarmSwitch = input.EnableAlarm.Contains("发送开关量信号"),
                EnableFaultCloud = input.EnableFault.Contains("云端报警"),
                EnableFaultSwitch = input.EnableFault.Contains("发送开关量信号"),
                FireUnitArchitectureId = input.FireUnitArchitectureId,
                FireUnitId = input.FireUnitId,
                NetDetectorNum = input.NetDetectorNum,
                Protocol = input.Protocol,
                NetComm = input.NetComm,
                State = GatewayStatus.Offline
            });

        }
        /// <summary>
        /// 修改电气火灾设施
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateFireElectricDevice(UpdateFireElectricDeviceDto input)
        {
            var elec = await _repFireElectricDevice.GetAsync(input.DeviceId);

            elec.NetComm = input.NetComm;
            elec.DeviceSn = input.DeviceSn;
            elec.DeviceModel = input.DeviceModel;
            elec.FireUnitArchitectureId = input.FireUnitArchitectureId;
            elec.EnableCloudAlarm = input.EnableAlarm.Contains("云端报警");
            elec.EnableEndAlarm = input.EnableAlarm.Contains("终端报警");
            elec.EnableAlarmSwitch = input.EnableAlarm.Contains("发送开关量信号");
            elec.ExistAmpere = input.MonitorItem.Contains("剩余电流");
            elec.ExistTemperature = input.MonitorItem.Contains("电缆温度");
            elec.FireUnitArchitectureFloorId = input.FireUnitArchitectureFloorId;
            elec.Location = input.Location;
            elec.PhaseType = input.PhaseType;
            elec.MinAmpere = input.Amin;
            elec.MaxAmpere = input.Amax;
            elec.MinL = input.Lmin;
            elec.MaxL = input.Lmax;
            elec.MinN = input.Nmin;
            elec.MaxN = input.Nmax;
            elec.MinL1 = input.L1min;
            elec.MaxL1 = input.L1max;
            elec.MinL2 = input.L2min;
            elec.MaxL2 = input.L2max;
            elec.MinL3 = input.L3min;
            elec.MaxL3 = input.L3max;

            await _repFireElectricDevice.UpdateAsync(elec);
        }
        /// <summary>
        /// 新增电气火灾设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddFireElectricDevice(FireElectricDeviceDto input)
        {
            Valid.Exception(_repFireElectricDevice.Count(item => item.DeviceSn.Equals(input.DeviceSn)) > 0, $"设备编号{input.DeviceSn}已存在");

            await _repFireElectricDevice.InsertAndGetIdAsync(new FireElectricDevice()
            {
                State = FireElectricDeviceState.Offline,
                DeviceSn = input.DeviceSn,
                DeviceModel = input.DeviceModel,
                FireUnitArchitectureId = input.FireUnitArchitectureId,
                FireUnitId = input.FireUnitId,
                EnableCloudAlarm = input.EnableAlarm.Contains("云端报警"),
                EnableEndAlarm = input.EnableAlarm.Contains("终端报警"),
                EnableAlarmSwitch = input.EnableAlarm.Contains("发送开关量信号"),
                ExistAmpere = input.MonitorItem.Contains("剩余电流"),
                ExistTemperature = input.MonitorItem.Contains("电缆温度"),
                FireUnitArchitectureFloorId = input.FireUnitArchitectureFloorId,
                Location = input.Location,
                PhaseType = input.PhaseType,
                NetComm = input.NetComm,
                DataRate = "2小时",
                MinAmpere = input.Amin,
                MaxAmpere = input.Amax,
                MinL = input.Lmin,
                MaxL = input.Lmax,
                MinL1 = input.L1min,
                MaxL1 = input.L1max,
                MinL2 = input.L2min,
                MaxL2 = input.L2max,
                MinL3 = input.L3min,
                MaxL3 = input.L3max,
                MinN = input.Nmin,
                MaxN = input.Nmax
            });
        }
        /// <summary>
        /// 修改其它消防设施
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> UpdateFireOrtherDevice(UpdateFireOrtherDeviceDto input)
        {
            var device = await _repFireOrtherDevice.FirstOrDefaultAsync(p => p.Id == input.DeviceId);
            if (device == null)
                return new SuccessOutput() { Success = false, FailCause = $"不存在ID为{input.DeviceId}的设备" };
            var arch = await _repFireUnitArchitecture.FirstOrDefaultAsync(p => p.Id == input.FireUnitArchitectureId);
            if (arch == null)
                return new SuccessOutput() { Success = false, FailCause = "建筑不存在" };
            try
            {

                device.DeviceSn = input.DeviceSn;
                device.DeviceType = input.DeviceType;
                device.FireUnitArchitectureId = input.FireUnitArchitectureId;
                device.FireUnitArchitectureFloorId = input.FireUnitArchitectureFloorId;
                device.Location = input.Location;
                device.DeviceName = input.DeviceName;
                device.FireSystemId = input.FireSystemId;
                device.StartTime = DateTime.Parse(input.StartTime);
                device.ExpireTime = DateTime.Parse(input.ExpireTime);
                await _repFireOrtherDevice.UpdateAsync(device);
            }
            catch (Exception e)
            {
                return new SuccessOutput() { Success = false, FailCause = e.Message };
            }
            return new SuccessOutput() { Success = true };
        }
        /// <summary>
        /// 通过Id获取其它消防设施
        /// </summary>
        /// <param name="deviceid"></param>
        /// <returns></returns>
        public async Task<GetFireOrtherDeviceOutput> GetFireOrtherDevice(int deviceid)
        {
            var device = await _repFireOrtherDevice.FirstOrDefaultAsync(p => p.Id == deviceid);
            var arch = await _repFireUnitArchitecture.FirstOrDefaultAsync(p => p.Id == device.FireUnitArchitectureId);
            var floor = await _repFireUnitArchitectureFloor.FirstOrDefaultAsync(p => p.Id == device.FireUnitArchitectureFloorId);
            return new GetFireOrtherDeviceOutput()
            {
                DeviceId = device.Id,
                DeviceSn = device.DeviceSn,
                StartTime = device.StartTime.ToString("yyyy-MM-dd"),
                ExpireTime = device.ExpireTime.ToString("yyyy-MM-dd"),
                FireSystemId = device.FireSystemId,
                DeviceName = device.DeviceName,
                DeviceType = device.DeviceType,
                FireUnitArchitectureFloorId = device.FireUnitArchitectureFloorId,
                FireUnitArchitectureId = device.FireUnitArchitectureId,
                FireUnitId = device.FireUnitId,
                Location = device.Location,
                FireUnitArchitectureFloorName = floor == null ? "" : floor.Name,
                FireUnitArchitectureName = arch == null ? "" : arch.Name
            };
        }
        /// <summary>
        /// 获取防火单位的其它消防设施的即将过期数量和已过期数量
        /// </summary>
        /// <param name="fireUnitId"></param>
        /// <returns></returns>
        public Task<GetFireOrtherDeviceExpireOutput> GetFireOrtherDeviceExpire(int fireUnitId)
        {
            var devices = _repFireOrtherDevice.GetAll().Where(p => p.FireUnitId == fireUnitId);
            return Task.FromResult(new GetFireOrtherDeviceExpireOutput()
            {
                ExpireNum = devices.Where(p => DateTime.Now >= p.ExpireTime).Count(),
                WillExpireNum = devices.Where(p => DateTime.Now >= p.ExpireTime.AddDays(-30) && DateTime.Now < p.ExpireTime).Count()
            });
        }
        /// <summary>
        /// 获取其它消防设施列表
        /// </summary>
        /// <param name="fireUnitId"></param>
        /// <param name="ExpireType"></param>
        /// <param name="FireUnitArchitectureName"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<FireOrtherDeviceItemDto>> GetFireOrtherDeviceList(int fireUnitId, string ExpireType, string FireUnitArchitectureName, PagedResultRequestDto dto)
        {
            return await Task.Run<PagedResultDto<FireOrtherDeviceItemDto>>(() =>
            {
                var query = from a in _repFireOrtherDevice.GetAll().Where(p => p.FireUnitId == fireUnitId)
                            join b0 in _repFireUnitArchitecture.GetAll() on a.FireUnitArchitectureId equals b0.Id into be
                            from b in be.DefaultIfEmpty()
                            join c0 in _repFireUnitArchitectureFloor.GetAll() on a.FireUnitArchitectureFloorId equals c0.Id into ce
                            from c in ce.DefaultIfEmpty()
                            select new
                            {
                                DeviceId = a.Id,
                                DeviceSn = a.DeviceSn,
                                DeviceName = a.DeviceName,
                                FireUnitArchitectureName = b == null ? "" : b.Name,
                                FireUnitArchitectureFloorName = c == null ? "" : c.Name,
                                ExpireTime = a.ExpireTime,//.ToString("yyyy-MM-dd"),
                                IsNet = "否",
                                Location = a.Location
                            };
                if (!string.IsNullOrEmpty(FireUnitArchitectureName))
                    query = query.Where(p => p.FireUnitArchitectureName.Equals(FireUnitArchitectureName));
                if (!string.IsNullOrEmpty(ExpireType))
                {
                    if (ExpireType.Equals("即将过期"))
                        query = query.Where(p => DateTime.Now >= p.ExpireTime.AddDays(-30) && DateTime.Now < p.ExpireTime);
                    else if (ExpireType.Equals("已过期"))
                        query = query.Where(p => DateTime.Now >= p.ExpireTime);
                }
                var q = query.Select(p => new FireOrtherDeviceItemDto()
                {
                    DeviceSn = p.DeviceSn,
                    DeviceId = p.DeviceId,
                    DeviceName = p.DeviceName,
                    ExpireTime = p.ExpireTime.ToString("yyyy-MM-dd"),
                    FireUnitArchitectureFloorName = p.FireUnitArchitectureFloorName,
                    FireUnitArchitectureName = p.FireUnitArchitectureName,
                    IsNet = p.IsNet,
                    Location = p.Location
                });
                return new PagedResultDto<FireOrtherDeviceItemDto>()
                {
                    TotalCount = q.Count(),
                    Items = q.Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList()
                };
            });
        }
        /// <summary>
        /// 新增其他消防设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> AddFireOrtherDevice(FireOrtherDeviceDto input)
        {
            var arch = await _repFireUnitArchitecture.FirstOrDefaultAsync(p => p.Id == input.FireUnitArchitectureId);
            if (arch == null)
                return new SuccessOutput() { Success = false, FailCause = "建筑不存在" };
            var device = await _repFireOrtherDevice.FirstOrDefaultAsync(p => p.DeviceSn.Equals(input.DeviceSn));
            if (device != null)
                return new SuccessOutput() { Success = false, FailCause = "设备编号已存在" };
            try
            {
                await _repFireOrtherDevice.InsertAsync(new FireOrtherDevice()
                {
                    DeviceSn = input.DeviceSn,
                    DeviceType = input.DeviceType,
                    FireUnitArchitectureId = input.FireUnitArchitectureId,
                    FireUnitId = input.FireUnitId,
                    FireUnitArchitectureFloorId = input.FireUnitArchitectureFloorId,
                    Location = input.Location,
                    DeviceName = input.DeviceName,
                    FireSystemId = input.FireSystemId,
                    StartTime = DateTime.Parse(input.StartTime),
                    ExpireTime = DateTime.Parse(input.ExpireTime)
                });
            }
            catch (Exception e)
            {
                return new SuccessOutput() { Success = false, FailCause = e.Message };
            }
            return new SuccessOutput() { Success = true };
        }

        /// <summary>
        /// 导入其它消防设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> ImportOrtherDevice(FireOtherDeviceImportDto input)
        {
            SuccessOutput output = new SuccessOutput();
            IFormFile file = input.file;
            if (file.Length > 0)
            {
                DataTable dt = new DataTable();
                string strMsg;
                dt = ExcelHelper.ExcelToDatatable(file.OpenReadStream(), Path.GetExtension(file.FileName), out strMsg);
                if (!string.IsNullOrEmpty(strMsg))
                {
                    output.Success = false;
                    output.FailCause = strMsg;
                    return output;
                }
                if (dt.Rows.Count > 0)
                {
                    if (dt.Columns.Count != 9 || dt.Columns[0].ColumnName != "设备编号" || dt.Columns[1].ColumnName != "设备名称" || dt.Columns[2].ColumnName != "设备型号"
                        || dt.Columns[3].ColumnName != "所属系统" || dt.Columns[4].ColumnName != "所在建筑" || dt.Columns[5].ColumnName != "楼层"
                        || dt.Columns[6].ColumnName != "具体位置" || dt.Columns[7].ColumnName != "启用日期" || dt.Columns[8].ColumnName != "有效期")
                    {
                        output.Success = false;
                        output.FailCause = "表格的字段不正确，请使用数据导入表格模板";
                        return output;
                    }
                    // 获得当前防火单位的消防系统
                    var fireUnitSystemlist = await _fireUnitSystemRep.GetAllListAsync(u => u.FireUnitId == input.FireUnitId);
                    var fireUnitSystems = from a in fireUnitSystemlist
                                          join b in _fireSystemRep.GetAll() on a.FireSystemId equals b.Id
                                          select new GetPatrolFireUnitSystemOutput
                                          {
                                              FireSystemId = a.FireSystemId,
                                              SystemName = b.SystemName,
                                          };
                    List<GetPatrolFireUnitSystemOutput> lstFireUnitSystem = fireUnitSystems.ToList();

                    // 获得当前防火单位的建筑及楼层数据
                    var FireUnitArchitectures = _repFireUnitArchitecture.GetAll();
                    var FireUnitArchitectureFloors = _repFireUnitArchitectureFloor.GetAll();

                    var expr = ExprExtension.True<FireUnitArchitecture>().And(item => item.FireUnitId.Equals(input.FireUnitId));
                    FireUnitArchitectures = FireUnitArchitectures.Where(expr);

                    var query = from a in FireUnitArchitectures
                                select new GetFireUnitArchitectureOutput
                                {
                                    Id = a.Id,
                                    Name = a.Name,
                                    Floors = FireUnitArchitectureFloors.Where(item => item.ArchitectureId.Equals(a.Id)).ToList()
                                };
                    List<GetFireUnitArchitectureOutput> lstFireUnitArchitecture = query.ToList();

                    List<string> lstDeviceNo = new List<string>();
                    // 验证表格数据的正确性
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow row = dt.Rows[i];
                        if (string.IsNullOrEmpty(row["设备编号"].ToString()))
                        {
                            strMsg = $"第{i + 1}行设备编号不能为空";
                            break;
                        }
                        else if (lstDeviceNo.Find(d => row["设备编号"].ToString().Trim().Equals(d)) != null)
                        {
                            strMsg = $"第{i + 1}行设备编号存在重复";
                            break;
                        }
                        else if (string.IsNullOrEmpty(row["设备名称"].ToString()))
                        {
                            strMsg = $"第{i + 1}行设备名称不能为空";
                            break;
                        }
                        else if (!string.IsNullOrEmpty(row["所属系统"].ToString()) && lstFireUnitSystem.Find(d => d.SystemName.Equals(row["所属系统"].ToString().Trim())) == null)
                        {
                            strMsg = $"第{i + 1}行所属系统在后台数据中不存在";
                            break;
                        }
                        else if (!string.IsNullOrEmpty(row["所在建筑"].ToString()) && lstFireUnitArchitecture.Find(d => d.Name.Equals(row["所在建筑"].ToString().Trim())) == null)
                        {
                            strMsg = $"第{i + 1}行所在建筑在后台数据中不存在";
                            break;
                        }
                        else if (!string.IsNullOrEmpty(row["所在建筑"].ToString()) && !string.IsNullOrEmpty(row["楼层"].ToString()) && lstFireUnitArchitecture.Find(d => d.Name.Equals(row["所在建筑"].ToString().Trim())).Floors.Find(f => f.Name.Equals(row["楼层"].ToString().Trim())) == null)
                        {
                            strMsg = $"第{i + 1}行楼层在后台数据中不存在";
                            break;
                        }
                        else if (!string.IsNullOrEmpty(row["启用日期"].ToString()) && !DateTime.TryParse(row["启用日期"].ToString().Trim(), out DateTime startDate))
                        {
                            strMsg = $"第{i + 1}行启用日期必须为合法的日期";
                            break;
                        }
                        else if (!string.IsNullOrEmpty(row["有效期"].ToString()) && !DateTime.TryParse(row["有效期"].ToString().Trim(), out DateTime validDate))
                        {
                            strMsg = $"第{i + 1}行有效期必须为合法的日期";
                            break;
                        }
                        lstDeviceNo.Add(row["设备编号"].ToString().Trim());
                    }
                    if (!string.IsNullOrEmpty(strMsg))
                    {
                        output.Success = false;
                        output.FailCause = strMsg;
                        return output;
                    }

                    await _repFireOrtherDevice.DeleteAsync(d => input.FireUnitId.Equals(d.FireUnitId));

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow row = dt.Rows[i];
                        await _repFireOrtherDevice.InsertAsync(new FireOrtherDevice()
                        {
                            FireUnitId = input.FireUnitId,
                            DeviceSn = row["设备编号"].ToString().Trim(),
                            DeviceType = row["设备型号"].ToString().Trim(),
                            FireUnitArchitectureId = !string.IsNullOrEmpty(row["所在建筑"].ToString().Trim()) ? lstFireUnitArchitecture.Find(d => d.Name.Equals(row["所在建筑"].ToString().Trim())).Id : 0,
                            FireUnitArchitectureFloorId = !string.IsNullOrEmpty(row["所在建筑"].ToString().Trim()) && !string.IsNullOrEmpty(row["楼层"].ToString().Trim()) ? lstFireUnitArchitecture.Find(d => d.Name.Equals(row["所在建筑"].ToString().Trim())).Floors.Find(f => f.Name.Equals(row["楼层"].ToString().Trim())).Id : 0,
                            Location = row["具体位置"].ToString().Trim(),
                            DeviceName = row["设备名称"].ToString().Trim(),
                            FireSystemId = !string.IsNullOrEmpty(row["所属系统"].ToString().Trim()) ? lstFireUnitSystem.Find(d => d.SystemName.Equals(row["所属系统"].ToString().Trim())).FireSystemId : 0,
                            StartTime = DateTime.Parse(row["启用日期"].ToString().Trim()),
                            ExpireTime = DateTime.Parse(row["有效期"].ToString().Trim())
                        });
                    }
                }
                output.Success = true;
            }
            else
            {
                output.Success = false;
                output.FailCause = "未读取到指定文件的数据";
                return output;
            }
            return output;
        }

        /// <summary>
        /// 新增网关设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //public async Task AddGateway(AddGatewayInput input)
        //{
        //    await _gatewayRep.InsertAsync(new Gateway()
        //    {
        //        FireSysType = input.FireSysType,
        //        Identify = input.Identify,
        //        Location = input.Location,
        //        FireUnitId = input.FireUnitId,
        //        Origin = input.Origin
        //    });
        //}

        /// <summary>
        /// 根据国际数值获取对应的部件类型
        /// </summary>
        /// <param name="GBtype"></param>
        /// <returns></returns>
        public DetectorType GetDetectorType(byte GBtype)
        {
            return _detectorTypeRep.GetAll().Where(p => p.GBType == GBtype).FirstOrDefault();
        }
        /// <summary>
        /// 根据Id获取单个火警联网部件详情
        /// </summary>
        /// <param name="detectorId"></param>
        /// <returns></returns>
        public async Task<FireAlarmDetector> GetDetectorById(int detectorId)
        {
            return await _repFireAlarmDetector.GetAsync(detectorId);
        }
        //public Detector GetDetector(string identify, string origin)
        //{
        //    return _detectorRep.GetAll().Where(p => p.Identify.Equals(identify) && p.Origin.Equals(origin)).FirstOrDefault();
        //}
        //public Gateway GetGateway(string gatewayIdentify, string origin)
        //{
        //    return _gatewayRep.GetAll().Where(p => p.Identify.Equals(gatewayIdentify) && p.Origin.Equals(origin)).FirstOrDefault();
        //}
        //public IQueryable<Detector> GetDetectorAll(int fireunitid, FireSysType fireSysType)
        //{
        //    return _detectorRep.GetAll().Where(p => p.FireSysType == (byte)fireSysType && p.FireUnitId == fireunitid);
        //}
        //public IQueryable<Detector> GetDetectorElectricAll()
        //{
        //    return _detectorRep.GetAll().Where(p => p.FireSysType == (byte)FireSysType.Electric);
        //}
        /// <summary>
        /// 添加电气火灾监测数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddElecRecord(AddDataElecInput input)
        {
            var fireElectricDevice = await _repFireElectricDevice.FirstOrDefaultAsync(item => item.DeviceSn.Equals(input.FireElectricDeviceSn));
            Valid.Exception(fireElectricDevice == null, $"未找到编号为{input.FireElectricDeviceSn}的电气火灾设备");
            Valid.Exception(input.Sign != "A" || input.Sign != "N" || input.Sign != "L" || input.Sign != "L1" || input.Sign != "L2" || input.Sign != "L3", "记录的类型标记错误");

            await _repFireElectricRecord.InsertAsync(new FireElectricRecord()
            {
                FireUnitId = fireElectricDevice.FireUnitId,
                FireElectricDeviceId = fireElectricDevice.Id,
                Sign = input.Sign,
                Analog = input.Analog
            });

            int max = 0;
            switch (input.Sign)
            {
                case "A":
                    max = fireElectricDevice.MaxAmpere;
                    break;
                case "N":
                    max = fireElectricDevice.MaxN;
                    break;
                case "L":
                    max = fireElectricDevice.MaxL;
                    break;
                case "L1":
                    max = fireElectricDevice.MaxL1;
                    break;
                case "L2":
                    max = fireElectricDevice.MaxL2;
                    break;
                case "L3":
                    max = fireElectricDevice.MaxL3;
                    break;
            }

            FireElectricDeviceState nowState = FireElectricDeviceState.Good;
            if (max > 0 && input.Analog > 0)
            {
                if (input.Analog > max) nowState = FireElectricDeviceState.Transfinite;
                else if (input.Analog >= max * 0.8) nowState = FireElectricDeviceState.Danger;
            }
            // 如果状态发生了变化，则修改电气火灾设施的状态
            if (fireElectricDevice.State != nowState)
            {
                fireElectricDevice.State = nowState;
                await _repFireElectricDevice.UpdateAsync(fireElectricDevice);
            }
            // 如果超限，则向AlarmToElectric表中插入一条数据
            if (nowState.Equals(FireElectricDeviceState.Transfinite))
            {
                await _repAlarmToElectric.InsertAsync(new AlarmToElectric()
                {
                    FireElectricDeviceId = fireElectricDevice.Id,
                    Sign = input.Sign,
                    Analog = input.Analog,
                    FireUnitId = fireElectricDevice.FireUnitId
                });
            }
        }
        //public async Task<AddDataOutput> AddOnlineDetector(AddOnlineDetectorInput input)
        //{
        //    var detector = GetDetector(input.Identify, input.Origin);
        //    if (detector == null)
        //    {
        //        return new AddDataOutput()
        //        {
        //            IsDetectorExit = false
        //        };
        //    }
        //    await _recordOnlineRep.InsertAsync(new RecordOnline()
        //    {
        //        State = (sbyte)(input.IsOnline ? GatewayStatus.Online : GatewayStatus.Offline),
        //        DetectorId = detector.Id
        //    });
        //    return new AddDataOutput() { IsDetectorExit = true };
        //}
        //public async Task AddOnlineGateway(AddOnlineGatewayInput input)
        //{
        //    var gateway = GetGateway(input.Identify, input.Origin);
        //    var detectors = _detectorRep.GetAll().Where(p => p.GatewayId == gateway.Id);
        //    foreach (var v in detectors)
        //    {
        //        await _recordOnlineRep.InsertAsync(new RecordOnline()
        //        {
        //            State = (sbyte)(input.IsOnline ? GatewayStatus.Online : GatewayStatus.Offline),
        //            DetectorId = v.Id
        //        });
        //        v.State = input.IsOnline ? "在线" : "离线";
        //        await _detectorRep.UpdateAsync(v);
        //    }
        //}
        /// <summary>
        /// 获取火警联网设备型号数组
        /// </summary>
        /// <returns></returns>
        public Task<List<string>> GetFireAlarmDeviceModels()
        {
            return Task.FromResult(_repFireAlarmDeviceModel.GetAll().OrderByDescending(p => p.CreationTime).Select(item => item.Name).ToList());
        }
        /// <summary>
        /// 获取电气火灾设备型号数组
        /// </summary>
        /// <returns></returns>
        public Task<List<string>> GetFireElectricDeviceModels()
        {
            return Task.FromResult(_repFireElectricDeviceModel.GetAll().OrderByDescending(p => p.CreationTime).Select(item => item.Name).ToList());
        }
        /// <summary>
        /// 获取火灾报警控制器协议数组
        /// </summary>
        /// <returns></returns>
        public Task<List<string>> GetFireAlarmDeviceProtocols()
        {
            return Task.FromResult(_repFireAlarmDeviceProtocol.GetAll().OrderByDescending(p => p.CreationTime).Select(item => item.Name).ToList());
        }
        /// <summary>
        /// 获取火警设备串口参数
        /// </summary>
        /// <param name="deviceId">火警联网设备ID</param>
        /// <returns></returns>
        public Task<SerialPortParamDto> GetSerialPortParam(int DeviceId)
        {
            return Task.FromResult(new SerialPortParamDto()
            {
                ComType = "RS232",
                StopBit = "1",
                DataBit = "00001",
                ParityBit = 1,
                Rate = "2400"
            });
        }
        /// <summary>
        /// 添加消防管网设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddFireWaterDevice(AddFireWaterDeviceInput input)
        {
            Valid.Exception(_repFireWaterDevice.Count(m => m.DeviceAddress.Equals(input.DeviceAddress)) > 0, "设备地址已存在");

            string heightPhase = JsonConvert.SerializeObject(new HeightPhase()
            {
                MinHeight = input.MinHeight,
                MaxHeight = input.MaxHeight
            });
            string pressPhase = JsonConvert.SerializeObject(new PressPhase()
            {
                MinPress = input.MinPress,
                MaxPress = input.MaxPress
            });

            await _repFireWaterDevice.InsertAsync(new FireWaterDevice()
            {
                CreationTime = DateTime.Now,
                FireUnitId = input.FireUnitId,
                DeviceAddress = input.DeviceAddress,
                State = "离线",
                Location = input.Location,
                Gateway_Type = input.Gateway_Type,
                Gateway_Sn = input.Gateway_Sn,
                Gateway_Location = input.Gateway_Location,
                Gateway_NetComm = input.Gateway_NetComm,
                Gateway_DataRate = "2小时",
                MonitorType = input.MonitorType,
                HeightThreshold = heightPhase,
                PressThreshold = pressPhase,
                EnableCloudAlarm = input.EnableCloudAlarm
            });
        }
        /// <summary>
        /// 修改消防管网设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateFireWaterDevice(UpdateFireWaterDeviceInput input)
        {
            Valid.Exception(_repFireWaterDevice.Count(m => m.DeviceAddress.Equals(input.DeviceAddress) && !m.Id.Equals(input.Id)) > 0, "设备地址已存在");

            FireWaterDevice device = await _repFireWaterDevice.GetAsync(input.Id);

            string heightPhase = JsonConvert.SerializeObject(new HeightPhase()
            {
                MinHeight = input.MinHeight,
                MaxHeight = input.MaxHeight
            });
            string pressPhase = JsonConvert.SerializeObject(new PressPhase()
            {
                MinPress = input.MinPress,
                MaxPress = input.MaxPress
            });

            device.DeviceAddress = input.DeviceAddress;
            device.Location = input.Location;
            device.Gateway_Type = input.Gateway_Type;
            device.Gateway_Sn = input.Gateway_Sn;
            device.Gateway_Location = input.Gateway_Location;
            device.Gateway_NetComm = input.Gateway_NetComm;
            device.MonitorType = input.MonitorType;
            device.HeightThreshold = heightPhase;
            device.PressThreshold = pressPhase;
            device.EnableCloudAlarm = input.EnableCloudAlarm;

            await _repFireWaterDevice.UpdateAsync(device);
        }

        /// <summary>
        /// 删除消防管网设备
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public async Task DeleteFireWaterDevice(int deviceId)
        {
            await _repFireWaterDevice.DeleteAsync(deviceId);
        }

        /// <summary>
        /// 获取单个消防管网设备信息
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public async Task<UpdateFireWaterDeviceInput> GetFireWaterDeviceById(int deviceId)
        {
            var device = await _repFireWaterDevice.GetAsync(deviceId);
            HeightPhase heightPhase = JsonConvert.DeserializeObject<HeightPhase>(device.HeightThreshold);
            PressPhase pressPhase = JsonConvert.DeserializeObject<PressPhase>(device.PressThreshold);
            return new UpdateFireWaterDeviceInput()
            {
                Id = device.Id,
                DeviceAddress = device.DeviceAddress,
                FireUnitId = device.FireUnitId,
                Location = device.Location,
                State = device.State,
                EnableCloudAlarm = device.EnableCloudAlarm,
                MonitorType = device.MonitorType,
                Gateway_Sn = device.Gateway_Sn,
                Gateway_Location = device.Gateway_Location,
                Gateway_Type = device.Gateway_Type,
                Gateway_NetComm = device.Gateway_NetComm,
                Gateway_DataRate = device.Gateway_DataRate,
                MinHeight = heightPhase.MinHeight,
                MaxHeight = heightPhase.MaxHeight,
                MinPress = pressPhase.MinPress,
                MaxPress = pressPhase.MaxPress
            };
        }

        /// <summary>
        /// 获取消防管网设备列表
        /// </summary>
        /// <param name="fireUnitId"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<FireWaterDevice>> GetFireWaterDeviceList(int fireUnitId, PagedResultRequestDto dto)
        {
            var lstWaterDevices = await _repFireWaterDevice.GetAllListAsync(d => fireUnitId.Equals(d.FireUnitId));

            var tCount = lstWaterDevices.Count();

            return new PagedResultDto<FireWaterDevice>()
            {
                Items = lstWaterDevices.Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList(),
                TotalCount = lstWaterDevices.Count
            };
        }

        /// <summary>
        /// 获取消防管网联网网关设备型号列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetFireWaterDeviceTypes()
        {
            return await Task.Run(() =>
            {
                return _repFireWaterDeviceType.GetAll().OrderBy(p => p.Name).Select(p => p.Name).ToList();
            });
        }

        /// <summary>
        /// 获取固件更新列表
        /// </summary>
        /// <returns></returns>
        public Task<List<GetFirmwareUpdateListOutput>> GetFirmwareUpdateList()
        {
            return Task.FromResult(new List<GetFirmwareUpdateListOutput>());
        }
    }
}
