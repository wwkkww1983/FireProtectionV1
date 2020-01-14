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
using System.Threading;
using System.Threading.Tasks;
using TsjDeviceServer.DeviceCtrl;

namespace FireProtectionV1.FireWorking.Manager
{
    public class DeviceManager : IDeviceManager
    {
        IRepository<FireAlarmDeviceProtocol> _repFireAlarmDeviceProtocol;
        IRepository<TsjDeviceModel> _repTsjDeviceModel;
        IRepository<FireUnitArchitectureFloor> _repFireUnitArchitectureFloor;
        IRepository<FireUnitArchitecture> _repFireUnitArchitecture;
        IRepository<FireAlarmDevice> _repFireAlarmDevice;
        IRepository<FireElectricDevice> _repFireElectricDevice;
        IRepository<FireOrtherDevice> _repFireOrtherDevice;
        IRepository<FireWaterDevice> _repFireWaterDevice;
        IRepository<FireUnitSystem> _repFireUnitSystem;
        IRepository<FireUnit> _repFireUnit;
        IRepository<ShortMessageLog> _repShortMessageLog;
        IRepository<FireSystem> _repFireSystem;
        IRepository<Fault> _repFault;
        IRepository<AlarmToFire> _repAlarmToFire;
        IRepository<AlarmToElectric> _repAlarmToElectric;
        IRepository<AlarmToWater> _repAlarmToWater;
        IRepository<RecordOnline> _repRecordOnline;
        IRepository<FireElectricRecord> _repFireElectricRecord;
        IRepository<FireWaterRecord> _repFireWaterRecord;
        IFireSettingManager _fireSettingManager;
        IRepository<DetectorType> _repDetectorType;
        IRepository<FireAlarmDetector> _repFireAlarmDetector;
        public DeviceManager(
            IRepository<FireAlarmDeviceProtocol> repFireAlarmDeviceProtocol,
            IRepository<TsjDeviceModel> repTsjDeviceModel,
            IRepository<FireUnitArchitectureFloor> repFireUnitArchitectureFloor,
            IRepository<FireUnitArchitecture> repFireUnitArchitecture,
            IRepository<FireAlarmDevice> repFireAlarmDevice,
            IRepository<FireElectricDevice> repFireElectricDevice,
            IRepository<FireOrtherDevice> repFireOrtherDevice,
            IRepository<FireWaterDevice> repFireWaterDevice,
            IRepository<FireUnitSystem> repFireUnitSystem,
            IRepository<FireSystem> repFireSystem,
            IRepository<FireUnit> repFireUnit,
            IRepository<ShortMessageLog> repShortMessageLog,
            IRepository<Fault> repFault,
            IRepository<AlarmToFire> repAlarmToFire,
            IRepository<AlarmToElectric> repAlarmToElectric,
            IRepository<AlarmToWater> repAlarmToWater,
            IRepository<RecordOnline> repRecordOnline,
            IRepository<FireElectricRecord> repFireElectricRecord,
            IRepository<FireWaterRecord> repFireWaterRecord,
            IFireSettingManager fireSettingManager,
            IRepository<DetectorType> repDetectorType,
            IRepository<FireAlarmDetector> repFireAlarmDetector
            )
        {
            _repFireAlarmDeviceProtocol = repFireAlarmDeviceProtocol;
            _repTsjDeviceModel = repTsjDeviceModel;
            _repFireUnitArchitectureFloor = repFireUnitArchitectureFloor;
            _repFireUnitArchitecture = repFireUnitArchitecture;
            _repFireAlarmDevice = repFireAlarmDevice;
            _repFireElectricDevice = repFireElectricDevice;
            _repFireOrtherDevice = repFireOrtherDevice;
            _repFireWaterDevice = repFireWaterDevice;
            _repFireUnitSystem = repFireUnitSystem;
            _repFireSystem = repFireSystem;
            _repFireUnit = repFireUnit;
            _repShortMessageLog = repShortMessageLog;
            _repFault = repFault;
            _repAlarmToFire = repAlarmToFire;
            _repAlarmToElectric = repAlarmToElectric;
            _repAlarmToWater = repAlarmToWater;
            _repRecordOnline = repRecordOnline;
            _repFireElectricRecord = repFireElectricRecord;
            _repFireWaterRecord = repFireWaterRecord;
            _fireSettingManager = fireSettingManager;
            _repDetectorType = repDetectorType;
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
            var detectortype = !string.IsNullOrEmpty(input.DetectorTypeName) ? await _repDetectorType.FirstOrDefaultAsync(p => p.Name.Equals(input.DetectorTypeName)) : null;

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
            Valid.Exception(_repAlarmToFire.Count(item => item.FireAlarmDeviceId.Equals(deviceId)) > 0, "该火警联网设施存在历史火警记录，不允许删除");
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
            var detectortype = await _repDetectorType.FirstOrDefaultAsync(p => p.Name.Equals(detectorDto.DetectorTypeName));
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
                NetComms = new List<string>() { "以太网", "WIFI", "NB-IoT", "4G" },
                State = device.State,
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
        public async Task<GetFireElectricDeviceOutput> GetFireElectricDevice(int deviceId)
        {
            var device = await _repFireElectricDevice.GetAsync(deviceId);
            List<string> lstEnableAlarm = new List<string>();
            List<string> lstMonitorItem = new List<string>();
            if (device.EnableEndAlarm) lstEnableAlarm.Add("终端报警");
            if (device.EnableCloudAlarm) lstEnableAlarm.Add("云端报警");
            if (device.EnableAlarmSwitch) lstEnableAlarm.Add("自动断电");
            if (device.ExistTemperature) lstMonitorItem.Add("电缆温度");
            if (device.ExistAmpere) lstMonitorItem.Add("剩余电流");

            var output = new GetFireElectricDeviceOutput()
            {
                CreationTime = device.CreationTime,
                DataRate = device.DataRate,
                DeviceModel = device.DeviceModel,
                DeviceSn = device.DeviceSn,
                EnableAlarmSwitch = device.EnableAlarmSwitch,
                EnableCloudAlarm = device.EnableCloudAlarm,
                EnableEndAlarm = device.EnableEndAlarm,
                ExistAmpere = device.ExistAmpere,
                ExistTemperature = device.ExistTemperature,
                FireUnitArchitectureFloorId = device.FireUnitArchitectureFloorId,
                FireUnitArchitectureId = device.FireUnitArchitectureId,
                FireUnitId = device.FireUnitId,
                Id = device.Id,
                Location = device.Location,
                MaxAmpere = device.MaxAmpere,
                MaxL = device.MaxL,
                MaxL1 = device.MaxL1,
                MaxL2 = device.MaxL2,
                MaxL3 = device.MaxL3,
                MaxN = device.MaxN,
                MinAmpere = device.MinAmpere,
                MinL = device.MinL,
                MinL1 = device.MinL1,
                MinL2 = device.MinL2,
                MinL3 = device.MinL3,
                MinN = device.MinN,
                NetComm = device.NetComm,
                PhaseType = device.PhaseType,
                State = device.State,
                EnableAlarmList = lstEnableAlarm,
                MonitorItemList = lstMonitorItem
            };
            return output;
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
                BadNum = fireElectricDevice.Count(p => p.State.Equals(FireElectricDeviceState.Danger)),
                GoodNum = fireElectricDevice.Count(p => p.State.Equals(FireElectricDeviceState.Good)),
                OfflineNum = fireElectricDevice.Count(p => p.State.Equals(FireElectricDeviceState.Offline)),
                WarnNum = fireElectricDevice.Count(p => p.State.Equals(FireElectricDeviceState.Transfinite))
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
            int badNum = fireElectricDevice.Count(p => p.State.Equals(FireElectricDeviceState.Danger));
            int goodNum = fireElectricDevice.Count(p => p.State.Equals(FireElectricDeviceState.Good));
            int offlineNum = fireElectricDevice.Count(p => p.State.Equals(FireElectricDeviceState.Offline));
            int warnNum = fireElectricDevice.Count(p => p.State.Equals(FireElectricDeviceState.Transfinite));
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
            goodNum = fireWaterDevice.Count(p => p.State.Equals(FireWaterDeviceState.Good));
            warnNum = fireWaterDevice.Count(p => p.State.Equals(FireWaterDeviceState.Transfinite));
            offlineNum = fireWaterDevice.Count(p => p.State.Equals(FireWaterDeviceState.Offline));
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

            return Task.FromResult(new PagedResultDto<FireElectricDeviceItemDto>()
            {
                Items = query.OrderByDescending(item => item.CreationTime).Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList(),
                TotalCount = query.Count()
            });
        }
        /// <summary>
        /// 获取辖区内各防火单位的电气火灾设施列表
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <param name="fireUnitName"></param>
        /// <param name="state"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<PagedResultDto<FireElectricDevice_DeptDto>> GetFireElectricDeviceList_Dept(int fireDeptId, string fireUnitName, string state, PagedResultRequestDto dto)
        {
            var fireElectricDevices = _repFireElectricDevice.GetAll();
            var expr = ExprExtension.True<FireElectricDevice>();
            if (!string.IsNullOrEmpty(state))
            {
                expr = expr.IfAnd(state.Equals("在线"), item => !item.State.Equals(FireElectricDeviceState.Offline))
                .IfAnd(state.Equals("离线"), item => item.State.Equals(FireElectricDeviceState.Offline))
                .IfAnd(state.Equals("良好"), item => item.State.Equals(FireElectricDeviceState.Good))
                .IfAnd(state.Equals("隐患"), item => item.State.Equals(FireElectricDeviceState.Danger))
                .IfAnd(state.Equals("超限"), item => item.State.Equals(FireElectricDeviceState.Transfinite));
            }
            fireElectricDevices = fireElectricDevices.Where(expr);

            var fireUnits = _repFireUnit.GetAll().Where(item => item.FireDeptId.Equals(fireDeptId));
            if (!string.IsNullOrEmpty(fireUnitName))
            {
                fireUnits = fireUnits.Where(item => item.Name.Contains(fireUnitName));
            }

            var fireUnitArchitectures = _repFireUnitArchitecture.GetAll();
            var fireUnitArchitectureFloors = _repFireUnitArchitectureFloor.GetAll();
            var fireElectricRecords = _repFireElectricRecord.GetAll().OrderByDescending(item => item.CreationTime);

            var query = from a in fireElectricDevices
                        join f in fireUnits on a.FireUnitId equals f.Id
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
                        select new FireElectricDevice_DeptDto()
                        {
                            FireUnitName = f.Name,
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

            return Task.FromResult(new PagedResultDto<FireElectricDevice_DeptDto>()
            {
                Items = query.OrderByDescending(item => item.FireUnitName).Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList(),
                TotalCount = query.Count()
            });
        }
        /// <summary>
        /// 刷新某一电气火灾设备的当前数值
        /// </summary>
        /// <param name="electricDeviceId"></param>
        /// <returns></returns>
        public async Task<GetSingleElectricDeviceDataOutput> GetSingleElectricDeviceData(int electricDeviceId)
        {
            var device = await _repFireElectricDevice.GetAsync(electricDeviceId);
            DateTime nowTime = DateTime.Now;

            // 调用通讯服务的接口向设备发送刷新数值的信号
            var cmdData = new
            {
                cmd = "UpdateAnalog",
                deviceSn = device.DeviceSn
            };
            await CmdClt.SendAsync(JsonConvert.SerializeObject(cmdData));

            var output = new GetSingleElectricDeviceDataOutput()
            {
                Result = 0
            };
            for (int i = 1; i <= 8; i++)
            {
                // 休眠一秒，去数据库查找比nowTime的时间还要新的数据，如果找到了就返回，最多循环5次，如果等了5秒还没有新数据就不继续等待
                Thread.Sleep(1000);
                var lstElectricRecord = _repFireElectricRecord.GetAll().Where(item => item.FireElectricDeviceId.Equals(electricDeviceId) && item.CreationTime >= nowTime)
                    .OrderByDescending(item => item.CreationTime).ToList();
                if (lstElectricRecord != null && lstElectricRecord.Count > 0)
                {
                    // 因为数值刷新时，硬件会返回多条数据，以下判断是为了防止只获取到硬件返回的其中一部分数据
                    if (device.ExistAmpere && !lstElectricRecord.Exists(item => item.Sign.Equals("A"))) break;
                    if (device.ExistTemperature)
                    {
                        if (!lstElectricRecord.Exists(item => item.Sign.Equals("N"))) break;
                        if (device.PhaseType.Equals(PhaseType.Single) && !(lstElectricRecord.Exists(item => item.Sign.Equals("L")))) break;
                        if (device.PhaseType.Equals(PhaseType.Third) && !(lstElectricRecord.Exists(item => item.Sign.Equals("L1")) && lstElectricRecord.Exists(item => item.Sign.Equals("L2")) && lstElectricRecord.Exists(item => item.Sign.Equals("L3")))) break;
                    }
                    // 重新获取设备数据
                    device = await _repFireElectricDevice.GetAsync(electricDeviceId);
                    var deviceData = new FireElectricDeviceItemDto();
                    deviceData.CreationTime = device.CreationTime;
                    deviceData.DeviceId = device.Id;
                    deviceData.DeviceSn = device.DeviceSn;
                    deviceData.FireUnitArchitectureId = device.FireUnitArchitectureId;
                    deviceData.FireUnitArchitectureFloorId = device.FireUnitArchitectureFloorId;
                    deviceData.Location = device.Location;
                    deviceData.State = device.State;
                    deviceData.ExistAmpere = device.ExistAmpere;
                    deviceData.ExistTemperature = device.ExistTemperature;
                    deviceData.PhaseType = device.PhaseType;
                    var architecture = await _repFireUnitArchitecture.GetAsync(device.FireUnitArchitectureId);
                    var architectureFloor = await _repFireUnitArchitectureFloor.GetAsync(device.FireUnitArchitectureFloorId);
                    deviceData.FireUnitArchitectureName = architecture != null ? architecture.Name : "";
                    deviceData.FireUnitArchitectureFloorName = architectureFloor != null ? architectureFloor.Name : "";
                    if (device.State.Equals(FireElectricDeviceState.Offline))
                    {
                        deviceData.L = "未知℃";
                        deviceData.N = "未知℃";
                        deviceData.A = "未知mA";
                        deviceData.L1 = "未知℃";
                        deviceData.L2 = "未知℃";
                        deviceData.L3 = "未知℃";
                    }
                    else
                    {
                        deviceData.A = lstElectricRecord.FirstOrDefault(item => item.Sign.Equals("A")).Analog + "mA";
                        deviceData.N = lstElectricRecord.FirstOrDefault(item => item.Sign.Equals("N")).Analog + "℃";
                        if (device.PhaseType.Equals(PhaseType.Single))
                        {
                            deviceData.L = lstElectricRecord.FirstOrDefault(item => item.Sign.Equals("L")).Analog + "℃";
                        }
                        else
                        {
                            deviceData.L1 = lstElectricRecord.FirstOrDefault(item => item.Sign.Equals("L1")).Analog + "℃";
                            deviceData.L2 = lstElectricRecord.FirstOrDefault(item => item.Sign.Equals("L2")).Analog + "℃";
                            deviceData.L3 = lstElectricRecord.FirstOrDefault(item => item.Sign.Equals("L3")).Analog + "℃";
                        }
                    }
                    output.DeviceData = deviceData;
                    output.Result = 1;
                    break;
                }
            }
            return output;
        }
        /// <summary>
        /// 发送断电信号
        /// </summary>
        /// <param name="electricDeviceId"></param>
        /// <returns></returns>
        public async Task BreakoffPower(int electricDeviceId)
        {
            var device = await _repFireElectricDevice.GetAsync(electricDeviceId);

            // 调用通讯服务的接口向设备发送断电信号
            var cmdData = new
            {
                cmd = "Switch",
                deviceSn = device.DeviceSn
            };
            await CmdClt.SendAsync(JsonConvert.SerializeObject(cmdData));

            // 延时1秒钟再返回，否则一点击发送信号就返回，感觉不大好
            Thread.Sleep(1000);
        }
        /// <summary>
        /// 获取某个火警联网设施下的故障部件列表
        /// </summary>
        /// <param name="fireAlarmDeviceId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<FaultDetectorOutput>> GetFireAlarmFaultDetectorList(int fireAlarmDeviceId, PagedResultRequestDto dto)
        {
            var device = await _repFireAlarmDevice.FirstOrDefaultAsync(p => p.Id == fireAlarmDeviceId);
            var fireUnitArchitecture = await _repFireUnitArchitecture.GetAsync(device.FireUnitArchitectureId);

            var fireAlarmFaultDetectors = _repFireAlarmDetector.GetAll().Where(item => item.FireAlarmDeviceId.Equals(fireAlarmDeviceId) && item.State.Equals(FireAlarmDetectorState.Fault));
            var fireAlarmDetectorFaults = _repFault.GetAll().Where(item => item.FireAlarmDeviceId.Equals(fireAlarmDeviceId));
            var detectorTypes = _repDetectorType.GetAll();
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
                            FaultContent = "",
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
            var alarmToFires = _repAlarmToFire.GetAll().Where(item => item.FireAlarmDeviceId == fireAlarmDeviceId && item.CreationTime >= DateTime.Now.AddDays(-30));
            var fireAlarmDetectors = _repFireAlarmDetector.GetAll().Where(item => item.FireAlarmDeviceId == fireAlarmDeviceId);
            var detectorTypes = _repDetectorType.GetAll();
            var fireUnitArchitectureFloors = _repFireUnitArchitectureFloor.GetAll();

            var query = from a in alarmToFires
                        join b in fireAlarmDetectors on a.FireAlarmDetectorId equals b.Id
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
        /// 获取火警联网设施部件类型数组
        /// </summary>
        /// <returns></returns>
        public Task<List<string>> GetFireAlarmDetectorTypes()
        {
            return Task.FromResult(_repDetectorType.GetAll().Where(item => item.ApplyForTSJ).Select(item => item.Name).ToList());
        }
        /// <summary>
        /// 获取指定火警联网设施ID的高频报警部件列表
        /// </summary>
        /// <param name="fireAlarmDeviceId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<PagedResultDto<GetFireAlarmHighDto>> GetFireAlarmHighList(int fireAlarmDeviceId, PagedResultRequestDto dto)
        {
            int highFreq = int.Parse(ConfigHelper.Configuration["FireDomain:HighFreqAlarm"]);   // 从配置文件中获取何谓高频

            var lstDetectorHigh = _repAlarmToFire.GetAll().Where(item => item.FireAlarmDeviceId == fireAlarmDeviceId && item.CreationTime >= DateTime.Now.AddDays(-30))
                .GroupBy(item => item.FireAlarmDetectorId).Where(item => item.Count() >= highFreq).Select(item => new
                {
                    FireAlarmDetectorId = item.Key,
                    AlarmNum = item.Count()
                }).ToList();
            var fireAlarmDetectors = _repFireAlarmDetector.GetAll().Where(item => item.FireAlarmDeviceId == fireAlarmDeviceId);
            var detectorTypes = _repDetectorType.GetAll();
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
            int highFreq = int.Parse(ConfigHelper.Configuration["FireDomain:HighFreqAlarm"]);   // 高频火警联网部件的阈值设定

            var fireAlarmDevices = _repFireAlarmDevice.GetAll().Where(item => item.FireUnitId.Equals(fireUnitId));
            var fireUnitArchitectures = _repFireUnitArchitecture.GetAll();

            // 30天火警数量及高频报警部件数量
            var groupFireAlarm = _repAlarmToFire.GetAll().Where(item => item.FireUnitId == fireUnitId && item.CreationTime >= DateTime.Now.AddDays(-30))
                .GroupBy(item => item.FireAlarmDeviceId).Select(p => new
                {
                    FireAlarmDeviceId = p.Key,
                    AlarmNum = p.Count(),
                    HighDeviceNum = p.GroupBy(p1 => p1.FireAlarmDetectorId).Where(p1 => p1.Count() > highFreq).Count()
                });

            // 故障部件数量统计
            var groupGtFault = _repFireAlarmDetector.GetAll()
                .Where(item => item.FireUnitId.Equals(fireUnitId) && item.State.Equals(FireAlarmDetectorState.Fault))
                .GroupBy(p => p.FireAlarmDeviceId).Select(p => new
                {
                    FireAlarmDeviceId = p.Key,
                    FaultNum = p.Count()
                });

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
                            State = a.State,
                            FireUnitArchitectureId = a_d != null ? a_d.Id : 0,
                            FireUnitArchitectureName = a_d != null ? a_d.Name : "",
                            NetDetectorNum = a.NetDetectorNum,
                            FaultDetectorNum = a_b != null ? a_b.FaultNum : 0,
                            DetectorFaultRate = "0%",
                            AlarmNum30Day = a_c != null ? a_c.AlarmNum : 0,
                            HighAlarmDetectorNum = a_c != null ? a_c.HighDeviceNum : 0,
                            CreationTime = a.CreationTime
                        };

            var lstOutput = query.OrderByDescending(item => item.CreationTime).Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList();
            foreach (var item in lstOutput)
            {
                if (item.NetDetectorNum > 0 && item.FaultDetectorNum > 0)
                {
                    item.DetectorFaultRate = (Math.Round((double)item.FaultDetectorNum / item.NetDetectorNum, 4) * 100).ToString() + "%";
                }
            }
            return Task.FromResult(new PagedResultDto<FireAlarmDeviceItemDto>()
            {
                Items = lstOutput,
                TotalCount = query.Count()
            });
        }
        /// <summary>
        /// 获取辖区内各防火单位的火警联网设施列表
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <param name="fireUnitName"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<PagedResultDto<FireAlarmDevice_DeptDto>> GetFireAlarmDeviceList_Dept(int fireDeptId, string fireUnitName, PagedResultRequestDto dto)
        {
            int highFreq = int.Parse(ConfigHelper.Configuration["FireDomain:HighFreqAlarm"]);   // 高频火警联网部件的阈值设定

            var fireAlarmDevices = _repFireAlarmDevice.GetAll();
            var fireUnitArchitectures = _repFireUnitArchitecture.GetAll();
            var fireUnits = _repFireUnit.GetAll().Where(item => item.FireDeptId.Equals(fireDeptId));
            if (!string.IsNullOrEmpty(fireUnitName))
            {
                fireUnits = fireUnits.Where(item => item.Name.Contains(fireUnitName));
            }
            var alarmToFires = _repAlarmToFire.GetAll().Where(item => item.CreationTime >= DateTime.Now.AddDays(-30));
            var faultDetectors = _repFireAlarmDetector.GetAll().Where(item => item.State.Equals(FireAlarmDetectorState.Fault));

            // 30天火警数量及高频报警部件数量
            var groupFireAlarm = from a in alarmToFires
                                 join b in fireUnits on a.FireUnitId equals b.Id
                                 group a by a.FireAlarmDeviceId into result1
                                 select new
                                 {
                                     FireAlarmDeviceId = result1.Key,
                                     AlarmNum = result1.Count(),
                                     HighDeviceNum = result1.GroupBy(p1 => p1.FireAlarmDetectorId).Where(p1 => p1.Count() > highFreq).Count()
                                 };

            // 故障部件数量统计
            var groupGtFault = from a in faultDetectors
                               join b in fireUnits on a.FireUnitId equals b.Id
                               group a by a.FireAlarmDeviceId into result1
                               select new
                               {
                                   FireAlarmDeviceId = result1.Key,
                                   FaultNum = result1.Count()
                               };

            var query = from a in fireAlarmDevices
                        join f in fireUnits on a.FireUnitId equals f.Id
                        join b in groupGtFault on a.Id equals b.FireAlarmDeviceId into result1
                        from a_b in result1.DefaultIfEmpty()
                        join c in groupFireAlarm on a.Id equals c.FireAlarmDeviceId into result2
                        from a_c in result2.DefaultIfEmpty()
                        join d in fireUnitArchitectures on a.FireUnitArchitectureId equals d.Id into result3
                        from a_d in result3.DefaultIfEmpty()
                        select new FireAlarmDevice_DeptDto()
                        {
                            FireUnitName = f.Name,
                            DeviceId = a.Id,
                            DeviceSn = a.DeviceSn,
                            State = a.State,
                            FireUnitArchitectureId = a_d != null ? a_d.Id : 0,
                            FireUnitArchitectureName = a_d != null ? a_d.Name : "",
                            NetDetectorNum = a.NetDetectorNum,
                            FaultDetectorNum = a_b != null ? a_b.FaultNum : 0,
                            DetectorFaultRate = "0%",
                            AlarmNum30Day = a_c != null ? a_c.AlarmNum : 0,
                            HighAlarmDetectorNum = a_c != null ? a_c.HighDeviceNum : 0,
                            CreationTime = a.CreationTime
                        };

            var lstOutput = query.OrderByDescending(item => item.FireUnitName).Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList();
            foreach (var item in lstOutput)
            {
                if (item.NetDetectorNum > 0 && item.FaultDetectorNum > 0)
                {
                    item.DetectorFaultRate = (Math.Round((double)item.FaultDetectorNum / item.NetDetectorNum, 4) * 100).ToString() + "%";
                }
            }
            return Task.FromResult(new PagedResultDto<FireAlarmDevice_DeptDto>()
            {
                Items = lstOutput,
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
            var detectorTypes = _repDetectorType.GetAll();
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
        //    var detectorType = await _repDetectorType.SingleAsync(p => p.Id == detector.DetectorTypeId);
        //    output.Name = detectorType.Name;
        //    output.Location = detector.Location;
        //    var state = _repRecordOnline.GetAll().Where(p => p.DetectorId == input.DetectorId).OrderByDescending(p => p.CreationTime).FirstOrDefault();
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
        //                  join b in _repRecordOnline.GetAll().Where(p => p.DetectorId == input.DetectorId && p.State == (sbyte)GatewayStatus.Offline && p.CreationTime >= input.Start && p.CreationTime <= input.End)
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
        //                       join b in _repAlarmToFire.GetAll().Where(p => p.CreationTime >= input.Start && p.CreationTime <= input.End)
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
        //                     join b in _repFault.GetAll().Where(p => p.CreationTime >= input.Start && p.CreationTime <= input.End)
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
                end = DateTime.Parse(input.End.ToShortDateString() + " 23:59:59");
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
        //    var detectorType = await _repDetectorType.SingleAsync(p => p.Id == detector.DetectorTypeId);
        //    output.Name = detectorType.Name;
        //    output.Location = detector.Location;
        //    var state = _repRecordOnline.GetAll().Where(p => p.DetectorId == input.DetectorId).OrderByDescending(p => p.CreationTime).FirstOrDefault();
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
        //                join b in _repDetectorType.GetAll()
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
        //               join b in _repDetectorType.GetAll()
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
        //               join b in _repDetectorType.GetAll()
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

        //    var type = _repDetectorType.GetAll().Where(p => p.GBType == input.DetectorGBType).FirstOrDefault();
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
                    List<string> lstDeteType = _repDetectorType.GetAll().Where(item => item.ApplyForTSJ).Select(item => item.Name).ToList();
                    //    new List<string>()
                    //{
                    //    "火灾报警控制器","感烟式火灾探测器","感温式火灾探测器",
                    //    "感光式火灾探测器","可燃气体火灾探测器","复合式火灾探测器","手动火灾报警按钮","其它"
                    //};

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

                    List<DetectorType> lstDetectorType = await _repDetectorType.GetAllListAsync();
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
        /// 在线/离线事件接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateDeviceState(UpdateDeviceStateInput input)
        {
            switch (input.GatewayType)
            {
                case TsjDeviceType.FireAlarm:
                    var fireAlarmDevice = await _repFireAlarmDevice.FirstOrDefaultAsync(item => item.DeviceSn.Equals(input.GatewaySn));
                    Valid.Exception(fireAlarmDevice == null, $"未找到编号为{input.GatewaySn}的火警联网设施");

                    fireAlarmDevice.State = input.GatewayStatus;
                    await _repFireAlarmDevice.UpdateAsync(fireAlarmDevice);
                    break;
                case TsjDeviceType.FireElectric:
                    var fireElectricDevice = await _repFireElectricDevice.FirstOrDefaultAsync(item => item.DeviceSn.Equals(input.GatewaySn));
                    Valid.Exception(fireElectricDevice == null, $"未找到编号为{input.GatewaySn}的电气火灾设施");

                    fireElectricDevice.State = input.GatewayStatus.Equals(GatewayStatus.Offline) ? FireElectricDeviceState.Offline : FireElectricDeviceState.Good;
                    await _repFireElectricDevice.UpdateAsync(fireElectricDevice);
                    //发送配置给设备，每次设备重新连线后
                    if (input.GatewayStatus == GatewayStatus.Online)
                    {
                        //设备通信
                        var cmdData = new
                        {
                            cmd = "ConfigPhase",
                            deviceSn = fireElectricDevice.DeviceSn,
                            phaseType = fireElectricDevice.PhaseType,
                            minAmpere = fireElectricDevice.MinAmpere,
                            maxAmpere = fireElectricDevice.MaxAmpere,
                            minL = fireElectricDevice.MinL,
                            maxL = fireElectricDevice.MaxL,
                            minN = fireElectricDevice.MinN,
                            maxN = fireElectricDevice.MaxN,
                            minL1 = fireElectricDevice.MinL1,
                            maxL1 = fireElectricDevice.MaxL1,
                            minL2 = fireElectricDevice.MinL2,
                            maxL2 = fireElectricDevice.MaxL2,
                            minL3 = fireElectricDevice.MinL3,
                            maxL3 = fireElectricDevice.MaxL3
                        };
                        await CmdClt.SendAsync(JsonConvert.SerializeObject(cmdData));
                    }

                    break;
                case TsjDeviceType.FireWater:
                    var lstFireWaterDevice = _repFireWaterDevice.GetAll().Where(item => item.Gateway_Sn.Equals(input.GatewaySn)).ToList();
                    Valid.Exception(lstFireWaterDevice == null, $"未找到编号为{input.GatewaySn}的消防管网监测设施");

                    foreach (var device in lstFireWaterDevice)
                    {
                        device.State = input.GatewayStatus.Equals(GatewayStatus.Offline) ? FireWaterDeviceState.Offline : FireWaterDeviceState.Good;
                        await _repFireWaterDevice.UpdateAsync(device);
                    }
                    break;
            }
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
            elec.EnableCloudAlarm = input.EnableAlarmList.Contains("云端报警");
            elec.EnableEndAlarm = input.EnableAlarmList.Contains("终端报警");
            elec.EnableAlarmSwitch = input.EnableAlarmList.Contains("自动断电");
            elec.ExistAmpere = input.MonitorItemList.Contains("剩余电流");
            elec.ExistTemperature = input.MonitorItemList.Contains("电缆温度");
            elec.FireUnitArchitectureFloorId = input.FireUnitArchitectureFloorId;
            elec.Location = input.Location;
            elec.PhaseType = input.PhaseType;
            elec.MinAmpere = input.MinAmpere;
            elec.MaxAmpere = input.MaxAmpere;
            elec.MinL = input.MinL;
            elec.MaxL = input.MaxL;
            elec.MinN = input.MinN;
            elec.MaxN = input.MaxN;
            elec.MinL1 = input.MinL1;
            elec.MaxL1 = input.MaxL1;
            elec.MinL2 = input.MinL2;
            elec.MaxL2 = input.MaxL2;
            elec.MinL3 = input.MinL3;
            elec.MaxL3 = input.MaxL3;
            await _repFireElectricDevice.UpdateAsync(elec);
            //设备通信
            var cmdData = new
            {
                cmd = "ConfigPhase",
                deviceSn = input.DeviceSn,
                phaseType = input.PhaseType,
                minAmpere = input.MinAmpere,
                maxAmpere = input.MaxAmpere,
                minL = input.MinL,
                maxL = input.MaxL,
                minN = input.MinN,
                maxN = input.MaxN,
                minL1 = input.MinL1,
                maxL1 = input.MaxL1,
                minL2 = input.MinL2,
                maxL2 = input.MaxL2,
                minL3 = input.MinL3,
                maxL3 = input.MaxL3
            };
            await CmdClt.SendAsync(JsonConvert.SerializeObject(cmdData));
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
                EnableCloudAlarm = input.EnableAlarmList.Contains("云端报警"),
                EnableEndAlarm = input.EnableAlarmList.Contains("终端报警"),
                EnableAlarmSwitch = input.EnableAlarmList.Contains("自动断电"),
                ExistAmpere = input.MonitorItemList.Contains("剩余电流"),
                ExistTemperature = input.MonitorItemList.Contains("电缆温度"),
                FireUnitArchitectureFloorId = input.FireUnitArchitectureFloorId,
                Location = input.Location,
                PhaseType = input.PhaseType,
                NetComm = input.NetComm,
                DataRate = "2小时",
                MinAmpere = input.MinAmpere,
                MaxAmpere = input.MaxAmpere,
                MinL = input.MinL,
                MaxL = input.MaxL,
                MinL1 = input.MinL1,
                MaxL1 = input.MaxL1,
                MinL2 = input.MinL2,
                MaxL2 = input.MaxL2,
                MinL3 = input.MinL3,
                MaxL3 = input.MaxL3,
                MinN = input.MinN,
                MaxN = input.MaxN
            });
            //设备通信
            var cmdData = new
            {
                cmd= "ConfigPhase",
                deviceSn = input.DeviceSn,
                phaseType = input.PhaseType,
                minAmpere = input.MinAmpere,
                maxAmpere = input.MaxAmpere,
                minL = input.MinL,
                maxL = input.MaxL,
                minN = input.MinN,
                maxN = input.MaxN,
                minL1 = input.MinL1,
                maxL1 = input.MaxL1,
                minL2 = input.MinL2,
                maxL2 = input.MaxL2,
                minL3 = input.MinL3,
                maxL3 = input.MaxL3
            };
            await CmdClt.SendAsync(JsonConvert.SerializeObject(cmdData));
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
                device.DeviceModel = input.DeviceType;
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
                DeviceType = device.DeviceModel,
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
                    DeviceModel = input.DeviceType,
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
                    var fireUnitSystemlist = await _repFireUnitSystem.GetAllListAsync(u => u.FireUnitId == input.FireUnitId);
                    var fireUnitSystems = from a in fireUnitSystemlist
                                          join b in _repFireSystem.GetAll() on a.FireSystemId equals b.Id
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
                                    Floors = FireUnitArchitectureFloors.Where(item => item.ArchitectureId.Equals(a.Id)).Select(item => new GetFloorListOutput()
                                    {
                                        Id = item.Id,
                                        Name = item.Name
                                    }).ToList()
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
                            DeviceModel = row["设备型号"].ToString().Trim(),
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
            return _repDetectorType.GetAll().Where(p => p.GBType == GBtype).FirstOrDefault();
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
            Valid.Exception(input.Sign != "A" && input.Sign != "N" && input.Sign != "L" && input.Sign != "L1" && input.Sign != "L2" && input.Sign != "L3", "记录的类型标记错误");

            double analog = Math.Round(input.Analog, 2);
            await _repFireElectricRecord.InsertAsync(new FireElectricRecord()
            {
                FireUnitId = fireElectricDevice.FireUnitId,
                FireElectricDeviceId = fireElectricDevice.Id,
                Sign = input.Sign,
                Analog = analog
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
            if (max > 0 && analog > 0)
            {
                if (analog > max) nowState = FireElectricDeviceState.Transfinite;
                else if (analog >= max * 0.8) nowState = FireElectricDeviceState.Danger;
            }
            // 如果状态发生了变化，则修改电气火灾设施的状态
            if (fireElectricDevice.State != nowState)
            {
                if (nowState.Equals(FireElectricDeviceState.Transfinite))
                {
                    // 断电
                    if (fireElectricDevice.EnableAlarmSwitch)
                    {
                        // 调用通讯服务的接口向设备发送断电信号
                        var cmdData = new
                        {
                            cmd = "Switch",
                            deviceSn = fireElectricDevice.DeviceSn
                        };
                        await CmdClt.SendAsync(JsonConvert.SerializeObject(cmdData));
                    }
                    // 发送报警短信
                    bool flag = bool.Parse(ConfigHelper.Configuration["FireDomain:SendShortMessage"]);   // 从配置文件中获取是否允许发送短信
                    if (flag)
                    {
                        var fireUnit = await _repFireUnit.GetAsync(fireElectricDevice.FireUnitId);
                        if (fireUnit != null && !string.IsNullOrEmpty(fireUnit.ContractPhone))
                        {
                            string contents = "电气火灾报警：";

                            try
                            {
                                var architecture = await _repFireUnitArchitecture.GetAsync(fireElectricDevice.FireUnitArchitectureId);
                                string architectureName = architecture != null ? architecture.Name : "";
                                var floor = await _repFireUnitArchitectureFloor.GetAsync(fireElectricDevice.FireUnitArchitectureFloorId);
                                string floorName = floor != null ? floor.Name : "";
                                string unit = input.Sign.Equals("A") ? "mA" : "℃";
                                contents += $"位于“{fireUnit.Name}{architectureName}{floorName}{fireElectricDevice.Location}”，编号为“{fireElectricDevice.DeviceSn}”的“电气火灾防护设施”发出报警，时间为“{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}”，数值为{input.Sign}：{analog}{unit}";
                                contents += "，请立即安排处置！【天树聚电气火灾防护】";

                                int result = await ShotMessageHelper.SendMessage(new Common.Helper.ShortMessage()
                                {
                                    Phones = fireUnit.ContractPhone,
                                    Contents = contents
                                });

                                await _repShortMessageLog.InsertAsync(new ShortMessageLog()
                                {
                                    AlarmType = AlarmType.Electric,
                                    FireUnitId = fireElectricDevice.FireUnitId,
                                    Phones = fireUnit.ContractPhone,
                                    Contents = contents,
                                    Result = result
                                });
                            }
                            catch { }
                        }
                    }
                }
                fireElectricDevice.State = nowState;
                await _repFireElectricDevice.UpdateAsync(fireElectricDevice);

                // 如果隐患或超限，则向AlarmToElectric表中插入一条数据
                if (nowState.Equals(FireElectricDeviceState.Transfinite) || nowState.Equals(FireElectricDeviceState.Danger))
                {
                    await _repAlarmToElectric.InsertAsync(new AlarmToElectric()
                    {
                        FireElectricDeviceId = fireElectricDevice.Id,
                        Sign = input.Sign,
                        Analog = analog,
                        State = nowState,
                        FireUnitId = fireElectricDevice.FireUnitId
                    });
                }
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
        //    await _repRecordOnline.InsertAsync(new RecordOnline()
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
        //        await _repRecordOnline.InsertAsync(new RecordOnline()
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
            return Task.FromResult(_repTsjDeviceModel.GetAll().Where(item => item.DeviceType.Equals(TsjDeviceType.FireAlarm)).Select(item => item.Model).ToList());
        }
        /// <summary>
        /// 获取电气火灾设备型号数组
        /// </summary>
        /// <returns></returns>
        public Task<List<string>> GetFireElectricDeviceModels()
        {
            return Task.FromResult(_repTsjDeviceModel.GetAll().Where(item => item.DeviceType.Equals(TsjDeviceType.FireElectric)).Select(item => item.Model).ToList());
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
        public Task<SerialPortParamDto> GetSerialPortParam(int deviceId)
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
            Valid.Exception(_repFireWaterDevice.Count(m => m.Gateway_Sn.Equals(input.Gateway_Sn) && !m.FireUnitId.Equals(input.FireUnitId)) > 0, "设备网关编号已存在");
            Valid.Exception(_repFireWaterDevice.Count(m => m.DeviceAddress.Equals(input.DeviceAddress) && m.Gateway_Sn.Equals(input.Gateway_Sn)) > 0, "设备地址已存在");

            await _repFireWaterDevice.InsertAsync(new FireWaterDevice()
            {
                CreationTime = DateTime.Now,
                FireUnitId = input.FireUnitId,
                DeviceAddress = input.DeviceAddress,
                State = FireWaterDeviceState.Offline,
                Location = input.Location,
                Gateway_Model = input.Gateway_Model,
                Gateway_Sn = input.Gateway_Sn,
                Gateway_Location = input.Gateway_Location,
                Gateway_NetComm = input.Gateway_NetComm,
                Gateway_DataRate = "2小时",
                MonitorType = input.MonitorType,
                MinThreshold = input.MinThreshold,
                MaxThreshold = input.MaxThreshold,
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
            Valid.Exception(_repFireWaterDevice.Count(m => m.Gateway_Sn.Equals(input.Gateway_Sn) && !m.FireUnitId.Equals(input.FireUnitId)) > 0, "设备网关编号已存在");
            Valid.Exception(_repFireWaterDevice.Count(m => m.DeviceAddress.Equals(input.DeviceAddress) && m.Gateway_Sn.Equals(input.Gateway_Sn) && !m.Id.Equals(input.Id)) > 0, "设备地址已存在");

            FireWaterDevice device = await _repFireWaterDevice.GetAsync(input.Id);

            device.DeviceAddress = input.DeviceAddress;
            device.Location = input.Location;
            device.Gateway_Model = input.Gateway_Model;
            device.Gateway_Sn = input.Gateway_Sn;
            device.Gateway_Location = input.Gateway_Location;
            device.Gateway_NetComm = input.Gateway_NetComm;
            device.MonitorType = input.MonitorType;
            device.MinThreshold = input.MinThreshold;
            device.MaxThreshold = input.MaxThreshold;
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
                Gateway_Model = device.Gateway_Model,
                Gateway_NetComm = device.Gateway_NetComm,
                Gateway_DataRate = device.Gateway_DataRate,
                MinThreshold = device.MinThreshold,
                MaxThreshold = device.MaxThreshold
            };
        }

        /// <summary>
        /// 获取消防管网设备列表
        /// </summary>
        /// <param name="fireUnitId"></param>
        /// <returns></returns>
        public Task<PagedResultDto<GetFireWaterDeviceListOutput>> GetFireWaterDeviceList(int fireUnitId, PagedResultRequestDto dto)
        {
            var fireWaterDevices = _repFireWaterDevice.GetAll().Where(item => item.FireUnitId.Equals(fireUnitId));

            var query = from a in fireWaterDevices
                        orderby a.CreationTime descending
                        select new GetFireWaterDeviceListOutput()
                        {
                            Id = a.Id,
                            DeviceAddress = a.DeviceAddress,
                            Location = a.Location,
                            Gateway_Sn = a.Gateway_Sn,
                            Gateway_Model = a.Gateway_Model,
                            Gateway_Location = a.Gateway_Location,
                            MonitorType = a.MonitorType,
                            MinThreshold = a.MinThreshold,
                            MaxThreshold = a.MaxThreshold,
                            State = a.State,
                            CurrentValue = a.State.Equals(FireWaterDeviceState.Offline) ? "未知" : a.CurrentValue + (a.MonitorType.Equals(MonitorType.Height) ? "m" : "MPa")
                        };

            return Task.FromResult(new PagedResultDto<GetFireWaterDeviceListOutput>()
            {
                Items = query.Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList(),
                TotalCount = query.Count()
            });
        }
        /// <summary>
        /// 获取辖区各防火单位的消防管网设施列表
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <param name="fireUnitName"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<PagedResultDto<GetFireWaterDeviceList_DeptOutput>> GetFireWaterDeviceList_Dept(int fireDeptId, string fireUnitName, PagedResultRequestDto dto)
        {
            var fireWaterDevices = _repFireWaterDevice.GetAll();
            var fireUnits = _repFireUnit.GetAll().Where(item => item.FireDeptId.Equals(fireDeptId));
            if (!string.IsNullOrEmpty(fireUnitName))
            {
                fireUnits = fireUnits.Where(item => item.Name.Contains(fireUnitName));
            }

            var query = from a in fireWaterDevices
                        join b in fireUnits on a.FireUnitId equals b.Id
                        orderby a.CreationTime descending
                        select new GetFireWaterDeviceList_DeptOutput()
                        {
                            FireUnitName = b.Name,
                            Id = a.Id,
                            DeviceAddress = a.DeviceAddress,
                            Location = a.Location,
                            Gateway_Sn = a.Gateway_Sn,
                            Gateway_Model = a.Gateway_Model,
                            Gateway_Location = a.Gateway_Location,
                            MonitorType = a.MonitorType,
                            MinThreshold = a.MinThreshold,
                            MaxThreshold = a.MaxThreshold,
                            State = a.State,
                            CurrentValue = a.State.Equals(FireWaterDeviceState.Offline) ? "未知" : a.CurrentValue + (a.MonitorType.Equals(MonitorType.Height) ? "m" : "MPa")
                        };

            return Task.FromResult(new PagedResultDto<GetFireWaterDeviceList_DeptOutput>()
            {
                Items = query.Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList(),
                TotalCount = query.Count()
            });
        }
        /// <summary>
        /// 获取消防管网联网网关设备型号列表
        /// </summary>
        /// <returns></returns>
        public Task<List<string>> GetFireWaterDeviceModels()
        {
            return Task.FromResult(_repTsjDeviceModel.GetAll().Where(item => item.DeviceType.Equals(TsjDeviceType.FireWater)).Select(item => item.Model).ToList());
        }

        /// <summary>
        /// 添加消防水管网监测数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddFireWaterRecord(AddFireWaterRecordInput input)
        {
            var device = await _repFireWaterDevice.FirstOrDefaultAsync(item => item.Gateway_Sn.Equals(input.FireWaterGatewaySn) && item.DeviceAddress.Equals(input.FireWaterDeviceSn));
            Valid.Exception(device == null, "未找到对应的消防水管网监测设施");

            await _repFireWaterRecord.InsertAndGetIdAsync(new FireWaterRecord()
            {
                FireWaterDeviceId = device.Id,
                Value = input.Value
            });

            device.CurrentValue = input.Value;
            if (input.Value >= device.MinThreshold && input.Value <= device.MaxThreshold)
            {
                device.State = FireWaterDeviceState.Good;
            }
            else
            {
                await _repAlarmToWater.InsertAsync(new AlarmToWater()
                {
                    FireUnitId = device.FireUnitId,
                    FireWaterDeviceId = device.Id,
                    Analog = input.Value
                });
                device.State = FireWaterDeviceState.Transfinite;
            }

            await _repFireWaterDevice.UpdateAsync(device);
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
