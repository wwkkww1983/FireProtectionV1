using Abp.Domain.Repositories;
using FireProtectionV1.Common.Enum;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Model;
using FireProtectionV1.User.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.FireWorking.Manager
{
    public class AlarmManager:IAlarmManager
    {
        IFireUnitUserManager _fireUnitUserManager;
        IDeviceManager _deviceManager;
        IRepository<AlarmCheck> _alarmCheckRep;
        IRepository<AlarmToFire> _alarmToFireRep;
        IRepository<AlarmToElectric> _alarmToElectricRep;
        public AlarmManager(
            IFireUnitUserManager fireUnitUserManager,
            IRepository<AlarmCheck> alarmCheckRep,
            IDeviceManager deviceManager,
            IRepository<AlarmToFire> alarmToFireRep,
            IRepository<AlarmToElectric> alarmToElectricRep
        )
        {
            _fireUnitUserManager = fireUnitUserManager;
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
            int id= _alarmToElectricRep.InsertAndGetId(new AlarmToElectric() {
                FireUnitId = detector.FireUnitId,
                DetectorId=detector.Id,
                Analog=input.Analog,
                AlarmLimit=alarmLimit,
                Unit=input.Unit
            });
            await _alarmCheckRep.InsertAsync(new AlarmCheck()
            {
                AlarmDataId = id,
                FireSysType=detector.FireSysType,
                FireUnitId = detector.FireUnitId,
                CheckState = (byte)CheckStateType.UnCheck
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
            int id= _alarmToFireRep.InsertAndGetId(new AlarmToFire()
            {
                FireUnitId= detector.FireUnitId,
                DetectorId= detector.Id
            });
            await _alarmCheckRep.InsertAsync(new AlarmCheck()
            {
                AlarmDataId = id,
                FireSysType = detector.FireSysType,
                FireUnitId = detector.FireUnitId,
                CheckState = (byte)CheckStateType.UnCheck
            });
            return new AddDataOutput()
            {
                IsDetectorExit = true
            };
        }
        private string AlarmName(DetectorType type, AlarmToFire alarmToFire)
        {
            return $"{type.Name}";
        }
        private string AlarmName(DetectorType type,AlarmToElectric alarmToElectric)
        {
            return $"{type.Name} {alarmToElectric.Analog}{alarmToElectric.Unit} 【标准:{alarmToElectric.AlarmLimit}】";
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
                           Time = a.CreationTime.ToString("yyyy-MM-dd HH:mm"),
                           Alarm = AlarmName(c,a),
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
                          Time = a.CreationTime.ToString("yyyy-MM-dd HH:mm"),
                          Alarm = AlarmName(c,a),
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
        /// <summary>
        /// 保存核警信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task CheckAlarm(AlarmCheckDetailDto dto)
        {
            var alarmCheck = await _alarmCheckRep.SingleAsync(p => p.Id == dto.CheckId);
            alarmCheck.UserId = dto.UserId;
            alarmCheck.CreationTime = DateTime.Now;
            alarmCheck.CheckState = dto.CheckState;
            alarmCheck.Content = dto.Content;
            if (dto.PictureUrl_1 != null)
                alarmCheck.PicturUrls= dto.PictureUrl_1;
            if (dto.PictureUrl_2 != null)
                alarmCheck.PicturUrls = string.IsNullOrEmpty(alarmCheck.PicturUrls) ? "" : "," + dto.PictureUrl_2;
            if (dto.PictureUrl_3 != null)
                alarmCheck.PicturUrls = string.IsNullOrEmpty(alarmCheck.PicturUrls) ? "" : "," + dto.PictureUrl_3;
            alarmCheck.VioceUrl = dto.VioceUrl;
            await _alarmCheckRep.UpdateAsync(alarmCheck);
        }
        public void RepairData()
        {
            var elec = _alarmToElectricRep.GetAll().Select(p =>
              new
              {
                  p.CreationTime,
                  DataId = p.Id,
                  p.FireUnitId,
                  FireSysType=(byte)FireSysType.Electric
              });
            var fire = _alarmToFireRep.GetAll().Select(p =>
              new
              {
                  p.CreationTime,
                  DataId = p.Id,
                  p.FireUnitId,
                  FireSysType = (byte)FireSysType.Fire
              });
            var all = elec.ToList().Union(fire.ToList()).OrderBy(p => p.CreationTime);
            foreach(var v in all)
            {
                _alarmCheckRep.InsertAndGetId(new AlarmCheck()
                {
                    CreationTime = v.CreationTime,
                    FireSysType = v.FireSysType,
                    FireUnitId = v.FireUnitId,
                    AlarmDataId = v.DataId,
                    CheckState = (byte)CheckStateType.UnCheck
                });
            }
        }
        public async Task<AlarmCheckDetailOutput> GetAlarmCheckDetail(int checkId)
        {
            var dto = new AlarmCheckDetailOutput();
            AlarmCheck alarmCheck = await _alarmCheckRep.SingleAsync(p => p.Id == checkId);
            if (alarmCheck.FireSysType == (byte)FireSysType.Electric)
            {
                var alarm = await _alarmToElectricRep.SingleAsync(p => p.Id == alarmCheck.AlarmDataId);
                var detector = await _deviceManager.GetDetectorAsync(alarm.DetectorId);
                var type = await _deviceManager.GetDetectorTypeAsync(detector.DetectorTypeId);
                dto.CheckId = checkId;
                dto.Time = alarm.CreationTime.ToString("yyyy-MM-dd HH:mm");
                dto.Alarm = AlarmName(type, alarm);
                dto.Location = detector.Location;
            }
            else if (alarmCheck.FireSysType == (byte)FireSysType.Fire)
            {
                var alarm = await _alarmToFireRep.SingleAsync(p => p.Id == alarmCheck.AlarmDataId);
                var detector = await _deviceManager.GetDetectorAsync(alarm.DetectorId);
                var type = await _deviceManager.GetDetectorTypeAsync(detector.DetectorTypeId);
                dto.CheckId = checkId;
                dto.Time = alarm.CreationTime.ToString("yyyy-MM-dd HH:mm");
                dto.Alarm = AlarmName(type, alarm);
                dto.Location = detector.Location;
            }
            dto.CheckStateValue = alarmCheck.CheckState;
            dto.CheckStateName = CheckStateTypeNames.GetName((CheckStateType)alarmCheck.CheckState);
            dto.Content = alarmCheck.Content;
            if (!string.IsNullOrEmpty(alarmCheck.PicturUrls))
            {
                string[] ss = alarmCheck.PicturUrls.Split(',');
                if (ss.Count() > 0)
                    dto.PictureUrl_1 = ss[0];
                if (ss.Count() > 1)
                    dto.PictureUrl_2 = ss[1];
                if (ss.Count() > 2)
                    dto.PictureUrl_3 = ss[2];
            }
            dto.VioceUrl = alarmCheck.VioceUrl;
            try
            {
                var user = await _fireUnitUserManager.GetUserInfo(new User.Dto.GetUnitPeopleInput() { AccountID = alarmCheck.UserId });
                dto.UserName = user.Name;
                dto.UserPhone = user.Account;
            }catch(Exception e )
            {

            }
            dto.CheckTime = alarmCheck.CreationTime.ToString("yyyy-MM-dd HH:mm");
            return dto;
        }
    }
}
