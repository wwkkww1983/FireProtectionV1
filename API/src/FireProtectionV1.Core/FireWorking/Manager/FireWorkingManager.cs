using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using FireProtectionV1.Configuration;
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
        IRepository<FireUnitType> _fireUnitTypeRep;
        IRepository<FireUnit> _fireUnitRep;
        IRepository<Duty> _dutyRep;
        IRepository<DataToPatrol> _patrolRep;
        IRepository<Fault> _faultRep;
        IRepository<Gateway> _gatewayRep;
        IRepository<Detector> _detectorRep;
        IRepository<DetectorType> _detectorTypeRep;
        IRepository<AlarmToFire> _alarmToFireRep;
        IRepository<AlarmToElectric> _alarmToElectricRep;
        public FireWorkingManager(
            IRepository<FireUnitType> fireUnitTypeRep,
            IRepository<FireUnit> fireUnitRep,
            IRepository<Duty> dutyRep,
            IRepository<DataToPatrol> patrolRep,
            IRepository<Fault> faultRep,
            IRepository<Gateway> gatewayRep,
            IRepository<Detector> detectorRep,
            IRepository<DetectorType> detectorTypeRep,
            IRepository<AlarmToFire> alarmToFireR,
            IRepository<AlarmToElectric> alarmToElectricR)
        {
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
        public async Task<GetFireUnitAlarmOutput> GetFireUnitAlarm(GetByFireUnitIdInput input)
        {
            GetFireUnitAlarmOutput output = new GetFireUnitAlarmOutput();
            int highFreq = int.Parse(ConfigHelper.Configuration["FireDomain:HighFreqAlarm"]);
            await Task.Run(() =>
            {
                var fireunit =  _fireUnitRep.Single(p => p.Id == input.Id);
                output.FireUnitName = fireunit.Name;
                //安全用电数据：管控点位数量、网关状态、最近30天报警次数（可查）、高频报警部件数量（可查）
                var alarmElec = _alarmToElectricRep.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime >= DateTime.Now.Date.AddDays(-30));
                output.Elec30DayCount = alarmElec.Count();
                output.ElecHighCount = alarmElec.GroupBy(p =>p.DetectorId).Where(p => p.Count() > highFreq).Count();
                var detectsEle = _detectorRep.GetAll().Where(p => p.FireUnitId == input.Id && p.FireSysType == FireSysType.Electric);
                output.ElecPointsCount = detectsEle.Count();
                var netStates = from a in detectsEle
                                join b in _gatewayRep.GetAll()
                                on a.GatewayId equals b.Id
                                select b.Status;
                int netStatesCount = netStates.Count();
                if (netStatesCount > 0)
                {
                    output.ElecState = netStates.First();
                    //if (netStatesCount > 1)
                    //    output.ElecState = $"{output.ElecState}({netStates.Select(p => p.Equals(output.ElecState)).Count()}/{netStatesCount})";
                }
                //火警预警数据：管控点位数量、网关状态、最近30天报警次数（可查）、高频报警部件数量（可查）；
                var alarmFire = _alarmToFireRep.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime >= DateTime.Now.Date.AddDays(-30));
                output.Fire30DayCount = alarmFire.Count();
                output.FireHighCount = alarmFire.GroupBy(p =>p.DetectorId).Where(p => p.Count() > highFreq).Count();
                var detectsFire = _detectorRep.GetAll().Where(p => p.FireUnitId == input.Id && p.FireSysType == FireSysType.Fire);
                output.FirePointsCount = detectsFire.Count();
                netStates = from a in detectsFire
                            join b in _gatewayRep.GetAll()
                            on a.GatewayId equals b.Id
                            select b.Status;
                netStatesCount = netStates.Count();
                if (netStatesCount > 0)
                {
                    output.FireState = netStates.First();
                    //if (netStatesCount > 1)
                    //    output.FireState = $"{output.FireState}({netStates.Select(p => p.Equals(output.FireState)).Count()}/{netStatesCount})";
                }
                //故障数据
                var faults = _faultRep.GetAll().Where(p => p.FireUnitId == input.Id);
                output.FaultCount = faults.Count();
                output.FaultPendingCount = faults.Where(p => p.ProcessState == 0).Count();
                output.FaultProcessedCount = output.FaultCount - output.FaultPendingCount;
                //巡查记录：最近提交时间、最近30天提交记录数量
                output.Patrol30DayCount = _patrolRep.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime >= DateTime.Now.Date.AddDays(-30)).Count();
                //值班记录：最近提交时间、最近30天提交记录数量
                output.Duty30DayCount = _dutyRep.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime >= DateTime.Now.Date.AddDays(-30)).Count();
            });
            return output;
        }
        /// <summary>
        /// 安全用电最近30天报警记录查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<AlarmRecord>> GetFireUnit30DayAlarmEle(GetPageByFireUnitIdInput input)
        {
            var output = new PagedResultDto<AlarmRecord>();
            await Task.Run(() =>
            {
                var alarm30 = _alarmToElectricRep.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime >= DateTime.Now.Date.AddDays(-30))
                    .OrderByDescending(p => p.CreationTime);
                var lstRecords = from a in alarm30
                                 join b in _detectorRep.GetAll()
                                 on a.DetectorId equals b.Id
                                 select new AlarmRecord()
                                 {
                                     Time = a.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                     Content = $"{b.Name}发生预警，当前值{a.CurrentData},安全范围{a.SafeRange}"
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
                                 select new AlarmRecord()
                                 {
                                     Time = a.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                     Content = $"{b.Name}发生预警，{a.AlarmRemark}"
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
        /// <returns></returns>
        public async Task<PagedResultDto<HighFreqAlarmDetector>> GetFireUnitHighFreqAlarmEle(GetPageByFireUnitIdInput input)
        {
            var output = new PagedResultDto<HighFreqAlarmDetector>();
            await Task.Run(() =>
            {
                var alarmFire = _alarmToElectricRep.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime >= DateTime.Now.Date.AddDays(-30));
                int highFreq = int.Parse(ConfigHelper.Configuration["FireDomain:HighFreqAlarm"]);
                var alarmDevices = alarmFire.GroupBy(p =>  p.DetectorId ).Where(p => p.Count() > highFreq)
                    .Select(p => new { DetectorId = p.Key, Count = p.Count() });
                var lstResult = from a in alarmDevices
                                join b in _detectorRep.GetAll()
                                on a.DetectorId equals b.Id
                                orderby a.Count descending
                                select new HighFreqAlarmDetector()
                                {
                                    Name = b.Name,
                                    Time = b.AlarmTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                    Count = a.Count
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
                var alarmDevices = alarmFire.GroupBy(p => p.DetectorId).Where(p => p.Count() > highFreq)
                    .Select(p => new { DetectorId = p.Key, Count = p.Count() });
                var lstResult = from a in alarmDevices
                                join b in _detectorRep.GetAll()
                                on a.DetectorId equals b.Id
                                orderby a.Count descending
                                select new HighFreqAlarmDetector()
                                {
                                    Name = b.Name,
                                    Time = b.AlarmTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                    Count = a.Count
                                };
                var lst = lstResult.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
                output.TotalCount = lstResult.Count();
                output.Items = lst;
            });
            return output;
        }
        /// <summary>
        /// 设备设施故障待处理故障查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<PendingFault>> GetFireUnitPendingFault(GetPageByFireUnitIdInput input)
        {
            var output = new PagedResultDto<PendingFault>();
            await Task.Run(() =>
            {
                var lstResult = _faultRep.GetAll().Where(p => p.FireUnitId == input.Id && p.ProcessState == 0)
                .OrderByDescending(p => p.CreationTime)
                .Select(p => new PendingFault()
                {
                    Time = p.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    Content = p.FaultRemark
                });
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
                var alarmElec = _alarmToElectricRep.GetAll().Where(p => p.CreationTime >= DateTime.Now.Date.AddDays(-30));
                output.JoinFireUnitCount = alarmElec.Count();
                //联网防火单位类型数量分布
                var detectElec = _detectorRep.GetAll().Where(p => p.FireSysType == FireSysType.Electric);
                var joinTypeCounts = from a in detectElec
                                     join b in _fireUnitRep.GetAll()
                                     on a.FireUnitId equals b.Id
                                     group b.TypeId by new { b.Id, b.TypeId } into g
                                     join c in _fireUnitTypeRep.GetAll()
                                     on g.Key.TypeId equals c.Id
                                     group g.Count() by c.Name into g2
                                     select new JoinTypeCount()
                                     {
                                         Type = g2.Key,
                                         Count = g2.FirstOrDefault()
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
                                       join c in _gatewayRep.GetAll().Where(p=>p.Status==Common.Enum.GatewayStatus.Offline)
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
                .GroupBy(p=>p.CreationTime.ToString("yyyy年MM月")).Select(p=>new MonthAlarmCount() { Month = p.Key, Count = p.Count() });
                output.MonthAlarmCounts = monthAlarmCounts.ToList();
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
                output.Top10FireUnits = top10FireUnits.ToList();
            });
            return output;
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
                //安全用电数据：管控点位数量、网关状态、最近30天报警次数（可查）、高频报警部件数量（可查）
                var alarmFire = _alarmToFireRep.GetAll().Where(p => p.CreationTime >= DateTime.Now.Date.AddDays(-30));
                output.JoinFireUnitCount = alarmFire.Count();
                //联网防火单位类型数量分布
                var detectFire = _detectorRep.GetAll().Where(p => p.FireSysType == FireSysType.Fire);
                var joinTypeCounts = from a in detectFire
                                     join b in _fireUnitRep.GetAll()
                                     on a.FireUnitId equals b.Id
                                     group b.TypeId by new { b.Id, b.TypeId } into g
                                     join c in _fireUnitTypeRep.GetAll()
                                     on g.Key.TypeId equals c.Id
                                     group g.Count() by c.Name into g2
                                     select new JoinTypeCount()
                                     {
                                         Type = g2.Key,
                                         Count = g2.FirstOrDefault()
                                     };
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
                .GroupBy(p => p.CreationTime.ToString("yyyy年MM月")).Select(p => new MonthAlarmCount() { Month = p.Key, Count = p.Count() });
                output.MonthAlarmCounts = monthAlarmCounts.ToList();
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
                output.Top10FireUnits = top10FireUnits.ToList();
            });
            return output;
        }
    }
}
