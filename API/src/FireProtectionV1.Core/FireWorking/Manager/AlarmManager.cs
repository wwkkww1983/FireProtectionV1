using Abp.Domain.Repositories;
using FireProtectionV1.Common.Enum;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.FireWorking.Manager
{
    public class AlarmManager:IAlarmManager
    {
        IDeviceManager _deviceManager;
        IRepository<AlarmCheck> _alarmCheckRep;
        IRepository<AlarmToFire> _alarmToFireRep;
        IRepository<AlarmToElectric> _alarmToElectricRep;
        public AlarmManager(
            IRepository<AlarmCheck> alarmCheckRep,
            IDeviceManager deviceManager,
            IRepository<AlarmToFire> alarmToFireRep,
            IRepository<AlarmToElectric> alarmToElectricRep
        )
        {
            _alarmCheckRep = alarmCheckRep;
            _deviceManager = deviceManager;
            _alarmToElectricRep = alarmToElectricRep;
            _alarmToFireRep = alarmToFireRep;
        }
        public IQueryable< AlarmToElectric> GetNewElecAlarm(DateTime startTime)
        {
            return _alarmToElectricRep.GetAll().Where(p => p.CreationTime > startTime);
        }
        /// <summary>
        /// 新增安全用电报警
        /// </summary>
        /// <param name="input"></param>
        /// <param name="alarmLimit"></param>
        /// <returns></returns>
        public async Task<AddDataOutput> AddAlarmElec(AddDataElecInput input,string alarmLimit)
        {
            Detector detector = _deviceManager.GetDetector(input.Identify,input.Origin);
            if (detector == null)
            {
                return new AddDataOutput()
                {
                    IsDetectorExit = false
                };
            }
            await _alarmToElectricRep.InsertAsync(new AlarmToElectric() {
                FireUnitId = detector.FireUnitId,
                DetectorId=detector.Id,
                Analog=input.Analog,
                AlarmLimit=alarmLimit,
                Unit=input.Unit
            });
            return new AddDataOutput() { IsDetectorExit = true }
;        }
        /// <summary>
        /// 新增火灾监控设备报警
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AddDataOutput> AddAlarmFire(AddAlarmFireInput input)
        {
            Detector detector = _deviceManager.GetDetector(input.Identify,input.Origin);
            if (detector == null)
            {
                return new AddDataOutput()
                {
                    IsDetectorExit = false
                };
            }
            await _alarmToFireRep.InsertAsync(new AlarmToFire()
            {
                FireUnitId= detector.FireUnitId,
                DetectorId= detector.Id
            });
            return new AddDataOutput()
            {
                IsDetectorExit = true
            };
        }
        public async Task<List<AlarmCheckOutput>> GetAlarmChecks(int fireunitid)
        {
            var elec = from a in _alarmToElectricRep.GetAll().Where(p => p.FireUnitId == fireunitid)
                       join b in _deviceManager.GetDetectorAll(fireunitid, FireSysType.Electric)
                       on a.DetectorId equals b.Id
                       join c in _deviceManager.GetDetectorTypeAll()
                       on b.DetectorTypeId equals c.Id
                       join d in _alarmCheckRep.GetAll().Where(p => p.FireUnitId == fireunitid && p.FireSysType == (byte)FireSysType.Electric)
                       on a.Id equals d.AlarmDataId
                       select new AlarmCheckOutput()
                       {
                           CheckId=d.Id,
                           Time = a.CreationTime,
                           Alarm = $"{c.Name} {a.Analog}{a.Unit} 【标准:{a.AlarmLimit}{a.Unit}】",
                           Location = b.Location,
                           CheckStateValue = d.CheckState,
                           CheckStateName = CheckStateTypeNames.GetName((CheckStateType)d.CheckState)
                       };
            var fire= from a in _alarmToFireRep.GetAll().Where(p => p.FireUnitId == fireunitid)
                      join b in _deviceManager.GetDetectorAll(fireunitid, FireSysType.Fire)
                      on a.DetectorId equals b.Id
                      join c in _deviceManager.GetDetectorTypeAll()
                      on b.DetectorTypeId equals c.Id
                      join d in _alarmCheckRep.GetAll().Where(p => p.FireUnitId == fireunitid && p.FireSysType == (byte)FireSysType.Fire)
                      on a.Id equals d.AlarmDataId
                      select new AlarmCheckOutput()
                      {
                          CheckId = d.Id,
                          Time = a.CreationTime,
                          Alarm = $"{c.Name}",
                          Location = b.Location,
                          CheckStateValue = d.CheckState,
                          CheckStateName = CheckStateTypeNames.GetName((CheckStateType)d.CheckState)
                      };
            List<AlarmCheckOutput> all=new List<AlarmCheckOutput>();
            await Task.Run(() =>
            {
                all = elec.ToList().Union(fire.ToList()).OrderByDescending(p => p.Time).ToList();
            });
            return all;
        }

        public async Task<AlarmCheckInput> GetAlarmCheckDetail(int checkId)
        {
            throw new NotImplementedException();
        }
    }
}
