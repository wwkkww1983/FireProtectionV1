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
        IRepository<FireElectricDeviceType> _repFireElectricDeviceType;
        IRepository<FireAlarmDeviceType> _repFireAlarmDeviceType;
        IRepository<FireUnitArchitectureFloor> _repFireUnitArchitectureFloor;
        IRepository<FireUnitArchitecture> _repFireUnitArchitecture;
        IRepository<FireAlarmDevice> _repFireAlarmDevice;
        IRepository<FireElectricDevice> _repFireElectricDevice;
        IRepository<FireOrtherDevice> _repFireOrtherDevice;
        IRepository<FireUntiSystem> _fireUnitSystemRep;
        IRepository<FireSystem> _fireSystemRep;
        IRepository<Fault> _faultRep;
        IRepository<AlarmToFire> _alarmToFireRep;
        IRepository<RecordOnline> _recordOnlineRep;
        IRepository<RecordAnalog> _recordAnalogRep;
        IFireSettingManager _fireSettingManager;
        IRepository<DetectorType> _detectorTypeRep;
        IRepository<Gateway> _gatewayRep;
        IRepository<Detector> _detectorRep;
        public DeviceManager(
            IRepository<FireAlarmDeviceProtocol> repFireAlarmDeviceProtocol,
            IRepository<FireElectricDeviceType> repFireElectricDeviceType,
            IRepository<FireAlarmDeviceType> repFireAlarmDeviceType,
            IRepository<FireUnitArchitectureFloor> repFireUnitArchitectureFloor,
            IRepository<FireUnitArchitecture> repFireUnitArchitecture,
            IRepository<FireAlarmDevice> repFireAlarmDevice,
            IRepository<FireElectricDevice> repFireElectricDevice,
            IRepository<FireOrtherDevice> repFireOrtherDevice,
            IRepository<FireUntiSystem> fireUnitSystemRep,
            IRepository<FireSystem> fireSystemRep,
            IRepository<Fault> faultRep,
            IRepository<AlarmToFire> alarmToFireRep,
            IRepository<RecordOnline> recordOnlineRep,
            IRepository<RecordAnalog> recordAnalogRep,
            IFireSettingManager fireSettingManager,
            IRepository<Detector> detectorRep,
             IRepository<DetectorType> detectorTypeRep,
           IRepository<Gateway> gatewayRep)
        {
            _repFireAlarmDeviceProtocol = repFireAlarmDeviceProtocol;
            _repFireElectricDeviceType = repFireElectricDeviceType;
            _repFireAlarmDeviceType = repFireAlarmDeviceType;
            _repFireUnitArchitectureFloor = repFireUnitArchitectureFloor;
            _repFireUnitArchitecture = repFireUnitArchitecture;
            _repFireAlarmDevice = repFireAlarmDevice;
            _repFireElectricDevice = repFireElectricDevice;
            _repFireOrtherDevice = repFireOrtherDevice;
            _fireUnitSystemRep = fireUnitSystemRep;
            _fireSystemRep = fireSystemRep;
            _faultRep = faultRep;
            _alarmToFireRep = alarmToFireRep;
            _recordOnlineRep = recordOnlineRep;
            _recordAnalogRep = recordAnalogRep;
            _fireSettingManager = fireSettingManager;
            _detectorTypeRep = detectorTypeRep;
            _detectorRep = detectorRep;
            _gatewayRep = gatewayRep;
        }
        public async Task<SuccessOutput> UpdateFireAlarmDetector(UpdateDetectorDto input)
        {
            try
            {
                var detector = await _detectorRep.GetAsync(input.DetectorId);
                var detectortype = await _detectorTypeRep.FirstOrDefaultAsync(p => p.Name.Equals(input.DetectorTypeName));
                detector.DetectorTypeId = detectortype == null ? 13 : detectortype.Id;
                detector.FireUnitArchitectureFloorId = input.FireUnitArchitectureFloorId;
                detector.Location = input.Location;
                detector.Identify = input.Identify;
            }
            catch(Exception e)
            {
                return new SuccessOutput() { Success = false, FailCause = e.Message };
            }
            return new SuccessOutput() { Success = true };
        }
        public async Task<SuccessOutput> DeleteFireOrtherDevice(int DeviceId)
        {
            if (await _repFireOrtherDevice.FirstOrDefaultAsync(p => p.Id == DeviceId) == null)
                return new SuccessOutput() { Success = false, FailCause = $"不存在ID为{DeviceId}的设备" };
            await _repFireOrtherDevice.DeleteAsync(DeviceId);
            return new SuccessOutput() { Success = true };
        }
        public async Task<SuccessOutput> DeleteFireElectricDevice(int DeviceId)
        {
            var device = await _repFireElectricDevice.FirstOrDefaultAsync(p => p.Id == DeviceId);
            if (device == null)
                return new SuccessOutput() { Success = false, FailCause = $"不存在ID为{DeviceId}的设备" };
            await _repFireElectricDevice.DeleteAsync(DeviceId);
            var gateway = await _gatewayRep.FirstOrDefaultAsync(device.GatewayId);
            if(gateway!=null)
            {
                await _detectorRep.DeleteAsync(p => p.GatewayId == gateway.Id);
                await _gatewayRep.DeleteAsync(gateway);
            }
            return new SuccessOutput() { Success = true };
        }
        public async Task<SuccessOutput> DeleteFireAlarmDevice(int DeviceId)
        {
            var device = await _repFireAlarmDevice.FirstOrDefaultAsync(p => p.Id == DeviceId);
            if (device == null)
                return new SuccessOutput() { Success = false, FailCause = $"不存在ID为{DeviceId}的设备" };
            await _repFireAlarmDevice.DeleteAsync(DeviceId);
            var gateway = await _gatewayRep.FirstOrDefaultAsync(device.GatewayId);
            if (gateway != null)
            {
                await _detectorRep.DeleteAsync(p => p.GatewayId == gateway.Id);
                await _gatewayRep.DeleteAsync(gateway);
            }
            return new SuccessOutput() { Success = true };
        }

        public async Task<SuccessOutput> DeleteDetector(int DetectorId)
        {
            var detector = await _detectorRep.FirstOrDefaultAsync(DetectorId);
            if(detector==null)
                return new SuccessOutput() { Success = false, FailCause = "部件不存在"};
            await _detectorRep.DeleteAsync(DetectorId);
            return new SuccessOutput() { Success = true };
        }
        /// <summary>
        /// 添加火警联网部件
        /// </summary>
        /// <param name="detectorDto"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        public async Task<AddDeviceDetectorOutput> AddFireAlarmDetector(AddDetectorDto detectorDto ,string origin)
        {
            var device = await _repFireAlarmDevice.FirstOrDefaultAsync(p => p.DeviceSn.Equals(detectorDto.DeviceSn));
            var detectortype = await _detectorTypeRep.FirstOrDefaultAsync(p => p.Name.Equals(detectorDto.DetectorTypeName));
            var id=await _detectorRep.InsertAndGetIdAsync(new Detector()
            {
                Origin = origin,
                FireSysType = (byte)FireSysType.Fire,
                DetectorTypeId = detectortype==null?13:detectortype.Id,
                Location = detectorDto.Location,
                GatewayId = device.GatewayId,
                FaultNum = 0,
                FireUnitArchitectureFloorId = detectorDto.FireUnitArchitectureFloorId,
                FireUnitId = device.FireUnitId,
                Identify = detectorDto.Identify
            });
            return new AddDeviceDetectorOutput() { Success = true, DetectorId = id };
        }
        public async Task<GetFireAlarmDeviceDto> GetFireAlarmDevice(int DeviceId)
        {
            var device = await _repFireAlarmDevice.FirstOrDefaultAsync(p => p.Id == DeviceId);
            if (device == null)
                return new GetFireAlarmDeviceDto();
            var gateway = await _gatewayRep.FirstOrDefaultAsync(p => p.Id == device.GatewayId);
            if (gateway == null)
                return new GetFireAlarmDeviceDto() ;
            var output = new GetFireAlarmDeviceDto()
            {
                DataRate="2小时",
                NetComms = new List<string>() { "以太网", "WIFI", "NB-IoT" },
                State=gateway.Status==GatewayStatus.Online?"在线":"离线",
                Brand = device.Brand,
                DeviceId = device.Id,
                DeviceSn = device.DeviceSn,
                DeviceType = device.DeviceType,
                FireUnitArchitectureId = device.FireUnitArchitectureId,
                FireUnitId = device.FireUnitId,
                NetDetectorNum = device.NetDetectorNum,
                Protocol = device.Protocol,
                NetComm=device.NetComm
            };
            output.EnableAlarm = new List<string>();
            output.EnableFault = new List<string>();
            if (device.EnableAlarm)
                output.EnableAlarm.Add("云端报警");
            if(device.EnableAlarmSwitch)
                output.EnableAlarm.Add("发送开关量信号");
            if (device.EnableFault)
                output.EnableFault.Add("云端报警");
            if (device.EnableFaultSwitch)
                output.EnableFault.Add("发送开关量信号");
            return output;
        }
        public async Task<SuccessOutput> UpdateFireAlarmDevice(UpdateFireAlarmDeviceDto input)
        {
            var device = await _repFireAlarmDevice.FirstOrDefaultAsync(p => p.Id == input.DeviceId);
            if (device == null)
                return new SuccessOutput() { Success = false, FailCause = $"不存在ID为{input.DeviceId}的设备" };
            var gateway = await _gatewayRep.FirstOrDefaultAsync(p => p.Id == device.GatewayId);
            if (gateway == null)
                return new SuccessOutput() { Success = false, FailCause = $"ID为{input.DeviceId}的设备对应网关已被删除" };
            device.Brand = input.Brand;
            device.DeviceSn = input.DeviceSn;
            device.DeviceType = input.DeviceType;
            device.EnableAlarm = input.EnableAlarm.Contains("云端报警");
            device.EnableAlarmSwitch = input.EnableAlarm.Contains("发送开关量信号");
            device.EnableFault = input.EnableFault.Contains("云端报警");
            device.EnableFaultSwitch = input.EnableFault.Contains("发送开关量信号");
            device.FireUnitArchitectureId = input.FireUnitArchitectureId;
            device.NetDetectorNum = input.NetDetectorNum;
            device.Protocol = input.Protocol;
            device.NetComm = input.NetComm;
            await _repFireAlarmDevice.UpdateAsync(device);
            gateway.Identify = device.DeviceSn;
            var arch = await _repFireUnitArchitecture.FirstOrDefaultAsync(p => p.Id == device.FireUnitArchitectureId);
            if (arch != null)
                gateway.Location = arch.Name;
            return new SuccessOutput() { Success = true };
        }
        public async Task<UpdateFireElectricDeviceDto> GetFireElectricDevice(int DeviceId)
        {
            var output = new UpdateFireElectricDeviceDto();
            var device = await _repFireElectricDevice.FirstOrDefaultAsync(p => p.Id == DeviceId);
            if (device == null)
                return output;
            var gateway = await _gatewayRep.FirstOrDefaultAsync(p => p.Id == device.GatewayId);
            output.State = gateway.Status==GatewayStatus.Online? device.State:"离线";
            output.Location = device.Location;
            output.FireUnitId = device.FireUnitId;
            output.DeviceId = device.Id;
            output.FireUnitArchitectureFloorId = device.FireUnitArchitectureFloorId;
            output.FireUnitArchitectureId = device.FireUnitArchitectureId;
            output.DeviceType = device.DeviceType;
            output.DeviceSn = device.DeviceSn;
            output.PhaseType = device.PhaseType;
            output.DataRate = "2小时";
            output.NetComms = new List<string>() { "以太网", "WIFI", "NB-IoT" };
            output.NetComm = device.NetComm;
            if (output.PhaseType.Equals("单相"))
            {
                SinglePhase singlePhase = JsonConvert.DeserializeObject<SinglePhase>(device.PhaseJson);
                output.Lmin = singlePhase.Lmin;
                output.Lmax = singlePhase.Lmax;
                output.Nmin = singlePhase.Nmin;
                output.Nmax = singlePhase.Nmax;
                output.Amax = singlePhase.I0max;
                output.Amin = singlePhase.I0min;
            }else if (output.PhaseType.Equals("三相"))
            {
                ThreePhase threePhase = JsonConvert.DeserializeObject<ThreePhase>(device.PhaseJson);
                output.L1min = threePhase.L1min;
                output.L1max = threePhase.L1max;
                output.L2min = threePhase.L2min;
                output.L2max = threePhase.L2max;
                output.L3min = threePhase.L3min;
                output.L3max = threePhase.L3max;
                output.Nmin = threePhase.Nmin;
                output.Nmax = threePhase.Nmax;
                output.Amax = threePhase.I0max;
                output.Amin = threePhase.I0min;
            }
            output.EnableAlarm = new List<string>();
            output.MonitorItem = new List<string>();
            if (device.EnableCloudAlarm)
                output.EnableAlarm.Add("云端报警");
            if (device.EnableEndAlarm)
                output.EnableAlarm.Add("终端报警");
            if (device.EnableAlarmSwitch)
                output.EnableAlarm.Add("发送开关量信号");
            if (device.ExistAmpere)
                output.MonitorItem.Add("剩余电流");
            if (device.ExistTemperature)
                output.MonitorItem.Add("电缆温度");
            return output;
        }
        public async Task<GetFireElectricDeviceStateOutput> GetFireElectricDeviceState(int FireUnitId)
        {

            var devices = from a in _repFireElectricDevice.GetAll().Where(p=>p.FireUnitId==FireUnitId)
                           join b in _gatewayRep.GetAll().Where(p => p.FireUnitId == FireUnitId && p.FireSysType == (byte)FireSysType.Electric)
                           on a.GatewayId equals b.Id
                           select new {
                               b.Status,
                               a.State
                           };
            //var query = from a in _detectorRep.GetAll()
            //                join b in _repFireElectricDevice.GetAll().Where(p => p.FireUnitId == FireUnitId) on a.GatewayId equals b.GatewayId
            //                select new
            //                {
            //                    a.Id,
            //                    Identify=a.Identify,
            //                    a.GatewayId,
            //                    State=a.State==null?"":a.State,
            //                    b.PhaseType,
            //                    PhaseJson=string.IsNullOrEmpty( b.PhaseJson)?"{}": b.PhaseJson
            //                };
            //var detectors = query.ToList();
            ////和设备配置比较 80% 良好，隐患
            //var d2 = detectors.Select(p => new
            //{
            //    Max = !JObject.Parse(p.PhaseJson).ContainsKey($"{p.Identify}max")?0:double.Parse(JObject.Parse(p.PhaseJson)[$"{p.Identify}max"].ToString()),
            //    p.Id, p.GatewayId,
            //    Value= System.Text.RegularExpressions.Regex.Replace(p.State, @"[^\d.\d]", "").Equals("")?0:double.Parse(System.Text.RegularExpressions.Regex.Replace(p.State, @"[^\d.\d]", "")),
            //}).ToList();

            //var group = d2.GroupBy(p => p.GatewayId).Select(p => new
            //{
            //    GatewayId = p.Key,
            //    BadNum = p.Where(p1 => p1.Value < p1.Max && p1.Value >= 0.8 * p1.Max).Count(),
            //    GoodNum = p.Where(p1 => p1.Value < 0.8 * p1.Max).Count(),
            //    WarnNum = p.Where(p1 => p1.Value >= p1.Max).Count()
            //}).ToList();
            return new GetFireElectricDeviceStateOutput()
            {
                BadNum = devices.Where(p => p.State.Equals("隐患")).Count(),
                GoodNum = devices.Where(p => p.State.Equals("良好")).Count(),
                OfflineNum = devices.Count(p => p.Status != GatewayStatus.Online),
                OnlineNum = devices.Count(p=>p.Status==GatewayStatus.Online),
                WarnNum = devices.Where(p=> p.State.Equals("超限")).Count()
            };
        }
        public async Task<PagedResultDto<FireElectricDeviceItemDto>> GetFireElectricDeviceList(int fireUnitId, string state, PagedResultRequestDto dto)
        {
            var devices = _repFireElectricDevice.GetAll().Where(p => p.FireUnitId == fireUnitId);
            var gateways = _gatewayRep.GetAll().Where(p=>p.FireUnitId==fireUnitId&&p.FireSysType==(byte)FireSysType.Electric);
            if (state != null)
            {
                if (state.Equals("在线"))
                    gateways = gateways.Where(p => p.Status == GatewayStatus.Online);
                else if (state.Equals("离线"))
                    gateways = gateways.Where(p => p.Status != GatewayStatus.Online);
                else if (!string.IsNullOrEmpty(state))
                    devices = devices.Where(p => p.State.Equals(state));
            }
            var detectors = _detectorRep.GetAll().Where(p => p.FireUnitId == fireUnitId).ToList();
            var query0 = from a in devices
                         join b0 in _repFireUnitArchitectureFloor.GetAll() on a.FireUnitArchitectureFloorId equals b0.Id into be
                        from b in be.DefaultIfEmpty()
                        join c0 in _repFireUnitArchitecture.GetAll() on a.FireUnitArchitectureId equals c0.Id into ce
                        from c in ce.DefaultIfEmpty()
                        join d in gateways on a.GatewayId equals d.Id
                        orderby a.CreationTime descending
                        let L= detectors.FirstOrDefault(p => p.GatewayId == a.GatewayId && p.Identify.Equals("L"))
                        let N= detectors.FirstOrDefault(p => p.GatewayId == a.GatewayId && p.Identify.Equals("N"))
                        let A = detectors.FirstOrDefault(p => p.GatewayId == a.GatewayId && p.Identify.Equals("I0"))
                        let L1 = detectors.FirstOrDefault(p => p.GatewayId == a.GatewayId && p.Identify.Equals("L1"))
                        let L2 = detectors.FirstOrDefault(p => p.GatewayId == a.GatewayId && p.Identify.Equals("L2"))
                        let L3 = detectors.FirstOrDefault(p => p.GatewayId == a.GatewayId && p.Identify.Equals("L3"))
                        select new FireElectricDeviceItemDto()
                        {
                            State=d.Status!=GatewayStatus.Online?"离线": a.State,
                            DeviceId = a.Id,
                            DeviceSn = a.DeviceSn,
                            FireUnitArchitectureFloorId = a.FireUnitArchitectureFloorId,
                            FireUnitArchitectureFloorName = b == null ? "" : b.Name,
                            FireUnitArchitectureId = a.FireUnitArchitectureId,
                            FireUnitArchitectureName = c == null ? "" : c.Name,
                            Location = a.Location,
                            PhaseType=a.PhaseType,
                            L=d.Status != GatewayStatus.Online?"未知":(L==null?"":L.State),
                            N = d.Status != GatewayStatus.Online ? "未知" : (N == null ? "" : N.State),
                            A = d.Status != GatewayStatus.Online ? "未知" : (A == null ? "" : A.State),
                            L1 = d.Status != GatewayStatus.Online ? "未知" : (L1 == null ? "" : L1.State),
                            L2 = d.Status != GatewayStatus.Online ? "未知" : (L2 == null ? "" : L2.State),
                            L3 = d.Status != GatewayStatus.Online ? "未知" : (L3 == null ? "" : L3.State),
                        };

            var query = query0.ToList();
            foreach (var v in query)
            {
                v.MonitorItem = new List<string>();
                var device = await _repFireElectricDevice.FirstOrDefaultAsync(v.DeviceId);
                if (device.ExistAmpere)
                    v.MonitorItem.Add("剩余电流");
                if (device.ExistTemperature)
                    v.MonitorItem.Add("电缆温度");
            }
            return await Task.Run<PagedResultDto<FireElectricDeviceItemDto>>(() =>
            {
                return new PagedResultDto<FireElectricDeviceItemDto>()
                {
                    Items = query.Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList(),
                    TotalCount = query.Count()
                };
            });
        }
        public async Task<PagedResultDto<FaultDetectorOutput>> GetFireAlarmFaultDetectorList(int DeviceId, PagedResultRequestDto dto)
        {
            var device = await _repFireAlarmDevice.FirstOrDefaultAsync(p => p.Id==DeviceId);
            if (device == null)
                return new PagedResultDto<FaultDetectorOutput>()
                {
                    Items = new List<FaultDetectorOutput>(),
                    TotalCount = 0
                };
            var fualtDetectors = from a in _detectorRep.GetAll().Where(p => p.GatewayId == device.GatewayId && p.FaultNum > 0)
                            join b0 in _faultRep.GetAll() on a.LastFaultId equals b0.Id into be
                            from b in be.DefaultIfEmpty()
                            select new
                            {
                                a,
                                b
                            };
            List<FaultDetectorOutput> lst = new List<FaultDetectorOutput>();
            foreach (var fault in fualtDetectors)
            {
                var type = await _detectorTypeRep.FirstOrDefaultAsync(fault.a.DetectorTypeId);
                var floor = await _repFireUnitArchitectureFloor.FirstOrDefaultAsync(fault.a.FireUnitArchitectureFloorId);
                FireUnitArchitecture arch = null;
                if (floor != null)
                    arch = await _repFireUnitArchitecture.FirstOrDefaultAsync(floor.ArchitectureId);

                lst.Add(new FaultDetectorOutput() {
                    State= fault.a.State,
                    DetectorId= fault.a.Id,
                    DetectorTypeName=type==null?"":type.Name,
                    FaultContent=fault.b==null?"": fault.b.FaultRemark,
                    FaultTime= fault.b == null ? "" : fault.b.CreationTime.ToString("yyyy-MM-dd"),
                    FireUnitArchitectureFloorName=floor==null?"":floor.Name,
                    FireUnitArchitectureFloorId=fault.a.FireUnitArchitectureFloorId,
                    FireUnitArchitectureId= arch==null?0: arch.Id,
                    FireUnitArchitectureName=arch==null?"":arch.Name,
                    Identify=fault.a.Identify,
                    Location=fault.a.Location
                });
            }
            return new PagedResultDto<FaultDetectorOutput>()
            {
                Items = lst.Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList(),
                TotalCount = lst.Count
            };
        }
        public async Task<PagedResultDto<GetFireAlarm30DayDto>> GetFireAlarm30DayList(int DeviceId, PagedResultRequestDto dto)
        {
            var device = await _repFireAlarmDevice.GetAsync(DeviceId);
            var gateway = await _gatewayRep.FirstOrDefaultAsync(p => p.Id == device.GatewayId);
            var alarm = _alarmToFireRep.GetAll().Where(p => p.GatewayId == device.GatewayId && p.CreationTime >= DateTime.Now.Date.AddDays(-30)).OrderByDescending(p=>p.CreationTime);
            var query = from a in alarm
                        join b in _detectorRep.GetAll() on a.DetectorId equals b.Id
                        join c in _detectorTypeRep.GetAll() on b.DetectorTypeId equals c.Id
                        join d0 in _repFireUnitArchitectureFloor.GetAll() on b.FireUnitArchitectureFloorId equals d0.Id into de
                        from d in de.DefaultIfEmpty()
                        select new GetFireAlarm30DayDto()
                        {
                            AlarmTime = a.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                            DetectorTypeName = c.Name,
                            FireUnitArchitectureFloorName = d==null?"":d.Name,
                            Identify = b.Identify,
                            Location = b.Location
                        };
            return await Task.Run<PagedResultDto<GetFireAlarm30DayDto>>(() =>
            {
                return new PagedResultDto<GetFireAlarm30DayDto>()
                {
                    Items = query.Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList(),
                    TotalCount = query.Count()
                };
            });
        }
        public async Task<PagedResultDto<GetFireAlarmHighDto>> GetFireAlarmHighList(int DeviceId, PagedResultRequestDto dto)
        {
            int highFreq = int.Parse(ConfigHelper.Configuration["FireDomain:HighFreqAlarm"]);
            var device = await _repFireAlarmDevice.GetAsync(DeviceId);
            var gateway = await _gatewayRep.FirstOrDefaultAsync(p => p.Id == device.GatewayId);
            var detectorHigh = _alarmToFireRep.GetAll().Where(p => p.GatewayId == device.GatewayId && p.CreationTime >= DateTime.Now.Date.AddDays(-30))
            .GroupBy(p=>p.DetectorId).Where(p=>p.Count()>highFreq).Select(p => new
            {
                DetectorId=p.Key,
                AlarmNum = p.Count()
            }).ToList();
            var query=from a in detectorHigh
                      join b in _detectorRep.GetAll() on a.DetectorId equals b.Id
                      join c in _detectorTypeRep.GetAll() on b.DetectorTypeId equals c.Id
                      join d0 in _repFireUnitArchitectureFloor.GetAll() on b.FireUnitArchitectureFloorId equals d0.Id into de
                      from d in de.DefaultIfEmpty()
                      select new GetFireAlarmHighDto()
                      {
                          DetectorTypeName = c.Name,
                          FireUnitArchitectureFloorName = d == null ? "" : d.Name,
                          Identify = b.Identify,
                          Location = b.Location,
                          AlarmNum=a.AlarmNum
                      };
            return new PagedResultDto<GetFireAlarmHighDto>()
            {
                Items = query.Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList(),
                TotalCount = query.Count()
            };
        }
        public async Task<PagedResultDto<FireAlarmDeviceItemDto>> GetFireAlarmDeviceList(int fireUnitId, PagedResultRequestDto dto)
        {
            int highFreq = int.Parse(ConfigHelper.Configuration["FireDomain:HighFreqAlarm"]);
            var groupGateway = _alarmToFireRep.GetAll().Where(p => p.FireUnitId == fireUnitId && p.CreationTime >= DateTime.Now.Date.AddDays(-30))
            .GroupBy(p => p.GatewayId).Select(p => new
            {
                GatewayId = p.Key,
                AlarmNum = p.Count(),
                High = p.GroupBy(p1 => p1.DetectorId).Where(p1 => p1.Count() > highFreq).Count()
            }).ToList();
            var groupGtFault = _detectorRep.GetAll().Where(p => p.FireUnitId == fireUnitId).GroupBy(p => p.GatewayId).Select(p => new
            {
                GatewayId = p.Key,
                FaultNum = p.Where(p1 => p1.FaultNum>0).Count(),
                FaultRate= p.Count()==0?"0%":(p.Where(p1 => p1.FaultNum>0).Count()/(double)p.Count()).ToString("P")
            }).ToList();

            var query = from a in _repFireAlarmDevice.GetAll().Where(p => p.FireUnitId == fireUnitId)
                        join b in _gatewayRep.GetAll().Where(p => p.FireUnitId == fireUnitId)
                        on a.GatewayId equals b.Id
                        join c in _repFireUnitArchitecture.GetAll()
                        on a.FireUnitArchitectureId equals c.Id
                        join d0 in groupGateway
                        on b.Id equals d0.GatewayId into dj
                        from d in dj.DefaultIfEmpty()
                        join e0 in groupGtFault
                        on b.Id equals e0.GatewayId into ej
                        from e in ej.DefaultIfEmpty()
                        orderby a.CreationTime descending
                        select new FireAlarmDeviceItemDto()
                        {
                            DeviceId=a.Id,
                            DeviceSn = a.DeviceSn,
                            State = b.Status == GatewayStatus.Online ? "在线" : "离线",
                            FireUnitArchitectureId = a.FireUnitArchitectureId,
                            FireUnitArchitectureName = c.Name,
                            NetDetectorNum = a.NetDetectorNum,
                            AlarmNum30Day = d == null ? 0 : d.AlarmNum,
                            HighAlarmDetectorNum = d == null ? 0 : d.High,
                            DetectorFaultRate = e == null ? "0%" : e.FaultRate,
                            FaultDetectorNum = e == null ? 0 : e.FaultNum
                        };
            return await Task.Run<PagedResultDto<FireAlarmDeviceItemDto>>(() =>
            {
                return new PagedResultDto<FireAlarmDeviceItemDto>()
                {
                    Items = query.Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList(),
                    TotalCount = query.Count()
                };
            });
        }
        public async Task<PagedResultDto<DetectorDto>> GetDeviceDetectorList(string DeviceSn, PagedResultRequestDto dto)
        {
            var device=await _repFireAlarmDevice.FirstOrDefaultAsync(p => p.DeviceSn.Equals(DeviceSn));
            if (device == null)
                return new PagedResultDto<DetectorDto>()
                {
                    Items = new List<DetectorDto>(),
                    TotalCount = 0
                };
            var detectors = _detectorRep.GetAll().Where(p => p.GatewayId == device.GatewayId).OrderByDescending(p=>p.CreationTime);
            List<DetectorDto> lst = new List<DetectorDto>();
            foreach(var v in detectors)
            {
                lst.Add( await GetDeviceDetector(v.Id));
            }
            return new PagedResultDto<DetectorDto>()
            {
                Items = lst.Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList(),
                TotalCount = lst.Count
            };
        }
        /// <summary>
        /// 查询防火单位网关状态列表
        /// </summary>
        /// <param name="fireSysType"></param>
        /// <param name="fireUnitId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GatewayStatusOutput>> GetFireUnitGatewaysStatus(int fireSysType, int fireUnitId, PagedResultRequestDto dto)
        {
            var status = _gatewayRep.GetAll().Where(p =>p.FireSysType== fireSysType && p.FireUnitId == fireUnitId).Select(p => new GatewayStatusOutput()
            {
                Gateway = p.Identify,
                Location = p.Location,
                Status = GatewayStatusNames.GetName(p.Status)
            });
            return await Task.Run<PagedResultDto<GatewayStatusOutput>>(() =>
            {
                return new PagedResultDto<GatewayStatusOutput>()
                {
                    TotalCount = status.Count(),
                    Items = status.Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList()
                };
            });
        }
        /// <summary>
        /// 非模拟量探测器历史记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<RecordUnAnalogOutput> GetRecordUnAnalog(GetRecordDetectorInput input)
        {
            var output = new RecordUnAnalogOutput();
            var detector =await _detectorRep.SingleAsync(p => p.Id == input.DetectorId);
            var detectorType = await _detectorTypeRep.SingleAsync(p => p.Id == detector.DetectorTypeId);
            output.Name = detectorType.Name;
            output.Location = detector.Location;
            var state=_recordOnlineRep.GetAll().Where(p => p.DetectorId == input.DetectorId).OrderByDescending(p => p.CreationTime).FirstOrDefault();
            if(state!=null)
            {
                output.State = GatewayStatusNames.GetName((GatewayStatus)state.State);
                output.LastTimeStateChange = state.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            List<string> dates = new List<string>();
            for(DateTime dt= input.Start; dt <= input.End; dt=dt.AddDays(1))
            {
                dates.Add(dt.ToString("yyyy-MM-dd"));
            }
            var onlines = from a in dates
                          join b in _recordOnlineRep.GetAll().Where(p => p.DetectorId == input.DetectorId && p.State == (sbyte)GatewayStatus.Offline && p.CreationTime >= input.Start && p.CreationTime <= input.End)
                          .GroupBy(p => p.CreationTime.ToString("yyyy-MM-dd"))
                          on a equals b.Key into u
                          from c in u.DefaultIfEmpty()
                          select new
                          {
                              Time = a,
                              Count = c == null ? 0 : c.Count()
                          };
            var gatawayDetector = _detectorRep.Single(p => p.Id == input.DetectorId);
            var gatway = _gatewayRep.Single(p => p.Identify == gatawayDetector.Identify && p.Origin == gatawayDetector.Origin);
            IQueryable<Detector> detectors = _detectorRep.GetAll().Where(p => p.GatewayId == gatway.Id);
            //报警包括UITD和下属部件的报警
            var alarmTofires = from a in detectors
                         join b in _alarmToFireRep.GetAll().Where(p => p.CreationTime >= input.Start && p.CreationTime <= input.End)
                         on a.Id equals b.DetectorId
                         select b;
            var alarms = from a in dates
                         join b in alarmTofires.GroupBy(p => p.CreationTime.ToString("yyyy-MM-dd"))
                         on a equals b.Key into u
                         from c in u.DefaultIfEmpty()
                         select new
                         {
                             Time = a,
                             Count = c == null ? 0 : c.Count()
                         };
            var faultdatas= from a in detectors
                            join b in _faultRep.GetAll().Where(p => p.CreationTime >= input.Start && p.CreationTime <= input.End)
                            on a.Id equals b.DetectorId
                            select b;
            var faults = from a in dates
                         join b in faultdatas.GroupBy(p => p.CreationTime.ToString("yyyy-MM-dd"))
                         on a equals b.Key into u
                         from c in u.DefaultIfEmpty()
                         select new
                         {
                             Time = a,
                             Count = c == null ? 0 : c.Count()
                         };
            int m = faults.Count();
            int m2 = alarms.Count();
            output.UnAnalogTimes = (from a in onlines
                                    join b in alarms
                                    on a.Time equals b.Time
                                    join c in faults
                                    on b.Time equals c.Time
                                    select new UnAnalogTime()
                                    {
                                        Time = a.Time,
                                        OfflineCount = a.Count,
                                        AlarmCount = b.Count,
                                        FaultCount = c.Count
                                    }).ToList();
            return output;
        }
        public async Task<GetRecordElectricOutput> GetRecordElectric(GetRecordElectricInput input)
        {
            DateTime end = DateTime.Now;
            if (input.End != null && input.End.Ticks != 0)
            {
                end = input.End;
            }
            DateTime start=end.Date.AddDays(-1);
            if (input.Start!=null&&input.Start.Ticks!=0)
            {
                start = input.Start;

            }
            var output = new GetRecordElectricOutput();
            try
            {
            var device = await _repFireElectricDevice.FirstOrDefaultAsync(p => p.Id == input.DeviceId);
            var detector = await _detectorRep.FirstOrDefaultAsync(p => p.Identify.Equals(input.Identify)&&p.GatewayId==device.GatewayId);
            if (input.Identify.Equals("剩余电流"))
                detector = await _detectorRep.FirstOrDefaultAsync(p => p.Identify.Equals("I0") && p.GatewayId == device.GatewayId);
            if (detector == null)
                return output;
            JObject jObject = JObject.Parse(device.PhaseJson);
            if (input.Identify.Equals("L") || input.Identify.Equals("L1") || input.Identify.Equals("L2") || input.Identify.Equals("L3") || input.Identify.Equals("N"))
            {
                output.Unit = "℃";
                output.MonitorItemName = "电缆温度 " + input.Identify;
                output.Min = jObject[$"{input.Identify}min"].ToString();
                output.Max = jObject[$"{input.Identify}max"].ToString();
            }else if (input.Identify.Equals("剩余电流"))
            {
                output.Unit = "mA";
                output.MonitorItemName = "剩余电流";
                output.Min = jObject["Amin"].ToString();
                output.Max = jObject["Amax"].ToString();
            }
            output.AnalogTimes = _recordAnalogRep.GetAll().Where(p => p.DetectorId == detector.Id&&p.CreationTime>=start&&p.CreationTime<=end).OrderByDescending(p => p.CreationTime).Select(p =>
                   new AnalogTime() { Time = p.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"), Value = p.Analog }).ToList();
            return output;

            }catch(Exception e)
            {
                return new GetRecordElectricOutput();
            }

        }
        /// <summary>
        /// 模拟量探测器历史记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<RecordAnalogOutput> GetRecordAnalog(GetRecordDetectorInput input)
        {
            var output = new RecordAnalogOutput();
            var detector = await _detectorRep.SingleAsync(p => p.Id == input.DetectorId);
            var detectorType = await _detectorTypeRep.SingleAsync(p => p.Id == detector.DetectorTypeId);
            output.Name = detectorType.Name;
            output.Location = detector.Location;
            var state = _recordOnlineRep.GetAll().Where(p => p.DetectorId == input.DetectorId).OrderByDescending(p => p.CreationTime).FirstOrDefault();
            if (state != null)
            {
                output.State = GatewayStatusNames.GetName((GatewayStatus)state.State);
                output.LastTimeStateChange = state.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            //output.AnalogTimes = _recordAnalogRep.GetAll().Where(p => p.DetectorId == input.DetectorId).OrderByDescending(p => p.CreationTime).Take(10).Select(p =>
            //       new AnalogTime() { Time = p.CreationTime.ToString("HH:mm:ss"), Value = p.Analog }).ToList();
            output.AnalogTimes = _recordAnalogRep.GetAll().Where(p => p.DetectorId == input.DetectorId).OrderByDescending(p => p.CreationTime).Select(p =>
                   new AnalogTime() { Time = p.CreationTime.ToString("HH:mm:ss"), Value = p.Analog }).ToList();
            return output;
        }
        /// <summary>
        /// 获取防火单位的终端状态
        /// </summary>
        /// <param name="fireUnitId"></param>
        /// <returns></returns>
        public async Task<PagedResultDeviceDto<EndDeviceStateOutput>> GetFireUnitEndDeviceState(int fireUnitId, int option, PagedResultRequestDto dto)
        {
            var uitd = (from a in _detectorRep.GetAll().Where(p => p.FireUnitId == fireUnitId && p.DetectorTypeId == GetDetectorType((byte)UnitType.UITD).Id
                       && (option == 0 ? true : (option == -1 ? p.State.Equals("离线") : !p.State.Equals("离线"))))
                        join b in _detectorTypeRep.GetAll()
                        on a.DetectorTypeId equals b.Id
                        select new EndDeviceStateOutput()
                        {
                            DetectorId = a.Id,
                            IsAnalog = false,
                           Name = b.Name,
                           Location = a.Location,
                           StateName = a.State,
                           Analog="-",
                           Standard="-"
                       }).ToList();
            var tem= (from a in _detectorRep.GetAll().Where(p => p.FireUnitId == fireUnitId && p.DetectorTypeId == GetDetectorType((byte)UnitType.ElectricTemperature).Id
                        && (option == 0 ? true : (option == -1 ? p.State.Equals("离线") : !p.State.Equals("离线"))))
                    join b in _detectorTypeRep.GetAll()
                     on a.DetectorTypeId equals b.Id
                     select new EndDeviceStateOutput()
                     {
                         IsAnalog=true,
                         DetectorId = a.Id,
                         Name = b.Name,
                         Location = a.Location,
                         StateName = a.State.Equals("离线")?"离线":"在线",
                         Analog= a.State.Equals("离线") ? "-":a.State
                     }).ToList();
            var setTem = await _fireSettingManager.GetByName("CableTemperature");
            foreach (var v in tem)
            {
                v.Standard = $"<={setTem.MaxValue}℃";
                double an;
                v.IsOverRange = double.TryParse(v.Analog, out an) ? an > setTem.MaxValue : false;
            }
            var ele = (from a in _detectorRep.GetAll().Where(p => p.FireUnitId == fireUnitId && p.DetectorTypeId == GetDetectorType((byte)UnitType.ElectricResidual).Id
                       && (option == 0 ? true : (option == -1 ? p.State.Equals("离线") : !p.State.Equals("离线"))))
                      join b in _detectorTypeRep.GetAll()
                      on a.DetectorTypeId equals b.Id
                      select new EndDeviceStateOutput()
                      {
                          IsAnalog = true,
                          DetectorId = a.Id,
                          Name = b.Name,
                          Location = a.Location,
                          StateName = a.State.Equals("离线") ? "离线" : "在线",
                          Analog = a.State.Equals("离线") ? "-" : a.State
                      }).ToList();
            var setEle = await _fireSettingManager.GetByName("ResidualCurrent");
            foreach (var v in ele)
            {
                v.Standard = $"<={setEle.MaxValue}mA";
                double an;
                v.IsOverRange = double.TryParse(v.Analog, out an) ? an> setEle.MaxValue : false;
            }
            var lst= uitd.Union(tem).Union(ele).ToList();
            return new PagedResultDeviceDto<EndDeviceStateOutput>()
            {
                OfflineCount = lst.Count(p => string.IsNullOrEmpty( p.StateName)||p.StateName.Equals("离线")),
                TotalCount = lst.Count,
                Items = lst.Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList()
            };
        }

        /// <summary>
        /// 新增探测器部件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddDetector(AddDetectorInput input)
        {
            var gateway = _gatewayRep.GetAll().Where(p => p.Identify == input.GatewayIdentify).FirstOrDefault();
            if (gateway == null)
                return ;
            var type = _detectorTypeRep.GetAll().Where(p => p.GBType == input.DetectorGBType).FirstOrDefault();
            if (type == null)
                return ;
             await _detectorRep.InsertAsync(new Detector()
            {
                DetectorTypeId = type.Id,
                FireSysType = gateway.FireSysType,
                Identify = input.Identify,
                Location =string.IsNullOrEmpty(input.Location)?gateway.Location: input.Location,
                GatewayId= gateway.Id,
                FireUnitId=gateway.FireUnitId,
                Origin=input.Origin
            });
        }
        /// <summary>
        /// 新增火警联网设备
        /// </summary>
        /// <param name="input"></param>
        /// <param name="origin">网关设备厂商</param>
        /// <returns></returns>
        public async Task<SuccessOutput> AddFireAlarmDevice(FireAlarmDeviceDto input,string origin)
        {
            var arch = await _repFireUnitArchitecture.FirstOrDefaultAsync(p => p.Id == input.FireUnitArchitectureId);
            if (arch == null)
                return new SuccessOutput() { Success = false, FailCause = "建筑不存在" };
            var gateway = await _gatewayRep.FirstOrDefaultAsync(p => p.Identify.Equals(input.DeviceSn) && p.Origin.Equals(origin));
            if(gateway!=null)
                return new SuccessOutput() { Success = false, FailCause = "设备编号已经存在" };
            try
            {
                var gatewayId = await _gatewayRep.InsertAndGetIdAsync(new Gateway()
                {
                    FireSysType = (byte)FireSysType.Fire,
                    Identify = input.DeviceSn,
                    Location = arch.Name,
                    FireUnitId = input.FireUnitId,
                    Origin = origin,
                    Status = GatewayStatus.UnKnow,
                    StatusChangeTime = DateTime.Now
                });
                await _repFireAlarmDevice.InsertAsync(new FireAlarmDevice()
                {
                    Brand = input.Brand,
                    DeviceSn = input.DeviceSn,
                    DeviceType = input.DeviceType,
                    EnableAlarm = input.EnableAlarm.Contains("云端报警"),
                    EnableAlarmSwitch = input.EnableAlarm.Contains("发送开关量信号"),
                    EnableFault = input.EnableFault.Contains("云端报警"),
                    EnableFaultSwitch = input.EnableFault.Contains("发送开关量信号"),
                    FireUnitArchitectureId = input.FireUnitArchitectureId,
                    FireUnitId = input.FireUnitId,
                    GatewayId = gatewayId,
                    NetDetectorNum = input.NetDetectorNum,
                    Protocol = input.Protocol,
                    NetComm=input.NetComm
                });
            }catch(Exception e)
            {
                return new SuccessOutput() { Success = false, FailCause = e.Message};
            }
            return new SuccessOutput() { Success = true };
        }
        public async Task<SuccessOutput> UpdateFireElectricDevice(UpdateFireElectricDeviceDto input)
        {
            var elec = await _repFireElectricDevice.FirstOrDefaultAsync(p => p.Id==input.DeviceId);
            if (elec == null)
                return new SuccessOutput() { Success = false, FailCause = $"不存在ID为{input.DeviceId}的设备" };
            var gateway = await _gatewayRep.FirstOrDefaultAsync(p => p.Id==elec.GatewayId);
            if (elec == null)
                return new SuccessOutput() { Success = false, FailCause = "设备网关被删除" };
            gateway.Identify = input.DeviceSn;
            gateway.Location = input.Location;
            await _gatewayRep.UpdateAsync(gateway);
            elec.NetComm = input.NetComm;
            elec.DeviceSn = input.DeviceSn;
            elec.DeviceType = input.DeviceType;
            elec.FireUnitArchitectureId = input.FireUnitArchitectureId;
            elec.EnableCloudAlarm = input.EnableAlarm.Contains("云端报警");
            elec.EnableEndAlarm = input.EnableAlarm.Contains("终端报警");
            elec.EnableAlarmSwitch = input.EnableAlarm.Contains("发送开关量信号");
            elec.ExistAmpere = input.MonitorItem.Contains("剩余电流");
            elec.ExistTemperature = input.MonitorItem.Contains("电缆温度");
            elec.FireUnitArchitectureFloorId = input.FireUnitArchitectureFloorId;
            elec.Location = input.Location;
            var aDetector=await _detectorRep.FirstOrDefaultAsync(p => p.GatewayId == elec.GatewayId && p.Identify.Equals("I0"));
            aDetector.FireUnitArchitectureFloorId = input.FireUnitArchitectureFloorId;
            aDetector.Location = input.Location;
            await _detectorRep.UpdateAsync(aDetector);
            var nDetector = await _detectorRep.FirstOrDefaultAsync(p => p.GatewayId == elec.GatewayId && p.Identify.Equals("N"));
            nDetector.FireUnitArchitectureFloorId = input.FireUnitArchitectureFloorId;
            nDetector.Location = input.Location;
            await _detectorRep.UpdateAsync(nDetector);

            var temperatureType = await _detectorTypeRep.FirstOrDefaultAsync(p => p.Name.Equals("电缆温度探测器"));
            string phase = "";
            if (input.PhaseType.Equals("单相"))
            {
                var lDetector=await _detectorRep.FirstOrDefaultAsync(p => p.GatewayId == elec.GatewayId && p.Identify.Equals("L"));
                if (lDetector == null)
                {
                    lDetector = new Detector()
                    {
                        DetectorTypeId = temperatureType.Id,
                        FaultNum = 0,
                        FireSysType = (byte)FireSysType.Electric,
                        FireUnitId = input.FireUnitId,
                        GatewayId = elec.GatewayId,
                        Identify = "L",
                        Origin = gateway.Origin
                    };
                }
                lDetector.FireUnitArchitectureFloorId = input.FireUnitArchitectureFloorId;
                lDetector.Location = input.Location;
                await _detectorRep.InsertOrUpdateAsync(lDetector);
                SinglePhase singlePhase = new SinglePhase()
                {
                    I0max = input.Amax,
                    I0min = input.Amin,
                    Lmax = input.Lmax,
                    Lmin = input.Lmin,
                    Nmax = input.Nmax,
                    Nmin = input.Nmin
                };
                phase = JsonConvert.SerializeObject(singlePhase);
            }
            else if (input.PhaseType.Equals("三相"))
            {
                var l3Detector = await _detectorRep.FirstOrDefaultAsync(p => p.GatewayId == elec.GatewayId && p.Identify.Equals("L3"));
                if (l3Detector == null)
                {
                    l3Detector = new Detector()
                    {
                        DetectorTypeId = temperatureType.Id,
                        FaultNum = 0,
                        FireSysType = (byte)FireSysType.Electric,
                        FireUnitId = input.FireUnitId,
                        GatewayId = elec.GatewayId,
                        Identify = "L",
                        Origin = gateway.Origin
                    };
                }
                l3Detector.FireUnitArchitectureFloorId = input.FireUnitArchitectureFloorId;
                l3Detector.Location = input.Location;
                await _detectorRep.InsertOrUpdateAsync(l3Detector);
                var l2Detector = await _detectorRep.FirstOrDefaultAsync(p => p.GatewayId == elec.GatewayId && p.Identify.Equals("L2"));
                if (l2Detector == null)
                {
                    l2Detector = new Detector()
                    {
                        DetectorTypeId = temperatureType.Id,
                        FaultNum = 0,
                        FireSysType = (byte)FireSysType.Electric,
                        FireUnitId = input.FireUnitId,
                        GatewayId = elec.GatewayId,
                        Identify = "L",
                        Origin = gateway.Origin
                    };
                }
                l2Detector.FireUnitArchitectureFloorId = input.FireUnitArchitectureFloorId;
                l2Detector.Location = input.Location;
                await _detectorRep.InsertOrUpdateAsync(l2Detector);
                var l1Detector = await _detectorRep.FirstOrDefaultAsync(p => p.GatewayId == elec.GatewayId && p.Identify.Equals("L1"));
                if (l1Detector == null)
                {
                    l1Detector = new Detector()
                    {
                        DetectorTypeId = temperatureType.Id,
                        FaultNum = 0,
                        FireSysType = (byte)FireSysType.Electric,
                        FireUnitId = input.FireUnitId,
                        GatewayId = elec.GatewayId,
                        Identify = "L",
                        Origin = gateway.Origin
                    };
                }
                l1Detector.FireUnitArchitectureFloorId = input.FireUnitArchitectureFloorId;
                l1Detector.Location = input.Location;
                await _detectorRep.InsertOrUpdateAsync(l1Detector);
                ThreePhase threePhase = new ThreePhase()
                {
                    I0max = input.Amax,
                    I0min = input.Amin,
                    L1max = input.L1max,
                    L1min = input.L1min,
                    L2max = input.L2max,
                    L2min = input.L2min,
                    L3max = input.L3max,
                    L3min = input.L3min,
                    Nmax = input.Nmax,
                    Nmin = input.Nmin
                };
                phase = JsonConvert.SerializeObject(threePhase);
            }
            elec.PhaseType = input.PhaseType;
            elec.PhaseJson = phase;
            await _repFireElectricDevice.UpdateAsync(elec);

            return new SuccessOutput() { Success = true };
        }
        //public async Task<UpdateFireElectricDeviceDto> GetFireElectricDevice(int DeviceId)
        //{

        //}
        /// <summary>
        /// 新增电气火灾设备
        /// </summary>
        /// <param name="input"></param>
        /// <param name="origin">网关设备厂商</param>
        /// <returns></returns>
        public async Task<SuccessOutput> AddFireElectricDevice(FireElectricDeviceDto input, string origin)
        {
            var arch = await _repFireUnitArchitecture.FirstOrDefaultAsync(p => p.Id == input.FireUnitArchitectureId);
            if (arch == null)
                return new SuccessOutput() { Success = false, FailCause = "建筑不存在" };
            var gateway = await _gatewayRep.FirstOrDefaultAsync(p => p.Identify.Equals(input.DeviceSn) && p.Origin.Equals(origin));
            if (gateway != null)
                return new SuccessOutput() { Success = false, FailCause = "设备编号已经存在" };
            try
            {
                var gatewayId = await _gatewayRep.InsertAndGetIdAsync(new Gateway()
                {
                    FireSysType = (byte)FireSysType.Electric,
                    Identify = input.DeviceSn,
                    Location = input.Location,
                    FireUnitId = input.FireUnitId,
                    Origin = origin,
                    Status = GatewayStatus.UnKnow,
                    StatusChangeTime = DateTime.Now
                });
                string phase = "";
                if (input.PhaseType.Equals("单相"))
                {
                    SinglePhase singlePhase = new SinglePhase()
                    {
                        I0max = input.Amax,
                        I0min = input.Amin,
                        Lmax = input.Lmax,
                        Lmin = input.Lmin,
                        Nmax = input.Nmax,
                        Nmin = input.Nmin
                    };
                    phase=JsonConvert.SerializeObject(singlePhase);
                }
                else if (input.PhaseType.Equals("三相"))
                {
                   ThreePhase threePhase = new ThreePhase()
                    {
                       I0max = input.Amax,
                       I0min = input.Amin,
                        L1max = input.L1max,
                        L1min = input.L1min,
                        L2max = input.L2max,
                        L2min = input.L2min,
                        L3max = input.L3max,
                        L3min = input.L3min,
                        Nmax = input.Nmax,
                        Nmin = input.Nmin
                    };
                    phase = JsonConvert.SerializeObject(threePhase);
                }
                await _repFireElectricDevice.InsertAsync(new FireElectricDevice()
                {
                    State="良好",
                    DeviceSn = input.DeviceSn,
                    DeviceType = input.DeviceType,
                    FireUnitArchitectureId = input.FireUnitArchitectureId,
                    FireUnitId = input.FireUnitId,
                    GatewayId = gatewayId,
                    EnableCloudAlarm=input.EnableAlarm.Contains("云端报警"),
                    EnableEndAlarm=input.EnableAlarm.Contains("终端报警"),
                    EnableAlarmSwitch= input.EnableAlarm.Contains("发送开关量信号"),
                    ExistAmpere =input.MonitorItem.Contains("剩余电流"),
                    ExistTemperature = input.MonitorItem.Contains("电缆温度"),
                    FireUnitArchitectureFloorId =input.FireUnitArchitectureFloorId,
                    Location=input.Location,
                    PhaseType=input.PhaseType,
                    PhaseJson= phase,
                    NetComm=input.NetComm
                });
                var ampereType = await _detectorTypeRep.FirstOrDefaultAsync(p => p.Name.Equals("剩余电流探测器"));
                var temperatureType = await _detectorTypeRep.FirstOrDefaultAsync(p => p.Name.Equals("电缆温度探测器"));
                if (ampereType == null || temperatureType == null)
                    return new SuccessOutput() { Success = false, FailCause = "数据库不存在‘剩余电流探测器’或‘电缆温度探测器’的类型" };
                var AId=await _detectorRep.InsertAndGetIdAsync(new Detector()
                {
                    DetectorTypeId = ampereType.Id,
                    FaultNum = 0,
                    FireSysType = (byte)FireSysType.Electric,
                    FireUnitArchitectureFloorId = input.FireUnitArchitectureFloorId,
                    FireUnitId = input.FireUnitId,
                    GatewayId = gatewayId,
                    Identify = "I0",
                    Location = input.Location,
                    Origin = origin
                });
                var NId = await _detectorRep.InsertAndGetIdAsync(new Detector()
                {
                    DetectorTypeId = temperatureType.Id,
                    FaultNum = 0,
                    FireSysType = (byte)FireSysType.Electric,
                    FireUnitArchitectureFloorId = input.FireUnitArchitectureFloorId,
                    FireUnitId = input.FireUnitId,
                    GatewayId = gatewayId,
                    Identify = "N",
                    Location = input.Location,
                    Origin = origin
                });
                if (input.PhaseType.Equals("单相"))
                {
                    var LId=await _detectorRep.InsertAndGetIdAsync(new Detector()
                    {
                        DetectorTypeId = temperatureType.Id,
                        FaultNum = 0,
                        FireSysType = (byte)FireSysType.Electric,
                        FireUnitArchitectureFloorId = input.FireUnitArchitectureFloorId,
                        FireUnitId = input.FireUnitId,
                        GatewayId = gatewayId,
                        Identify = "L",
                        Location = input.Location,
                        Origin = origin
                    });
                }
                else if(input.PhaseType.Equals("三相"))
                {
                    var L1Id = await _detectorRep.InsertAndGetIdAsync(new Detector()
                    {
                        DetectorTypeId = temperatureType.Id,
                        FaultNum = 0,
                        FireSysType = (byte)FireSysType.Electric,
                        FireUnitArchitectureFloorId = input.FireUnitArchitectureFloorId,
                        FireUnitId = input.FireUnitId,
                        GatewayId = gatewayId,
                        Identify = "L1",
                        Location = input.Location,
                        Origin = origin
                    });
                    var L2Id = await _detectorRep.InsertAndGetIdAsync(new Detector()
                    {
                        DetectorTypeId = temperatureType.Id,
                        FaultNum = 0,
                        FireSysType = (byte)FireSysType.Electric,
                        FireUnitArchitectureFloorId = input.FireUnitArchitectureFloorId,
                        FireUnitId = input.FireUnitId,
                        GatewayId = gatewayId,
                        Identify = "L2",
                        Location = input.Location,
                        Origin = origin
                    });
                    var L3Id = await _detectorRep.InsertAndGetIdAsync(new Detector()
                    {
                        DetectorTypeId = temperatureType.Id,
                        FaultNum = 0,
                        FireSysType = (byte)FireSysType.Electric,
                        FireUnitArchitectureFloorId = input.FireUnitArchitectureFloorId,
                        FireUnitId = input.FireUnitId,
                        GatewayId = gatewayId,
                        Identify = "L3",
                        Location = input.Location,
                        Origin = origin
                    });
                }
            }
            catch (Exception e)
            {
                return new SuccessOutput() { Success = false, FailCause = e.Message };
            }
            return new SuccessOutput() { Success = true };
        }
        public async Task<SuccessOutput> UpdateFireOrtherDevice(UpdateFireOrtherDeviceDto input)
        {
            var device = await _repFireOrtherDevice.FirstOrDefaultAsync(p => p.Id == input.DeviceId);
            if(device==null)
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
        public async Task<GetFireOrtherDeviceOutput> GetFireOrtherDevice(int Deviceid)
        {
            var device = await _repFireOrtherDevice.FirstOrDefaultAsync(p => p.Id == Deviceid);
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
                FireUnitArchitectureFloorName=floor==null?"":floor.Name,
                FireUnitArchitectureName= arch==null?"": arch.Name
            };
        }
        public async Task<GetFireOrtherDeviceExpireOutput> GetFireOrtherDeviceExpire(int FireUnitId)
        {
            var devices = _repFireOrtherDevice.GetAll().Where(p => p.FireUnitId == FireUnitId);
            return await Task.Run<GetFireOrtherDeviceExpireOutput>(() =>
          {
              return new GetFireOrtherDeviceExpireOutput()
              {
                  ExpireNum = devices.Where(p => DateTime.Now >= p.ExpireTime).Count(),
                  WillExpireNum = devices.Where(p => DateTime.Now >= p.ExpireTime.AddDays(-30) && DateTime.Now < p.ExpireTime).Count()
              };
          });
        }
        public async Task<PagedResultDto<FireOrtherDeviceItemDto>> GetFireOrtherDeviceList(int fireUnitId,string ExpireType,string FireUnitArchitectureName, PagedResultRequestDto dto)
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
                            FireUnitArchitectureName = b == null ? "":b.Name,
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
                    DeviceName=input.DeviceName,
                    FireSystemId=input.FireSystemId,
                    StartTime=DateTime.Parse(input.StartTime),
                    ExpireTime=DateTime.Parse(input.ExpireTime)
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

                    // 验证表格数据的正确性
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow row = dt.Rows[i];
                        if (string.IsNullOrEmpty(row["设备编号"].ToString()))
                        {
                            strMsg = $"第{i + 1}行设备编号不能为空";
                        }
                        else if (string.IsNullOrEmpty(row["设备名称"].ToString()))
                        {
                            strMsg = $"第{i + 1}行设备名称不能为空";
                        }
                        else if (!string.IsNullOrEmpty(row["所属系统"].ToString()) && lstFireUnitSystem.Find(d => d.SystemName.Equals(row["所属系统"].ToString().Trim())) == null)
                        {
                            strMsg = $"第{i + 1}行所属系统在后台数据中不存在";
                        }
                        else if (!string.IsNullOrEmpty(row["所在建筑"].ToString()) && lstFireUnitArchitecture.Find(d => d.Name.Equals(row["所在建筑"].ToString().Trim())) == null)
                        {
                            strMsg = $"第{i + 1}行所在建筑在后台数据中不存在";
                        }
                        else if (!string.IsNullOrEmpty(row["所在建筑"].ToString()) && !string.IsNullOrEmpty(row["楼层"].ToString()) && lstFireUnitArchitecture.Find(d => d.Name.Equals(row["所在建筑"].ToString().Trim())).Floors.Find(f => f.Name.Equals(row["楼层"].ToString().Trim())) == null)
                        {
                            strMsg = $"第{i + 1}行楼层在后台数据中不存在";
                        }
                    }
                    if (!string.IsNullOrEmpty(strMsg))
                    {
                        output.Success = false;
                        output.FailCause = strMsg;
                        return output;
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow row = dt.Rows[i];
                        await _repFireOrtherDevice.InsertAsync(new FireOrtherDevice()
                        {
                            FireUnitId = input.FireUnitId,
                            DeviceSn = row["设备编号"].ToString().Trim(),
                            DeviceType = row["设备型号"].ToString().Trim(),
                            FireUnitArchitectureId = !string.IsNullOrEmpty(row["所在建筑"].ToString().Trim()) ? lstFireUnitArchitecture.Find(d=>d.Name.Equals(row["所在建筑"].ToString().Trim())).Id : 0,
                            FireUnitArchitectureFloorId = !string.IsNullOrEmpty(row["所在建筑"].ToString().Trim()) && !string.IsNullOrEmpty(row["楼层"].ToString().Trim()) ? lstFireUnitArchitecture.Find(d => d.Name.Equals(row["所在建筑"].ToString().Trim())).Floors.Find(f=>f.Name.Equals(row["楼层"].ToString().Trim())).Id : 0,
                            Location = row["具体位置"].ToString().Trim(),
                            DeviceName = row["设备名称"].ToString().Trim(),
                            FireSystemId = !string.IsNullOrEmpty(row["所属系统"].ToString().Trim()) ? lstFireUnitSystem.Find(d => d.SystemName.Equals(row["所属系统"].ToString().Trim())).FireSystemId : 0,
                            StartTime = DateTime.Parse(row["启用时间"].ToString().Trim()),
                            ExpireTime = DateTime.Parse(row["有效期"].ToString().Trim())
                        });
                    }
                }
            }
            output.Success = true;
            return output;
        }

        /// <summary>
        /// 新增网关设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddGateway(AddGatewayInput input)
        {
            await _gatewayRep.InsertAsync(new Gateway()
            {
                FireSysType = input.FireSysType,
                Identify = input.Identify,
                Location = input.Location,
                FireUnitId = input.FireUnitId,
                Origin=input.Origin
            });
        }
        public async Task<DetectorType> GetDetectorTypeAsync(int id)
        {
            return await _detectorTypeRep.SingleAsync(p => p.Id == id);
        }
        public DetectorType GetDetectorType(byte GBtype)
        {
            return _detectorTypeRep.GetAll().Where(p => p.GBType == GBtype).FirstOrDefault();
        }
        public async Task<Detector> GetDetectorAsync(int id)
        {
            return await _detectorRep.SingleAsync(p=>p.Id==id);
        }

        public async Task<DetectorDto> GetDeviceDetector(int detectorId)
        {
            var detector = await _detectorRep.GetAsync(detectorId);
            var type = await _detectorTypeRep.FirstOrDefaultAsync(detector.DetectorTypeId);
            var floor = await _repFireUnitArchitectureFloor.FirstOrDefaultAsync(detector.FireUnitArchitectureFloorId);
            FireUnitArchitecture arch=null;
            if (floor != null)
                arch = await _repFireUnitArchitecture.FirstOrDefaultAsync(floor.ArchitectureId);
            return new DetectorDto()
            {
                DetectorId=detector.Id,
                DetectorTypeName = type == null ? "" : type.Name,
                FireUnitArchitectureFloorId = detector.FireUnitArchitectureFloorId,
                FireUnitArchitectureFloorName = floor == null ? "" : floor.Name,
                FireUnitArchitectureId = floor == null ? 0 : floor.ArchitectureId,
                FireUnitArchitectureName = arch == null ? "" : arch.Name,
                Identify = detector.Identify,
                Location = detector.Location,
                State = detector.FaultNum > 0 ? "故障" : "正常"
            };
        }
        public Detector GetDetector(string identify, string origin)
        {
            return _detectorRep.GetAll().Where(p => p.Identify.Equals(identify)&&p.Origin.Equals(origin)).FirstOrDefault();
        }
        public Gateway GetGateway(string gatewayIdentify, string origin)
        {
            return _gatewayRep.GetAll().Where(p => p.Identify.Equals(gatewayIdentify) && p.Origin.Equals(origin)).FirstOrDefault();
        }
        public IQueryable<Detector> GetDetectorAll(int fireunitid, FireSysType fireSysType)
        {
            return _detectorRep.GetAll().Where(p => p.FireSysType == (byte)fireSysType && p.FireUnitId==fireunitid);
        }
        public IQueryable<Detector> GetDetectorElectricAll()
        {
            return _detectorRep.GetAll().Where(p=>p.FireSysType==(byte)FireSysType.Electric);
        }
        public IQueryable<DetectorType> GetDetectorTypeAll()
        {
            return _detectorTypeRep.GetAll();
        }

        public async Task<AddDataOutput> AddRecordAnalog(AddDataElecInput input)
        {
            var detector = GetDetector(input.Identify, input.Origin);
            if (detector == null)
            {
                return new AddDataOutput()
                {
                    IsDetectorExit = false
                };
            }
            detector.State = $"{input.Analog}{input.Unit}";
            await _detectorRep.UpdateAsync(detector);
            await _recordAnalogRep.InsertAsync(new RecordAnalog()
            {
                Analog = input.Analog,
                DetectorId = detector.Id
            });
            var device = await _repFireElectricDevice.FirstOrDefaultAsync(p => p.GatewayId == detector.GatewayId);
            if(!string.IsNullOrEmpty(device.PhaseJson))
            {
                var jobject = JObject.Parse(device.PhaseJson);
                if (jobject.ContainsKey($"{input.Identify}max"))
                {
                    var max = double.Parse(jobject[$"{input.Identify}max"].ToString());
                    if (input.Analog >= max)
                        device.State = "超限";
                    else if (input.Analog >= max * 0.8)
                        device.State = "隐患";
                    else
                        device.State = "良好";
                    await _repFireElectricDevice.UpdateAsync(device);
                }
            }
            return new AddDataOutput() { IsDetectorExit = true };
        }
        public async Task<AddDataOutput> AddOnlineDetector(AddOnlineDetectorInput input)
        {
            var detector = GetDetector(input.Identify, input.Origin);
            if (detector == null)
            {
                return new AddDataOutput()
                {
                    IsDetectorExit = false
                };
            }
            await _recordOnlineRep.InsertAsync(new RecordOnline()
            {
                State=(sbyte)(input.IsOnline?GatewayStatus.Online:GatewayStatus.Offline),
                DetectorId = detector.Id
            });
            return new AddDataOutput() { IsDetectorExit = true };
        }
        public async Task AddOnlineGateway(AddOnlineGatewayInput input)
        {
            var gateway = GetGateway(input.Identify, input.Origin);
            var detectors = _detectorRep.GetAll().Where(p => p.GatewayId == gateway.Id);
            foreach(var v in detectors)
            {
                await _recordOnlineRep.InsertAsync(new RecordOnline()
                {
                    State = (sbyte)(input.IsOnline ? GatewayStatus.Online : GatewayStatus.Offline),
                    DetectorId = v.Id
                });
                v.State = input.IsOnline ? "在线":"离线";
                await _detectorRep.UpdateAsync(v);
            }
        }
        /// <summary>
        /// 获取火警联网设备型号数组
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetFireAlarmDeviceTypes()
        {
            return await Task.Run<List<string>>(() =>{
                return _repFireAlarmDeviceType.GetAll().OrderBy(p => p.Name).Select(p => p.Name).ToList();
            });
        }
        /// <summary>
        /// 获取电气火灾设备型号数组
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetFireElectricDeviceTypes()
        {
            return await Task.Run<List<string>>(() => {
                return _repFireElectricDeviceType.GetAll().OrderBy(p => p.Name).Select(p => p.Name).ToList();
            });
        }
        /// <summary>
        /// 获取火灾报警控制器协议数组
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetFireAlarmDeviceProtocols()
        {
            return await Task.Run<List<string>>(() => {
                return _repFireAlarmDeviceProtocol.GetAll().OrderBy(p => p.Name).Select(p => p.Name).ToList();
            });
        }
        public async Task<SerialPortParamDto> GetSerialPortParam(int DeviceId)
        {
            return await Task.FromResult<SerialPortParamDto>(new SerialPortParamDto()
            {
                ComType="RS232",
                StopBit="1",
                DataBit="00001",
                ParityBit=1,
                Rate="2400"
            });
        }
    }
}
