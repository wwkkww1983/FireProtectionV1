using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using FireProtectionV1.Common.Enum;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Model;
using FireProtectionV1.User.Manager;
using GovFire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.FireWorking.Manager
{
    public class AlarmManager:IAlarmManager
    {
        IRepository<PhotosPathSave> _photosPathSaveRep;
        IFireUnitUserManager _fireUnitUserManager;
        IDeviceManager _deviceManager;
        IRepository<AlarmCheck> _alarmCheckRep;
        IRepository<AlarmToFire> _alarmToFireRep;
        IRepository<AlarmToElectric> _alarmToElectricRep;
        IRepository<FireUnit> _repFireUnit;
        public AlarmManager(
            IRepository<FireUnit> repFireUnit,
            IRepository<PhotosPathSave> photosPathSaveRep,
            IFireUnitUserManager fireUnitUserManager,
            IRepository<AlarmCheck> alarmCheckRep,
            IDeviceManager deviceManager,
            IRepository<AlarmToFire> alarmToFireRep,
            IRepository<AlarmToElectric> alarmToElectricRep
        )
        {
            _repFireUnit = repFireUnit;
            _photosPathSaveRep = photosPathSaveRep;
            _fireUnitUserManager = fireUnitUserManager;
            _alarmCheckRep = alarmCheckRep;
            _deviceManager = deviceManager;
            _alarmToElectricRep = alarmToElectricRep;
            _alarmToFireRep = alarmToFireRep;
        }
        public IQueryable<AlarmToFire> GetAlarms(IQueryable<Detector> detectors, DateTime start, DateTime end)
        {
            return from a in detectors
                   join b in _alarmToFireRep.GetAll().Where(p => p.CreationTime >= start && p.CreationTime <= end)
                   on a.Id equals b.DetectorId
                   select b;
        }
        public IQueryable< AlarmToElectric> GetNewElecAlarm(DateTime startTime)
        {
            return _alarmToElectricRep.GetAll().Where(p => p.CreationTime > startTime&&p.CreationTime<=DateTime.Now);
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
            var detectorType = _deviceManager.GetDetectorType(input.DetectorGBType);
            var fireunit = await _repFireUnit.FirstOrDefaultAsync(detector.FireUnitId);
            var alarmDto = new GovFire.Dto.AlarmDto()
            {
                additionalinfo = $"当前值{input.Analog}{input.Unit}警戒值{alarmLimit}",
                alarmtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                devicelocation = detector.Location,
                devicesn = detector.Identify,
                devicetype = detectorType == null ? "" : detectorType.Name,
                firecompany = fireunit == null ? "" : fireunit.Name,
                lat = fireunit == null ? "" : fireunit.Lat.ToString(),
                lon = fireunit == null ? "" : fireunit.Lng.ToString()
            };
            var dataid=DataApi.UpdateAlarm(alarmDto);
            if (!string.IsNullOrEmpty(dataid))
            {
                DataApi.UpdateEvent(new GovFire.Dto.EventDto()
                {
                    id = id.ToString(),
                    state = "0",
                    createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    donetime = "",
                    eventcontent = $"{alarmDto.devicelocation},{alarmDto.devicesn},{alarmDto.devicetype}发生预警",
                    eventtype = "电气火灾系统报警数据",
                    firecompany = alarmDto.firecompany,
                    lat = alarmDto.lat,
                    lon = alarmDto.lon,
                    fireUnitId=dataid
                });
            }
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
            var detectorType =await _deviceManager.GetDetectorTypeAsync(detector.DetectorTypeId);
            var fireunit =await _repFireUnit.FirstOrDefaultAsync(detector.FireUnitId);
            var alarmDto = new GovFire.Dto.AlarmDto()
            {
                additionalinfo = "",
                alarmtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                devicelocation = detector.Location,
                devicesn = detector.Identify,
                devicetype = detectorType == null ? "" : detectorType.Name,
                firecompany = fireunit == null ? "" : fireunit.Name,
                lat = fireunit == null ? "" : fireunit.Lat.ToString(),
                lon = fireunit == null ? "" : fireunit.Lng.ToString()
            };
            DataApi.UpdateAlarm(alarmDto);
            var dataid = DataApi.UpdateAlarm(alarmDto);
            if (!string.IsNullOrEmpty(dataid))
            {
                DataApi.UpdateEvent(new GovFire.Dto.EventDto()
                {
                    id = id.ToString(),
                    state = "0",
                    createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    donetime = "",
                    eventcontent = $"{alarmDto.devicelocation},{alarmDto.devicesn},{alarmDto.devicetype}发生预警",
                    eventtype = "消防火警系统报警数据",
                    firecompany = alarmDto.firecompany,
                    lat = alarmDto.lat,
                    lon = alarmDto.lon,
                    fireUnitId = dataid
                });
            }
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
        public async Task<PagedResultDto<AlarmCheckOutput>> GetAlarmChecks(int fireunitid, List<string> filter, Abp.Application.Services.Dto.PagedResultRequestDto dto)
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
            List<AlarmCheckOutput> res=new List<AlarmCheckOutput>();
            int total = 0;
            await Task.Run(() =>
            {
                var resall = elec.Union(fire).OrderByDescending(p => p.Time).ToList();
                foreach(var v in resall)
                {
                    if(v.CheckStateValue==(byte)CheckStateType.UnCheck && DateTime.Now-DateTime.Parse(v.Time)>new TimeSpan(1,0,0))
                    {
                        v.CheckStateValue = (byte)CheckStateType.Expire;
                        v.CheckStateName = CheckStateTypeNames.GetName(CheckStateType.Expire);
                    }
                }
                //var resall = all.ToList();
                if (filter != null && filter.Count > 0)
                {
                    for(int i = 0; i < filter.Count; i++)
                    {
                        if (filter[i].Equals("未核警")){
                            filter[i] = "核警";
                            break;
                        }
                    }
                    resall = (from a in filter
                              join b in resall
                              on a equals b.CheckStateName
                              select b).ToList();
                }
                total = resall.Count();
                res = resall.Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList();
            });
            return new PagedResultDto<AlarmCheckOutput>(total, res);
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
                await _photosPathSaveRep.InsertAsync(new PhotosPathSave()
                {
                    DataId = alarmCheck.Id,
                    TableName = "AlarmCheck",
                    PhotoPath = dto.PictureUrl_1
                });
            if (dto.PictureUrl_2 != null)
                await _photosPathSaveRep.InsertAsync(new PhotosPathSave()
                {
                    DataId = alarmCheck.Id,
                    TableName = "AlarmCheck",
                    PhotoPath = dto.PictureUrl_2
                });
            if (dto.PictureUrl_3 != null)
                await _photosPathSaveRep.InsertAsync(new PhotosPathSave()
                {
                    DataId = alarmCheck.Id,
                    TableName = "AlarmCheck",
                    PhotoPath = dto.PictureUrl_3
                });

            //    alarmCheck.PicturUrls= dto.PictureUrl_1;
            //if (dto.PictureUrl_2 != null)
            //    alarmCheck.PicturUrls = string.IsNullOrEmpty(alarmCheck.PicturUrls) ? "" : "," + dto.PictureUrl_2;
            //if (dto.PictureUrl_3 != null)
            //    alarmCheck.PicturUrls = string.IsNullOrEmpty(alarmCheck.PicturUrls) ? "" : "," + dto.PictureUrl_3;
            alarmCheck.VioceUrl = dto.VioceUrl;
            alarmCheck.NotifyWorker = dto.NotifyWorker ? (byte)1 : (byte)0;
            alarmCheck.NotifyMiniaturefire = dto.NotifyMiniaturefire ? (byte)1 : (byte)0;
            alarmCheck.Notify119 = dto.Notify119 ? (byte)1 : (byte)0;
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
            var checkState = alarmCheck.CheckState;
            if (alarmCheck.CheckState == (byte)CheckStateType.UnCheck && (DateTime.Now - DateTime.Parse(dto.Time)) > new TimeSpan(1, 0, 0))
                checkState = (byte)CheckStateType.Expire;
            dto.CheckStateValue = checkState;
            dto.CheckStateName = CheckStateTypeNames.GetName((CheckStateType)checkState);
            dto.Content = alarmCheck.Content;
            var photos = _photosPathSaveRep.GetAll().Where(p => p.TableName.Equals("AlarmCheck") && p.DataId == alarmCheck.Id).Select(p => p.PhotoPath).ToList();
            if (photos.Count() > 0)
                dto.PictureUrl_1 = photos[0];
            if (photos.Count() > 1)
                dto.PictureUrl_2 = photos[1];
            if (photos.Count() > 2)
                dto.PictureUrl_3 = photos[2];
            //if (!string.IsNullOrEmpty(alarmCheck.PicturUrls))
            //{
            //    string[] ss = alarmCheck.PicturUrls.Split(',');
            //    if (ss.Count() > 0)
            //        dto.PictureUrl_1 = ss[0];
            //    if (ss.Count() > 1)
            //        dto.PictureUrl_2 = ss[1];
            //    if (ss.Count() > 2)
            //        dto.PictureUrl_3 = ss[2];
            //}
            dto.VioceUrl = alarmCheck.VioceUrl;
            dto.NotifyWorker = alarmCheck.NotifyWorker != 0;
            dto.NotifyMiniaturefire = alarmCheck.NotifyMiniaturefire != 0;
            dto.Notify119 = alarmCheck.Notify119 != 0;
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
