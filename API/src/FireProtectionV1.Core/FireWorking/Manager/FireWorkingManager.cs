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
                var fireunit = await _fireUnitRep.SingleAsync(p => p.Id == input.Id);
                //安全用电数据：管控点位数量、网关状态、最近30天报警次数（可查）、高频报警部件数量（可查）
                var alarmElec = _alarmToElectricRep.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime >= DateTime.Now.Date.AddDays(-30));
                output.Elec30DayCount = alarmElec.Count();
                output.ElecHighCount = alarmElec.GroupBy(p =>p.DetectorId).Where(p => p.Count() > highFreq).Count();
                output.ElecPointsCount = _detectorRep.GetAll().Where(p=>p.FireUnitId==input.Id)
                                          (from det in _detectorElectricRep.GetAll()
                                          join con in _controllerElectricRep.GetAll().Where(p => p.FireUnitId == input.Id)
                                          on det.ControllerId equals con.Id
                                          select det.Id).Count();
                var netStates = _controllerElectricRep.GetAll().Where(p => p.FireUnitId == input.Id).Select(p => p.Status);
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
                output.FireHighCount = alarmFire.GroupBy(p =>new { p.DeviceId, p.DeviceType }).Where(p => p.Count() > highFreq).Count();
                output.FirePointsCount = (from det in _detectorFireRep.GetAll()
                                          join con in _controllerFireRep.GetAll().Where(p => p.FireUnitId == input.Id)
                                          on det.ControllerId equals con.Id
                                          select det.Id).Count();
                //火警控制器可能不知道下面有多少点位，就以控制器数量充当
                output.FirePointsCount += _controllerFireRep.GetAll().Where(p => p.FireUnitId == input.Id).Count();
                netStates = _controllerFireRep.GetAll().Where(p => p.FireUnitId == input.Id).Select(p => p.Status);
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
            var alarm30 = _alarmToElectricRep.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime >= DateTime.Now.Date.AddDays(-30))
                .OrderByDescending(p => p.CreationTime);
            List<AlarmRecord> lstRecords = new List<AlarmRecord>();
            foreach (var alarm in alarm30)
            {
                AlarmRecord record = new AlarmRecord();
                record.Time = alarm.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
                if (alarm.DeviceType == Common.Enum.DeviceType.ControllerElectric)
                {
                    var device = await _controllerElectricRep.SingleAsync(p => p.Id == alarm.DeviceId);
                    record.Content = $"{device.Sn}发生预警，当前值{alarm.CurrentData},安全范围{alarm.SafeRange}";
                }else if (alarm.DeviceType == Common.Enum.DeviceType.DetectorElectric)
                {
                    var device = await _detectorElectricRep.SingleAsync(p => p.Id == alarm.DeviceId);
                    record.Content = $"{device.Name}发生预警，当前值{alarm.CurrentData},安全范围{alarm.SafeRange}";
                }
            }
            var lst = lstRecords.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return new PagedResultDto<AlarmRecord>(lstRecords.Count(), lst);
        }
        /// <summary>
        /// 火警预警最近30天报警记录查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<AlarmRecord>> GetFireUnit30DayAlarmFire(GetPageByFireUnitIdInput input)
        {
            var alarm30 = _alarmToFireRep.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime >= DateTime.Now.Date.AddDays(-30))
                .OrderByDescending(p => p.CreationTime);
            List<AlarmRecord> lstRecords = new List<AlarmRecord>();
            foreach (var alarm in alarm30)
            {
                AlarmRecord record = new AlarmRecord();
                record.Time = alarm.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
                if (alarm.DeviceType == Common.Enum.DeviceType.ControllerFire)
                {
                    var device = await _controllerElectricRep.SingleAsync(p => p.Id == alarm.DeviceId);
                    record.Content = $"{device.Sn}发生预警，当前值{alarm.AlarmRemark}";
                }
                else if (alarm.DeviceType == Common.Enum.DeviceType.DetectorFire)
                {
                    var device = await _detectorElectricRep.SingleAsync(p => p.Id == alarm.DeviceId);
                    record.Content = $"{device.Name}发生预警，当前值{alarm.AlarmRemark}";
                }
                lstRecords.Add(new AlarmRecord());
            }
            var lst = lstRecords.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return new PagedResultDto<AlarmRecord>(lstRecords.Count(), lst);
        }
        /// <summary>
        /// 安全用电高频报警部件查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnitHighFreqAlarmEleOutput> GetFireUnitHighFreqAlarmEle(GetByFireUnitIdInput input)
        {
            throw new NotImplementedException();
            //var alarmFire = _alarmToElectricRep.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime >= DateTime.Now.Date.AddDays(-30));
            //int highFreq = int.Parse(ConfigHelper.Configuration["FireDomain:HighFreqAlarm"]);
            //var alarmDevices = alarmFire.GroupBy(p => new { p.DeviceId, p.DeviceType }).Where(p => p.Count() > highFreq)
            //    .Select(p=>new { p.Key.DeviceId,p.Key.DeviceType,Count=p.Count()});
            //var v=from a in alarmDevices
            //      join b in 
        }
        /// <summary>
        /// 火警预警高频报警部件查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnitHighFreqAlarmFireOutput> GetFireUnitHighFreqAlarmFire(GetByFireUnitIdInput input)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 设备设施故障待处理故障查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnitPendingFaultOutput> GetFireUnitPendingFault(GetByFireUnitIdInput input)
        {
            throw new NotImplementedException();
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
                var joinTypeCounts = from a in _controllerElectricRep.GetAll()
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
                output.JoinTypeCounts = joinTypeCounts.ToArray();
                //联网监控点位（单位类型柱状图）
                output.JoinPointCount = _detectorElectricRep.GetAll().Count();
                var joinTypePointCounts = from a in _detectorElectricRep.GetAll()
                                     join b in _controllerElectricRep.GetAll()
                                     on a.ControllerId equals b.Id
                                     join c in _fireUnitRep.GetAll()
                                     on b.FireUnitId equals c.Id
                                     join d in _fireUnitTypeRep.GetAll()
                                     on c.TypeId equals d.Id
                                     group a by d.Name into g
                                     select new JoinTypePointCount() { Type = g.Key, Count = g.Count() };
                output.JoinTypePointCounts = joinTypePointCounts.ToArray();
                //网关离线单位
                var offlineFireUnits = from a in _controllerElectricRep.GetAll().Where(p => p.Status == Common.Enum.GatewayStatus.Offline)
                                       join b in _fireUnitRep.GetAll()
                                       on a.FireUnitId equals b.Id
                                       orderby a.StatusChangeTime descending
                                       select new OfflineFireUnit (){ Name= b.Name,Time=a.StatusChangeTime.ToString("yyyy-MM-dd") };
                output.OfflineFireUnitsCount = offlineFireUnits.Count();
                output.OfflineFireUnits = offlineFireUnits.Take(10).ToArray();
                //安全用电累计预警（最近月份流量图）
                DateTime now = DateTime.Now;
                DateTime nowMonDay1 = now.Date.AddDays(1 - now.Day);
                var monthAlarmCounts = _alarmToElectricRep.GetAll().Where(p => p.CreationTime >= nowMonDay1.AddMonths(-3))
                .GroupBy(p=>p.CreationTime.ToString("yyyy年MM月")).Select(p=>new MonthAlarmCount() { Month = p.Key, Count = p.Count() });
                output.MonthAlarmCounts = monthAlarmCounts.ToArray();
                //最近30天报警次数Top10
                var unitAlarmCounts30=_alarmToElectricRep.GetAll().Where(p => p.CreationTime >= now.Date.AddDays(-30))
                .GroupBy(p => p.FireUnitId).Select(p => new { FireUnitId = p.Key, Count = p.Count() }).OrderBy(p=>p.Count);
                var unitCounts30 = from a in unitAlarmCounts30.Take(10)
                                   join b in _fireUnitRep.GetAll()
                                   on a.FireUnitId equals b.Id
                                   select new { a.FireUnitId, a.Count, b.Name };
                var top10FireUnits = from a in unitCounts30
                                     join b in (from det in _detectorElectricRep.GetAll()
                                                join con in _controllerElectricRep.GetAll()
                                                on det.ControllerId equals con.Id
                                                group det by con.FireUnitId)
                                                on a.FireUnitId equals b.Key
                                     select new Top10FireUnit() { AlarmCount = a.Count, Name = a.Name, PointCount = b.Count() };
                output.Top10FireUnits = top10FireUnits.ToArray();
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
                var alarmElec = _alarmToFireRep.GetAll().Where(p => p.CreationTime >= DateTime.Now.Date.AddDays(-30));
                output.JoinFireUnitCount = alarmElec.Count();
                //联网防火单位类型数量分布
                var joinTypeCounts = from a in _controllerFireRep.GetAll()
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
                output.JoinTypeCounts = joinTypeCounts.ToArray();
                //网关离线单位
                var offlineFireUnits = from a in _controllerFireRep.GetAll().Where(p => p.Status == Common.Enum.GatewayStatus.Offline)
                                       join b in _fireUnitRep.GetAll()
                                       on a.FireUnitId equals b.Id
                                       select new OfflineFireUnit() { Name = b.Name, Time = a.StatusChangeTime.ToString("yyyy-MM-dd") };
                output.OfflineFireUnits = offlineFireUnits.ToArray();
                //安全用电累计预警（最近月份流量图）
                DateTime now = DateTime.Now;
                DateTime nowMonDay1 = now.Date.AddDays(1 - now.Day);
                var monthAlarmCounts = _alarmToFireRep.GetAll().Where(p => p.CreationTime >= nowMonDay1.AddMonths(-3))
                .GroupBy(p => p.CreationTime.ToString("yyyy年MM月")).Select(p => new MonthAlarmCount() { Month = p.Key, Count = p.Count() });
                output.MonthAlarmCounts = monthAlarmCounts.ToArray();
                //最近30天报警次数Top10
                var unitAlarmCounts30 = _alarmToFireRep.GetAll().Where(p => p.CreationTime >= now.Date.AddDays(-30))
                .GroupBy(p => p.FireUnitId).Select(p => new { FireUnitId = p.Key, Count = p.Count() }).OrderBy(p => p.Count);
                var unitCounts30 = from a in unitAlarmCounts30.Take(10)
                                   join b in _fireUnitRep.GetAll()
                                   on a.FireUnitId equals b.Id
                                   select new { a.FireUnitId, a.Count, b.Name };
                var top10FireUnits = from a in unitCounts30
                                     join b1 in (from det in _detectorFireRep.GetAll()
                                                join con in _controllerFireRep.GetAll()
                                                on det.ControllerId equals con.Id
                                                group det by con.FireUnitId)
                                     on a.FireUnitId equals b1.Key into g
                                     from b in g.DefaultIfEmpty()
                                     select new Top10FireUnit() { AlarmCount = a.Count, Name = a.Name, PointCount = b!=null?b.Count():
                                     _controllerFireRep.GetAll().Where(p=>p.FireUnitId==a.FireUnitId).Count()};
                output.Top10FireUnits = top10FireUnits.ToArray();
            });
            return output;
        }
    }
}
