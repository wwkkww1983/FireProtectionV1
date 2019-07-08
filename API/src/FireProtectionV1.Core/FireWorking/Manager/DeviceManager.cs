using Abp.Domain.Repositories;
using DeviceServer.Tcp.Protocol;
using FireProtectionV1.Common.Enum;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Model;
using FireProtectionV1.SettingCore.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.FireWorking.Manager
{
    public class DeviceManager : IDeviceManager
    {
        IRepository<Fault> _faultRep;
        IRepository<AlarmToFire> _alarmToFireRep;
        IRepository<RecordOnline> _recordOnlineRep;
        IRepository<RecordAnalog> _recordAnalogRep;
        IFireSettingManager _fireSettingManager;
        IRepository<DetectorType> _detectorTypeRep;
        IRepository<Gateway> _gatewayRep;
        IRepository<Detector> _detectorRep;
        public DeviceManager(
            IRepository<Fault> faultRep,
            IRepository<AlarmToFire> alarmToFireRep,
            IRepository<RecordOnline> recordOnlineRep,
            IRepository<RecordAnalog> recordAnalogRep,
            IFireSettingManager fireSettingManager,
            IRepository<Detector> detectorRep,
             IRepository<DetectorType> detectorTypeRep,
           IRepository<Gateway> gatewayRep)
        {
            _faultRep = faultRep;
            _alarmToFireRep = alarmToFireRep;
            _recordOnlineRep = recordOnlineRep;
            _recordAnalogRep = recordAnalogRep;
            _fireSettingManager = fireSettingManager;
            _detectorTypeRep = detectorTypeRep;
            _detectorRep = detectorRep;
            _gatewayRep = gatewayRep;
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
            for(DateTime dt= input.Start; dt <= input.End; dt.AddDays(1))
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
            output.AnalogTimes = _recordAnalogRep.GetAll().Where(p => p.DetectorId == input.DetectorId).OrderByDescending(p => p.CreationTime).Take(10).Select(p =>
                   new AnalogTime() { Time = p.CreationTime.ToString("yyyy-MM-dd HH:mm"), Value = p.Analog }).ToList();
            return output;
        }
        /// <summary>
        /// 获取防火单位的终端状态
        /// </summary>
        /// <param name="fireUnitId"></param>
        /// <returns></returns>
        public async Task<List<EndDeviceStateOutput>> GetFireUnitEndDeviceState(int fireUnitId, int option)
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
            }
            return uitd.Union(tem).Union(ele).ToList();
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
                Location =input.Location,
                GatewayId= gateway.Id,
                FireUnitId=gateway.FireUnitId,
                Origin=input.Origin
            });
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
            await _recordAnalogRep.InsertAsync(new RecordAnalog()
            {
                Analog = input.Analog,
                DetectorId = detector.Id
            });
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
    }
}
