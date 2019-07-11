using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using DeviceServer.Tcp.Protocol;
using FireProtectionV1.Common.Enum;
using FireProtectionV1.Configuration;
using FireProtectionV1.Enterprise.Dto;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Model;
using FireProtectionV1.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.FireWorking.Manager
{
    public class FireWorkingManager:IFireWorkingManager
    {
        IRepository<AlarmCheck> _alarmCheckRep;
        IRepository<FireUnitType> _fireUnitTypeRep;
        IRepository<FireUnit> _fireUnitRep;
        IRepository<DataToDuty> _dutyRep;
        IRepository<DataToPatrol> _patrolRep;
        IRepository<Fault> _faultRep;
        IRepository<Gateway> _gatewayRep;
        IRepository<Detector> _detectorRep;
        IRepository<DetectorType> _detectorTypeRep;
        IRepository<AlarmToFire> _alarmToFireRep;
        IRepository<AlarmToElectric> _alarmToElectricRep;
        public FireWorkingManager(
            IRepository<AlarmCheck> alarmCheckRep,
            IRepository<FireUnitType> fireUnitTypeRep,
            IRepository<FireUnit> fireUnitRep,
            IRepository<DataToDuty> dutyRep,
            IRepository<DataToPatrol> patrolRep,
            IRepository<Fault> faultRep,
            IRepository<Gateway> gatewayRep,
            IRepository<Detector> detectorRep,
            IRepository<DetectorType> detectorTypeRep,
            IRepository<AlarmToFire> alarmToFireR,
            IRepository<AlarmToElectric> alarmToElectricR)
        {
            _alarmCheckRep = alarmCheckRep;
            _fireUnitTypeRep = fireUnitTypeRep;
            _fireUnitRep = fireUnitRep;
            _dutyRep = dutyRep;
            _patrolRep = patrolRep;
            _faultRep = faultRep;
            _gatewayRep = gatewayRep;
            _detectorRep = detectorRep;
            _detectorTypeRep = detectorTypeRep;
            _alarmToElectricRep = alarmToElectricR;
            _alarmToFireRep = alarmToFireR;
        }
        /// <summary>
        /// 防火单位消防数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnitAlarmOutput> GetFireUnitAlarm(FireUnitIdInput input)
        {
            GetFireUnitAlarmOutput output = new GetFireUnitAlarmOutput();
            int highFreq = int.Parse(ConfigHelper.Configuration["FireDomain:HighFreqAlarm"]);
            await Task.Run(() =>
            {
                var fireunit =  _fireUnitRep.Single(p => p.Id == input.Id);
                output.FireUnitName = fireunit.Name;
                //安全用电数据：管控点位数量、网关状态、最近30天报警次数（可查）、高频报警部件数量（可查）
                var alarmElec = _alarmToElectricRep.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime >= DateTime.Now.Date.AddDays(-30));
                var alarmElecAll = _alarmToElectricRep.GetAll().Where(p => p.FireUnitId == input.Id).OrderBy(p => p.CreationTime);
                if (alarmElecAll.Count() > 0)
                {
                    output.ElecFirstAlarmTime = alarmElecAll.First().CreationTime.ToString("yyyy-MM-dd");
                    output.ElecLastAlarmTime = alarmElecAll.Max(p => p.CreationTime).ToString("yyyy-MM-dd HH:mm");

                }
                output.ElecAlarmCount = alarmElecAll.Count();
                output.ElecAlarmCheckCount= _alarmCheckRep.GetAll().Where(p => p.FireUnitId == input.Id && p.FireSysType == (byte)FireSysType.Electric && p.CheckState != (byte)CheckStateType.UnCheck).Count();
                output.Elec30DayCount = alarmElec.Count();
                output.ElecHighCount = output.Elec30DayCount == 0 ?
                0 : alarmElec.GroupBy(p => p.DetectorId).Select(p=>new {DetectorId=p.Key, Count = p.Count() })
                .Where(p => p.Count > highFreq).Count();
                var detectsEle = _detectorRep.GetAll().Where(p => p.FireUnitId == input.Id && p.FireSysType == (byte)FireSysType.Electric);
                var typeE = _detectorTypeRep.GetAll().Where(p => p.GBType == (byte)UnitType.ElectricResidual).FirstOrDefault();
                if(typeE != null)
                    output.ElecECount = detectsEle.Where(p=>p.DetectorTypeId== typeE.Id).Count();
                var typeT = _detectorTypeRep.GetAll().Where(p => p.GBType == (byte)UnitType.ElectricTemperature).FirstOrDefault();
                if (typeT != null)
                    output.ElecTCount = detectsEle.Where(p => p.DetectorTypeId == typeT.Id).Count();
                //火警预警数据：管控点位数量、网关状态、最近30天报警次数（可查）、高频报警部件数量（可查）；
                var alarmFire = _alarmToFireRep.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime >= DateTime.Now.Date.AddDays(-30));
                var alarmFireAll = _alarmToFireRep.GetAll().Where(p => p.FireUnitId == input.Id).OrderBy(p => p.CreationTime);
                if (alarmFireAll.Count() > 0)
                {
                    output.FireFirstAlarmTime = alarmFireAll.First().CreationTime.ToString("yyyy-MM-dd");
                    output.FireLastAlarmTime = alarmFireAll.Max(p => p.CreationTime).ToString("yyyy-MM-dd HH:mm");
                }
                output.FireAlarmCount = alarmFireAll.Count();
                output.FireAlarmCheckCount = _alarmCheckRep.GetAll().Where(p => p.FireUnitId == input.Id && p.FireSysType == (byte)FireSysType.Fire && p.CheckState != (byte)CheckStateType.UnCheck).Count();
                output.Fire30DayCount = alarmFire.Count();
                output.FireHighCount = output.Fire30DayCount == 0 ? 0 :
                alarmFire.GroupBy(p => p.DetectorId).Select(p => new { DetectorId = p.Key, Count = p.Count() })
                .Where(p => p.Count > highFreq).Count();
                var detectsFire = _detectorRep.GetAll().Where(p => p.FireUnitId == input.Id && p.FireSysType == (byte)FireSysType.Fire);
                output.FirePointsCount = detectsFire.Count();
                //故障数据
                var faults = _faultRep.GetAll().Where(p => p.FireUnitId == input.Id);
                output.FaultCount = faults.Count();
                output.FaultPendingCount = faults.Where(p => p.ProcessState == 0).Count();
                output.FaultProcessedCount = output.FaultCount - output.FaultPendingCount;
                //巡查记录：最近提交时间、最近30天提交记录数量
                var lastPatrol = _patrolRep.GetAll().Where(p => p.FireUnitId == input.Id).OrderByDescending(p => p.CreationTime).FirstOrDefault();
                output.PatrolLastTime = lastPatrol == null ? "" : lastPatrol.CreationTime.ToString("yyyy-MM-dd HH:mm");
                output.Patrol30DayCount = _patrolRep.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime >= DateTime.Now.Date.AddDays(-30)).Count();
                output.PatrolCount = _patrolRep.GetAll().Where(p => p.FireUnitId == input.Id).Count();
                if(output.PatrolCount>0)
                    output.FirstPatrolTime = _patrolRep.GetAll().Where(p => p.FireUnitId == input.Id).Min(p => p.CreationTime).ToString("yyyy-MM-dd");
                //值班记录：最近提交时间、最近30天提交记录数量
                var lastDuty = _dutyRep.GetAll().Where(p => p.FireUnitId == input.Id).OrderByDescending(p => p.CreationTime).FirstOrDefault();
                output.DutyLastTime = lastDuty == null ? "" : lastDuty.CreationTime.ToString("yyyy-MM-dd HH:mm");
                output.DutyCount = _dutyRep.GetAll().Where(p => p.FireUnitId == input.Id).Count();
                if (output.DutyCount>0)
                    output.FirstDutyTime = _dutyRep.GetAll().Where(p => p.FireUnitId == input.Id).Min(p => p.CreationTime).ToString("yyyy-MM-dd");
                output.Duty30DayCount = _dutyRep.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime >= DateTime.Now.Date.AddDays(-30)).Count();
            });
            return output;
        }
        /// <summary>
        /// 安全用电最近30天报警记录查询
        /// </summary>
        /// <param name="input"></param>
        /// <param name="detectorTypeId">探测器类型</param>
        /// <returns></returns>
        public async Task<PagedResultDto<AlarmRecord>> GetFireUnit30DayAlarmEle(GetPageByFireUnitIdInput input,int detectorTypeId)
        {
            var output = new PagedResultDto<AlarmRecord>();
            await Task.Run(() =>
            {
                var alarm30 = from a in _alarmToElectricRep.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime >= DateTime.Now.Date.AddDays(-30))
                              join b in _detectorRep.GetAll().Where(p => (-1==detectorTypeId?true:p.DetectorTypeId == detectorTypeId))
                              on a.DetectorId equals b.Id
                              orderby a.CreationTime descending
                              select a;
                var lstRecords = from a in alarm30
                                 join b in _detectorRep.GetAll()
                                 on a.DetectorId equals b.Id
                                 join c in _detectorTypeRep.GetAll()
                                 on b.DetectorTypeId equals c.Id
                                 select new AlarmRecord()
                                 {
                                     Time = a.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                     //Content = $"报警部件：{c.Name}，当前值：{a.Analog.ToString("0")}{a.Unit},警戒值：{a.AlarmLimit}",
                                     Content = $"【{c.Name}】发生报警，探测值{a.Analog.ToString("0")}{a.Unit},警戒值{a.AlarmLimit}",
                                     Location =b.Location
                                 };

                var lst = lstRecords.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
                output.TotalCount = lstRecords.Count();
                output.Items = lst;
            });
            return output;
        }
        /// <summary>
        /// 火警预警最近30天报警记录查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<AlarmRecord>> GetFireUnit30DayAlarmFire(GetPageByFireUnitIdInput input)
        {
            var output = new PagedResultDto<AlarmRecord>();
            await Task.Run(() =>
            {
                var alarm30 = _alarmToFireRep.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime >= DateTime.Now.Date.AddDays(-30))
                    .OrderByDescending(p => p.CreationTime);
                var lstRecords = from a in alarm30
                                 join b in _detectorRep.GetAll()
                                 on a.DetectorId equals b.Id
                                 join c in _detectorTypeRep.GetAll()
                                 on b.DetectorTypeId equals c.Id
                                 select new AlarmRecord()
                                 {
                                     Time = a.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                     //Content = $"报警部件：{c.Name}，报警地点：{b.Location}",
                                     Content = $"【{c.Name}】发生报警",
                                     Location = b.Location
                                 };

                var lst = lstRecords.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
                output.TotalCount = lstRecords.Count();
                output.Items = lst;
            });
            return output;
        }
        /// <summary>
        /// 安全用电高频报警部件查询
        /// </summary>
        /// <param name="input"></param>
        /// <param name="onlyElecOrTemp"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<HighFreqAlarmDetector>> GetFireUnitHighFreqAlarmEle(GetPageByFireUnitIdInput input, string onlyElecOrTemp = "")
        {
            var output = new PagedResultDto<HighFreqAlarmDetector>();
            await Task.Run(() =>
            {
                DetectorType detectorType = null;
                if (onlyElecOrTemp.Equals("elec"))
                    detectorType = _detectorTypeRep.GetAll().Where(p => p.GBType == (byte)UnitType.ElectricResidual).FirstOrDefault();
                else if(onlyElecOrTemp.Equals("temp"))
                    detectorType = _detectorTypeRep.GetAll().Where(p => p.GBType == (byte)UnitType.ElectricTemperature).FirstOrDefault();
                int typeid = detectorType == null ? 0 : detectorType.Id;
                //var alarmFire = string.IsNullOrEmpty(onlyElecOrTemp) ?
                //_alarmToElectricRep.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime >= DateTime.Now.Date.AddDays(-30))
                //:
                var alarmFire = from a in _alarmToElectricRep.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime >= DateTime.Now.Date.AddDays(-30))
                                join b in _detectorRep.GetAll().Where(p => (typeid == 0 ? true : (p.DetectorTypeId == typeid)))
                                on a.DetectorId equals b.Id
                                select a;
                int highFreq = int.Parse(ConfigHelper.Configuration["FireDomain:HighFreqAlarm"]);
                var alarmDevices = alarmFire.GroupBy(p => p.DetectorId).Select(p => new
                {
                    DetectorId = p.Key,
                    LastTime = p.Max(p1 => p1.CreationTime),
                    AlramCount = p.Count()
                })
                .Where(p => p.AlramCount > highFreq);
                    //.Select(p => new { DetectorId = p.Key, Count = p.Count() });
                var lstResult = from a in alarmDevices
                                join b in _detectorRep.GetAll()
                                on a.DetectorId equals b.Id
                                join c in _detectorTypeRep.GetAll()
                                on b.DetectorTypeId equals c.Id
                                orderby a.AlramCount descending
                                select new HighFreqAlarmDetector()
                                {
                                    Name = $"【{c.Name}】{b.Location}",
                                    Time = a.LastTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                    Count = a.AlramCount
                                };
                var lst = lstResult.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
                output.TotalCount = lstResult.Count();
                output.Items = lst;
            });
            return output;
        }
        /// <summary>
        /// 火警预警高频报警部件查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<HighFreqAlarmDetector>> GetFireUnitHighFreqAlarmFire(GetPageByFireUnitIdInput input)
        {
            var output = new PagedResultDto<HighFreqAlarmDetector>();
            await Task.Run(() =>
            {
                var alarmFire = _alarmToFireRep.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime >= DateTime.Now.Date.AddDays(-30));
                int highFreq = int.Parse(ConfigHelper.Configuration["FireDomain:HighFreqAlarm"]);
                var alarmDevices = alarmFire.GroupBy(p => p.DetectorId).Select(p => new
                {
                    DetectorId = p.Key,
                    LastTime = p.Max(p1 => p1.CreationTime),
                    AlramCount = p.Count()
                })
                .Where(p => p.AlramCount > highFreq);
                //.Select(p => new { DetectorId = p.Key, Count = p.Count() });
                var lstResult = from a in alarmDevices
                                join b in _detectorRep.GetAll()
                                on a.DetectorId equals b.Id
                                join c in _detectorTypeRep.GetAll()
                                on b.DetectorTypeId equals  c.Id
                                orderby a.AlramCount descending
                                select new HighFreqAlarmDetector()
                                {
                                    Name = $"【{c.Name}】{b.Location}",
                                    Time = a.LastTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                    Count = a.AlramCount
                                };
                var lst = lstResult.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
                output.TotalCount = lstResult.Count();
                output.Items = lst;
            });
            return output;
        }
        /// <summary>
        /// （所有防火单位）设备设施故障监控
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<PagedResultDto<FireUnitFaultOuput>> GetFireUnitFaultList(GetPagedFireUnitListInput input)
        {
            var query = from a in _faultRep.GetAll().GroupBy(p => p.FireUnitId)
                        join b in _fireUnitRep.GetAll()
                        on a.Key equals b.Id
                        select new FireUnitFaultOuput()
            {
                FireUnitId = a.Key,
                FaultCount = a.Count(),
                FireUnitName = b.Name,
                ProcessedCount = a.Where(p1 => p1.ProcessState == 1).Count(),
                PendingCount = a.Where(p1 => p1.ProcessState == 0).Count()
            };
            if (!string.IsNullOrEmpty(input.Name))
            {
                query = from a in query
                        join b in _fireUnitRep.GetAll().Where(p => p.Name.Contains(input.Name))
                        on a.FireUnitId equals b.Id
                        select a;
            }
            query = query.OrderByDescending(p => p.PendingCount);
            return Task.FromResult<PagedResultDto<FireUnitFaultOuput>>(new PagedResultDto<FireUnitFaultOuput>(
                query.Count(), query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList()));
        }
        /// <summary>
         /// 设备设施故障待处理故障查询
         /// </summary>
         /// <param name="input"></param>
         /// <returns></returns>
        public async Task<PagedResultDto<PendingFaultOutput>> GetFireUnitPendingFault(GetPageByFireUnitIdInput input)
        {
            var output = new PagedResultDto<PendingFaultOutput>();
            await Task.Run(() =>
            {
                var lstResult = from a in _faultRep.GetAll().Where(p => p.FireUnitId == input.Id && p.ProcessState == 0)
                                join b in _detectorRep.GetAll()
                                on a.DetectorId equals b.Id
                                join c in _detectorTypeRep.GetAll()
                                on b.DetectorTypeId equals c.Id
                                orderby a.CreationTime descending
                                select new PendingFaultOutput()
                                {
                                    DetectorTypeName =c.Name,
                                    Time = a.CreationTime.ToString("yyyy-MM-dd"),
                                    Content = a.FaultRemark
                                };
                var lst = lstResult.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
                output.TotalCount = lstResult.Count();
                output.Items = lst;
            });
            return output;
        }
        /// <summary>
        /// 安全用电数据分析
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetAreasAlarmElectricOutput> GetAreasAlarmElectric(GetAreasAlarmElectricInput input)
        {
            GetAreasAlarmElectricOutput output = new GetAreasAlarmElectricOutput();
            await Task.Run(() =>
            {
                //安全用电数据：管控点位数量、网关状态、最近30天报警次数（可查）、高频报警部件数量（可查）
                //var alarmElec = _alarmToElectricRep.GetAll().Where(p => p.CreationTime >= DateTime.Now.Date.AddDays(-30));
                //var elecFireUnits=_detectorRep.GetAll().Where(p=>p.FireSysType==)
                //output.JoinFireUnitCount = alarmElec.Count();
                //联网防火单位类型数量分布
                var detectElec = from a in _detectorRep.GetAll().Where(p => p.FireSysType == (byte)FireSysType.Electric)
                                 join b in _fireUnitRep.GetAll()
                                 on a.FireUnitId equals b.Id
                                 select a;
                var fireunits = detectElec.GroupBy(p => p.FireUnitId);
                output.JoinFireUnitCount = fireunits.Count();
                var joinTypeCounts = from a in fireunits.Select(p=>p.Key).ToList()
                                     join b in _fireUnitRep.GetAll()
                                     on a equals b.Id
                                     join c in _fireUnitTypeRep.GetAll()
                                     on b.TypeId equals c.Id
                                     group a by c.Name into g
                                     select new JoinTypeCount()
                                     {
                                         Type = g.Key,
                                         Count = g.Count()
                                     };
                output.JoinTypeCounts = joinTypeCounts.ToList();
                //联网监控点位（单位类型柱状图）
                output.JoinPointCount = detectElec.Count();
                var joinTypePointCounts = from a in detectElec
                                          join c in _fireUnitRep.GetAll()
                                          on a.FireUnitId equals c.Id
                                          join d in _fireUnitTypeRep.GetAll()
                                          on c.TypeId equals d.Id
                                          group a by d.Name into g
                                          select new JoinTypePointCount() { Type = g.Key, Count = g.Count() };
                output.JoinTypePointCounts = joinTypePointCounts.ToList();
                //网关离线单位
                var offlineFireUnits = from a in detectElec
                                       join c in _gatewayRep.GetAll().Where(p=>p.FireSysType== (byte)FireSysType.Electric&&p.Status==Common.Enum.GatewayStatus.Offline)
                                       on a.GatewayId equals c.Id
                                       join b in _fireUnitRep.GetAll()
                                       on a.FireUnitId equals b.Id
                                       orderby c.StatusChangeTime descending
                                       select new OfflineFireUnit (){ Name= b.Name,Time=c.StatusChangeTime.ToString("yyyy-MM-dd") };
                output.OfflineFireUnitsCount = offlineFireUnits.Count();
                output.OfflineFireUnits = offlineFireUnits.Take(10).ToList();
                //安全用电累计预警（最近月份流量图）
                DateTime now = DateTime.Now;
                DateTime nowMonDay1 = now.Date.AddDays(1 - now.Day);
                var monthAlarmCounts = _alarmToElectricRep.GetAll().Where(p => p.CreationTime >= nowMonDay1.AddMonths(-3))
                .GroupBy(p=>p.CreationTime.ToString("yyyy年MM月")).Select(p=>new MonthCount() { Month = p.Key, Count = p.Count() });
                output.MonthAlarmCounts = monthAlarmCounts.OrderBy(p => p.Month).ToList();
                //最近30天报警次数Top10
                var unitAlarmCounts30=_alarmToElectricRep.GetAll().Where(p => p.CreationTime >= now.Date.AddDays(-30))
                .GroupBy(p => p.FireUnitId).Select(p => new { FireUnitId = p.Key, Count = p.Count() }).OrderBy(p=>p.Count);
                var unitCounts30 = from a in unitAlarmCounts30.Take(10)
                                   join b in _fireUnitRep.GetAll()
                                   on a.FireUnitId equals b.Id
                                   select new { a.FireUnitId, a.Count, b.Name };
                var top10FireUnits = from a in detectElec
                                     join b in unitCounts30
                                     on a.FireUnitId equals b.FireUnitId
                                     group a by b into g
                                     select new Top10FireUnit() { AlarmCount = g.Key.Count, Name = g.Key.Name, PointCount = g.Count() };
                output.Top10FireUnits = top10FireUnits.OrderByDescending(p=>p.AlarmCount).ToList();
            });
            return output;
        }
        /// <summary>
        /// （所有防火单位）火灾报警监控列表
        /// </summary>
        /// <returns></returns>
        public Task<PagedResultDto<GetAreas30DayFireAlarmOutput>> GetAreas30DayFireAlarmList(GetPagedFireUnitListFilterTypeInput input)
        {
            var alarmFire = _alarmToFireRep.GetAll().Where(p => p.CreationTime >= DateTime.Now.Date.AddDays(-30));
            //模糊查询
            if (!string.IsNullOrEmpty(input.Name))
            {
                alarmFire = from a in alarmFire
                            join b in _fireUnitRep.GetAll().Where(p => p.Name.Contains(input.Name))
                            on a.FireUnitId equals b.Id
                            select a;
            }

            int highFreq = int.Parse(ConfigHelper.Configuration["FireDomain:HighFreqAlarm"]);
            var alarmFireUnits = alarmFire.GroupBy(p => p.FireUnitId).Select(p => new
            {
                FireUnitId = p.Key,
                LastAlarmTime = p.Max(p1 => p1.CreationTime),
                AlarmCount = p.Count(),
                FreqCount = p.GroupBy(p1 => p1.DetectorId).Where(p1 => p1.Count() > highFreq).Count()
            });
            GatewayStatus status = GatewayStatus.Unusual;
            if (!string.IsNullOrEmpty(input.GetwayStatusValue))
                status = (GatewayStatus)Enum.Parse(typeof(GatewayStatus), input.GetwayStatusValue);
            var states = (from a in alarmFireUnits
                          join b in _gatewayRep.GetAll().Where(p => p.FireSysType == (byte)FireSysType.Fire)
                          on a.FireUnitId equals b.FireUnitId
                          group b by a.FireUnitId into g
                          select new
                          {
                              FireUnitId = g.Key,
                              CountOnline = g.Count(p => p.Status == GatewayStatus.Online),
                              Count = g.Count()
                          }).ToList().Select(p => new
                          {
                              p.FireUnitId,
                              Status = p.Count == p.CountOnline ? GatewayStatus.Online : (p.CountOnline == 0 ? GatewayStatus.Offline
                         : GatewayStatus.PartOffline)
                          });
            var v = from a in alarmFireUnits
                    join b in states.Where(p => (string.IsNullOrEmpty(input.GetwayStatusValue) ? true : p.Status == status))
                    on a.FireUnitId equals b.FireUnitId
                    join c in _fireUnitRep.GetAll()
                    on a.FireUnitId equals c.Id
                    join d in _fireUnitTypeRep.GetAll().Where(p => 0 == input.FireUnitTypeId ? true : p.Id == input.FireUnitTypeId)
                    on c.TypeId equals d.Id
                    orderby a.LastAlarmTime descending
                    select new GetAreas30DayFireAlarmOutput()
                    {
                        FireUnitId = a.FireUnitId,
                        FireUnitName = c.Name,
                        TypeId = d.Id,
                        TypeName = d.Name,
                        AlarmTime = a.LastAlarmTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        AlarmCount = a.AlarmCount,
                        HighFreqCount = a.FreqCount,
                        StatusValue = b.Status,
                        StatusName = GatewayStatusNames.GetName(b.Status)
                    };
            return Task.FromResult<PagedResultDto<GetAreas30DayFireAlarmOutput>>(new PagedResultDto<GetAreas30DayFireAlarmOutput>
                 (v.Count(), v.Skip(input.SkipCount).Take(input.MaxResultCount).ToList()));
        }
        /// <summary>
        /// （所有防火单位）安全用电监控列表（电缆温度）
        /// </summary>
        /// <returns></returns>
        public Task<PagedResultDto<GetAreas30DayFireAlarmOutput>> GetAreas30DayTempAlarmList(GetPagedFireUnitListFilterTypeInput input)
        {
            var type = _detectorTypeRep.GetAll().Where(p => p.GBType == (byte)UnitType.ElectricTemperature).FirstOrDefault();
            return GetAreas30DayElecAlarmListOnlyId(input, type.Id);
        }
        /// <summary>
        /// （所有防火单位）安全用电监控列表（剩余电流）
        /// </summary>
        /// <returns></returns>
        public Task<PagedResultDto<GetAreas30DayFireAlarmOutput>> GetAreas30DayElecAlarmList(GetPagedFireUnitListFilterTypeInput input)
        {
            var type = _detectorTypeRep.GetAll().Where(p => p.GBType == (byte)UnitType.ElectricResidual).FirstOrDefault();
            return GetAreas30DayElecAlarmListOnlyId(input, type.Id);
        }
        /// <summary>
        /// 安全用电剩余电流监控列表
        /// </summary>
        /// <returns></returns>
        Task<PagedResultDto<GetAreas30DayFireAlarmOutput>> GetAreas30DayElecAlarmListOnlyId(GetPagedFireUnitListFilterTypeInput input,int id)
        {
            var alarmFire = from a in _alarmToElectricRep.GetAll().Where(p => p.CreationTime >= DateTime.Now.Date.AddDays(-30))
                            join b in _detectorRep.GetAll().Where(p => p.DetectorTypeId == id)
                            on a.DetectorId equals b.Id
                            select a;
            //模糊查询
            if (!string.IsNullOrEmpty(input.Name))
            {
                alarmFire = from a in alarmFire
                            join b in _fireUnitRep.GetAll().Where(p => p.Name.Contains(input.Name))
                            on a.FireUnitId equals b.Id
                            select a;
            }
            int highFreq = int.Parse(ConfigHelper.Configuration["FireDomain:HighFreqAlarm"]);
            var alarmFireUnits = alarmFire.GroupBy(p => p.FireUnitId).Select(p => new
            {
                FireUnitId = p.Key,
                LastAlarmTime = p.Max(p1 => p1.CreationTime),
                AlarmCount = p.Count(),
                FreqCount = p.GroupBy(p1 => p1.DetectorId).Where(p1 => p1.Count() > highFreq).Count()
            });
            GatewayStatus status = GatewayStatus.Unusual;
            if (!string.IsNullOrEmpty(input.GetwayStatusValue))
                status = (GatewayStatus)Enum.Parse(typeof(GatewayStatus), input.GetwayStatusValue);
            var states = (from a in alarmFireUnits
                          join b in _gatewayRep.GetAll().Where(p => p.FireSysType == (byte)FireSysType.Electric)
                          on a.FireUnitId equals b.FireUnitId
                          group b by a.FireUnitId into g
                          select new
                          {
                              FireUnitId = g.Key,
                              CountOnline = g.Count(p => p.Status == GatewayStatus.Online),
                              Count = g.Count()
                          }).ToList().Select(p => new
                          {
                              p.FireUnitId,
                              Status = p.Count == p.CountOnline ? GatewayStatus.Online : (p.CountOnline == 0 ? GatewayStatus.Offline
                         : GatewayStatus.PartOffline)
                          });

            var v = from a in alarmFireUnits
                    join b in states.Where(p => (string.IsNullOrEmpty(input.GetwayStatusValue) ? true : p.Status == status))
                    on a.FireUnitId equals b.FireUnitId
                    join c in _fireUnitRep.GetAll()
                    on a.FireUnitId equals c.Id
                    join d in _fireUnitTypeRep.GetAll().Where(p => (0 == input.FireUnitTypeId ? true : p.Id == input.FireUnitTypeId))
                    on c.TypeId equals d.Id
                    orderby a.LastAlarmTime descending
                    select new GetAreas30DayFireAlarmOutput()
                    {
                        FireUnitId = a.FireUnitId,
                        FireUnitName = c.Name,
                        TypeName = d.Name,
                        AlarmTime = a.LastAlarmTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        AlarmCount = a.AlarmCount,
                        HighFreqCount = a.FreqCount,
                        StatusValue = b.Status,
                        StatusName = GatewayStatusNames.GetName(b.Status)
                    };
            return Task.FromResult<PagedResultDto<GetAreas30DayFireAlarmOutput>>(new PagedResultDto<GetAreas30DayFireAlarmOutput>
                 (v.Count(), v.Skip(input.SkipCount).Take(input.MaxResultCount).ToList()));
        }
        /// <summary>
        /// 火警预警数据分析
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetAreasAlarmFireOutput> GetAreasAlarmFire(GetAreasAlarmFireInput input)
        {
            GetAreasAlarmFireOutput output = new GetAreasAlarmFireOutput();
            await Task.Run(() =>
            {
                //火警预警数据：管控点位数量、网关状态、最近30天报警次数（可查）、高频报警部件数量（可查）
                var detectFire = from a in _detectorRep.GetAll().Where(p => p.FireSysType == (byte)FireSysType.Fire)
                                 join b in _fireUnitRep.GetAll()
                                 on a.FireUnitId equals b.Id
                                 select a;
                var fireunits = detectFire.GroupBy(p => p.FireUnitId);
                output.JoinFireUnitCount = fireunits.Count();
                var joinTypeCounts = from a in fireunits.Select(p => p.Key).ToList()
                                     join b in _fireUnitRep.GetAll()
                                     on a equals b.Id
                                     join c in _fireUnitTypeRep.GetAll()
                                     on b.TypeId equals c.Id
                                     group a by c.Name into g
                                     select new JoinTypeCount()
                                     {
                                         Type = g.Key,
                                         Count = g.Count()
                                     };
                //联网防火单位类型数量分布
                output.JoinTypeCounts = joinTypeCounts.ToList();
                ////联网监控点位（单位类型柱状图）
                //output.JoinPointCount = detectElec.Count();
                //var joinTypePointCounts = from a in detectElec
                //                          join c in _fireUnitRep.GetAll()
                //                          on a.FireUnitId equals c.Id
                //                          join d in _fireUnitTypeRep.GetAll()
                //                          on c.TypeId equals d.Id
                //                          group a by d.Name into g
                //                          select new JoinTypePointCount() { Type = g.Key, Count = g.Count() };
                //output.JoinTypePointCounts = joinTypePointCounts.ToArray();
                //网关离线单位
                var offlineFireUnits = from a in detectFire
                                       join c in _gatewayRep.GetAll().Where(p => p.Status == Common.Enum.GatewayStatus.Offline)
                                       on a.GatewayId equals c.Id
                                       join b in _fireUnitRep.GetAll()
                                       on a.FireUnitId equals b.Id
                                       orderby c.StatusChangeTime descending
                                       select new OfflineFireUnit() { Name = b.Name, Time = c.StatusChangeTime.ToString("yyyy-MM-dd") };
                output.OfflineFireUnitsCount = offlineFireUnits.Count();
                output.OfflineFireUnits = offlineFireUnits.Take(10).ToList();
                //火警预警累计预警（最近月份流量图）
                DateTime now = DateTime.Now;
                DateTime nowMonDay1 = now.Date.AddDays(1 - now.Day);
                var monthAlarmCounts = _alarmToFireRep.GetAll().Where(p => p.CreationTime >= nowMonDay1.AddMonths(-3))
                .GroupBy(p => p.CreationTime.ToString("yyyy年MM月")).Select(p => new MonthCount() { Month = p.Key, Count = p.Count() });
                output.MonthAlarmCounts = monthAlarmCounts.OrderBy(p=>p.Month).ToList();
                //最近30天报警次数Top10
                var unitAlarmCounts30 = _alarmToFireRep.GetAll().Where(p => p.CreationTime >= now.Date.AddDays(-30))
                .GroupBy(p => p.FireUnitId).Select(p => new { FireUnitId = p.Key, Count = p.Count() }).OrderBy(p => p.Count);
                var unitCounts30 = from a in unitAlarmCounts30.Take(10)
                                   join b in _fireUnitRep.GetAll()
                                   on a.FireUnitId equals b.Id
                                   select new { a.FireUnitId, a.Count, b.Name };
                var top10FireUnits = from a in detectFire
                                     join b in unitCounts30
                                     on a.FireUnitId equals b.FireUnitId
                                     group a by b into g
                                     select new Top10FireUnit() { AlarmCount = g.Key.Count, Name = g.Key.Name, PointCount = g.Count() };
                output.Top10FireUnits = top10FireUnits.OrderByDescending(p=>p.AlarmCount).ToList();
            });
            return output;
        }


    }
}
