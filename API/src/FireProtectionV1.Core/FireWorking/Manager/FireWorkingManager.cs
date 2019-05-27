using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
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
            byte elecType = byte.Parse(ConfigHelper.Configuration["FireDomain:FireSysType:Electric"]);
            await Task.Run(() =>
            {
                var fireunit =  _fireUnitRep.Single(p => p.Id == input.Id);
                output.FireUnitName = fireunit.Name;
                //安全用电数据：管控点位数量、网关状态、最近30天报警次数（可查）、高频报警部件数量（可查）
                var alarmElec = _alarmToElectricRep.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime >= DateTime.Now.Date.AddDays(-30));
                output.Elec30DayCount = alarmElec.Count();
                output.ElecHighCount = output.Elec30DayCount == 0 ?
                0 : alarmElec.GroupBy(p => p.DetectorId).Select(p=>new {DetectorId=p.Key, Count = p.Count() })
                .Where(p => p.Count > highFreq).Count();
                var detectsEle = _detectorRep.GetAll().Where(p => p.FireUnitId == input.Id && p.FireSysType == elecType);
                output.ElecPointsCount = detectsEle.Count();
                var netStates = from a in detectsEle
                                join b in _gatewayRep.GetAll()
                                on a.GatewayId equals b.Id
                                select b.Status;
                int netStatesCount = netStates.Count();
                if (netStatesCount > 0)
                {
                    output.ElecStateValue = netStates.First();
                    output.ElecStateName = output.ElecStateValue == Common.Enum.GatewayStatus.Online ? "在线" : "离线";
                    //if (netStatesCount > 1)
                    //    output.ElecState = $"{output.ElecState}({netStates.Select(p => p.Equals(output.ElecState)).Count()}/{netStatesCount})";
                }
                //火警预警数据：管控点位数量、网关状态、最近30天报警次数（可查）、高频报警部件数量（可查）；
                byte fireType = byte.Parse(ConfigHelper.Configuration["FireDomain:FireSysType:Fire"]);
                var alarmFire = _alarmToFireRep.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime >= DateTime.Now.Date.AddDays(-30));
                output.Fire30DayCount = alarmFire.Count();
                output.FireHighCount = output.Fire30DayCount == 0 ? 0 :
                alarmFire.GroupBy(p => p.DetectorId).Select(p => new { DetectorId = p.Key, Count = p.Count() })
                .Where(p => p.Count > highFreq).Count();
                var detectsFire = _detectorRep.GetAll().Where(p => p.FireUnitId == input.Id && p.FireSysType == fireType);
                output.FirePointsCount = detectsFire.Count();
                netStates = from a in detectsFire
                            join b in _gatewayRep.GetAll()
                            on a.GatewayId equals b.Id
                            select b.Status;
                netStatesCount = netStates.Count();
                if (netStatesCount > 0)
                {
                    output.FireStateValue = netStates.First();
                    output.FireStateName = output.FireStateValue == Common.Enum.GatewayStatus.Online ? "在线" : "离线";
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
        /// <param name="detectorTypeId">探测器类型</param>
        /// <returns></returns>
        public async Task<PagedResultDto<AlarmRecord>> GetFireUnit30DayAlarmEle(GetPageByFireUnitIdInput input,int detectorTypeId)
        {
            var output = new PagedResultDto<AlarmRecord>();
            await Task.Run(() =>
            {
                var alarm30 = from a in _alarmToElectricRep.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime >= DateTime.Now.Date.AddDays(-30))
                              join b in _detectorRep.GetAll().Where(p => p.DetectorTypeId == detectorTypeId)
                              on a.DetectorId equals b.Id
                              orderby a.CreationTime descending
                              select a;
                var lstRecords = from a in alarm30
                                 join b in _detectorRep.GetAll()
                                 on a.DetectorId equals b.Id
                                 select new AlarmRecord()
                                 {
                                     Time = a.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                     Content = $"{b.Name}发生预警，当前值{a.CurrentData.ToString("0")},安全范围{a.SafeRange}"
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
        /// <param name="onlyElecOrTemp"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<HighFreqAlarmDetector>> GetFireUnitHighFreqAlarmEle(GetPageByFireUnitIdInput input, string onlyElecOrTemp = null)
        {
            var output = new PagedResultDto<HighFreqAlarmDetector>();
            await Task.Run(() =>
            {
                var alarmFire = string.IsNullOrEmpty(onlyElecOrTemp) ?
                _alarmToElectricRep.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime >= DateTime.Now.Date.AddDays(-30))
                : from a in _alarmToElectricRep.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime >= DateTime.Now.Date.AddDays(-30))
                  join b in _detectorRep.GetAll().Where(p => p.DetectorTypeId == (onlyElecOrTemp.Equals("elec") ? 6 : 15))
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
                                orderby a.AlramCount descending
                                select new HighFreqAlarmDetector()
                                {
                                    Name = b.Name,
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
                                orderby a.AlramCount descending
                                select new HighFreqAlarmDetector()
                                {
                                    Name = b.Name,
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
            var query = _faultRep.GetAll().GroupBy(p => p.FireUnitId).Select(p => new FireUnitFaultOuput()
            {
                FireUnitId = p.Key,
                FaultCount = p.Count(),
                ProcessedCount = p.Select(p1 => p1.ProcessState == 1).Count(),
                PendingCount = p.Select(p1 => p1.ProcessState == 0).Count()
            });
            if (!string.IsNullOrEmpty(input.Name))
            {
                query = from a in query
                        join b in _fireUnitRep.GetAll().Where(p => p.Name.Contains(input.Name))
                        on a.FireUnitId equals b.Id
                        select a;
            }
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
            byte electricType = byte.Parse(ConfigHelper.Configuration["FireDomain:FireSysType:Electric"]);
            await Task.Run(() =>
            {
                //安全用电数据：管控点位数量、网关状态、最近30天报警次数（可查）、高频报警部件数量（可查）
                //var alarmElec = _alarmToElectricRep.GetAll().Where(p => p.CreationTime >= DateTime.Now.Date.AddDays(-30));
                //var elecFireUnits=_detectorRep.GetAll().Where(p=>p.FireSysType==)
                //output.JoinFireUnitCount = alarmElec.Count();
                //联网防火单位类型数量分布
                var detectElec = from a in _detectorRep.GetAll().Where(p => p.FireSysType == electricType)
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
                FreqCount = p.GroupBy(p1 => p1.DetectorId).Select(p1 => p1.Count() > highFreq).Count()
            });
            GatewayStatus status = GatewayStatus.Unusual;
            if (!string.IsNullOrEmpty(input.GetwayStatusValue))
                status = (GatewayStatus)Enum.Parse(typeof(GatewayStatus), input.GetwayStatusValue);
            var v = from a in alarmFireUnits
                    join b in _gatewayRep.GetAll().Where(p => string.IsNullOrEmpty(input.GetwayStatusValue) ? true : p.Status == status)
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
                        StatusName = b.Status == Common.Enum.GatewayStatus.Online ? "在线" : "离线"
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
            return GetAreas30DayElecAlarmListOnlyId(input, 15);
        }
        /// <summary>
        /// （所有防火单位）安全用电监控列表（剩余电流）
        /// </summary>
        /// <returns></returns>
        public Task<PagedResultDto<GetAreas30DayFireAlarmOutput>> GetAreas30DayElecAlarmList(GetPagedFireUnitListFilterTypeInput input)
        {
            return GetAreas30DayElecAlarmListOnlyId(input, 6);
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
                FreqCount = p.GroupBy(p1 => p1.DetectorId).Select(p1 => p1.Count() > highFreq).Count()
            });
            GatewayStatus status = GatewayStatus.Unusual;
            if (!string.IsNullOrEmpty(input.GetwayStatusValue))
                status = (GatewayStatus)Enum.Parse(typeof(GatewayStatus), input.GetwayStatusValue);

            var v = from a in alarmFireUnits
                    join b in _gatewayRep.GetAll().Where(p => string.IsNullOrEmpty(input.GetwayStatusValue) ? true : p.Status == status)
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
                        TypeName = d.Name,
                        AlarmTime = a.LastAlarmTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        AlarmCount = a.AlarmCount,
                        HighFreqCount = a.FreqCount,
                        StatusValue = b.Status,
                        StatusName = b.Status == Common.Enum.GatewayStatus.Online ? "在线" : "离线"
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
            byte fireType = byte.Parse(ConfigHelper.Configuration["FireDomain:FireSysType:Fire"]);
            GetAreasAlarmFireOutput output = new GetAreasAlarmFireOutput();
            await Task.Run(() =>
            {
                //火警预警数据：管控点位数量、网关状态、最近30天报警次数（可查）、高频报警部件数量（可查）
                var detectFire = from a in _detectorRep.GetAll().Where(p => p.FireSysType == fireType)
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
        /// <summary>
        /// （所有防火单位）值班巡查监控（巡查记录）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<GetFireUnitPatrolListOutput> GetFireUnitPatrolList(GetPagedFireUnitListInput input)
        {
            DateTime now = DateTime.Now;
            var output = new GetFireUnitPatrolListOutput();
            output.NoWork7DayCount=_patrolRep.GetAll().Where(p => p.CreationTime >= now.Date.AddDays(-7)).GroupBy(p => p.FireUnitId).Count();
            var query = from a in _fireUnitRep.GetAll().Where(p=>string.IsNullOrEmpty(input.Name)?true:p.Name.Contains(input.Name))
                        join b in _patrolRep.GetAll().GroupBy(p => p.FireUnitId).Select(p => new
                        {
                            FireUnitId = p.Key,
                            LastTime = p.Max(p1 => p1.CreationTime),
                            Day30Count = p.Where(p1 => p1.CreationTime >= now.Date.AddDays(-30)).Count()
                        })
                        on a.Id equals b.FireUnitId into g
                        from b2 in g.DefaultIfEmpty()
                        select new FireUnitManualOuput()
                        {
                            FireUnitId = a.Id,
                            FireUnitName = a.Name,
                            LastTime = b2 == null ? "" : b2.LastTime.ToString("yyyy-MM-dd"),
                            Recent30DayCount = b2 == null ? 0 : b2.Day30Count
                        };
            query = query.OrderByDescending(p => p.LastTime);
            output.PagedResultDto = new PagedResultDto<FireUnitManualOuput>(query.Count()
                , query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList());
            return Task.FromResult<GetFireUnitPatrolListOutput>(output);
        }
        public IQueryable<DataToPatrol> GetPatrolDataAll()
        {
            return _patrolRep.GetAll();
        }
        public IQueryable<DataToPatrol> GetPatrolDataDuration(DateTime start,DateTime end)
        {
            return _patrolRep.GetAll().Where(p=>p.CreationTime>=start&&p.CreationTime<=end);
        }
        public IQueryable<FireUnitManualOuput> GetNoPatrol7DayFireUnits()
        {
            DateTime now = DateTime.Now;
            var workFireUnits = from a in _patrolRep.GetAll().Where(p => p.CreationTime >= now.Date.AddDays(-7))
                                    .GroupBy(p => p.FireUnitId).Select(p => p.Key).ToList()
                                join b in _fireUnitRep.GetAll()
                                on a equals b.Id
                                select b;

            var noWorkFireUnits = _fireUnitRep.GetAll().Except(workFireUnits);
            var query = from a in noWorkFireUnits
                        join b in _patrolRep.GetAll().GroupBy(p => p.FireUnitId).Select(p => new
                        {
                            FireUnitId = p.Key,
                            LastTime = p.Max(p1 => p1.CreationTime),
                            Day30Count = p.Where(p1 => p1.CreationTime >= now.Date.AddDays(-30)).Count()
                        })
                        on a.Id equals b.FireUnitId into g
                        from b2 in g.DefaultIfEmpty()
                        select new FireUnitManualOuput()
                        {
                            FireUnitId = a.Id,
                            FireUnitName = a.Name,
                            LastTime = b2 == null ? "" : b2.LastTime.ToString("yyyy-MM-dd"),
                            Recent30DayCount = b2 == null ? 0 : b2.Day30Count
                        };
            return query;
        }
        /// <summary>
        /// （所有防火单位）值班巡查监控（值班记录）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<GetFireUnitDutyListOutput> GetFireUnitDutyList(GetPagedFireUnitListInput input)
        {
            DateTime now = DateTime.Now;
            var output = new GetFireUnitDutyListOutput();
            output.NoWork1DayCount = _dutyRep.GetAll().Where(p => p.CreationTime >= now.Date.AddDays(-1)).GroupBy(p => p.FireUnitId).Count();
            var query = from a in _fireUnitRep.GetAll().Where(p => string.IsNullOrEmpty(input.Name) ? true : p.Name.Contains(input.Name))
                        join b in _dutyRep.GetAll().GroupBy(p => p.FireUnitId).Select(p => new
                        {
                            FireUnitId = p.Key,
                            LastTime = p.Max(p1 => p1.CreationTime),
                            Day30Count = p.Where(p1 => p1.CreationTime >= now.Date.AddDays(-30)).Count()
                        })
                        on a.Id equals b.FireUnitId into g
                        from b2 in g.DefaultIfEmpty()
                        select new FireUnitManualOuput()
                        {
                            FireUnitId = a.Id,
                            FireUnitName = a.Name,
                            LastTime = b2==null?"":b2.LastTime.ToString("yyyy-MM-dd"),
                            Recent30DayCount = b2==null?0:b2.Day30Count
                        };
            query = query.OrderByDescending(p => p.LastTime);
            output.PagedResultDto = new PagedResultDto<FireUnitManualOuput>(query.Count()
                , query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList());
            return Task.FromResult<GetFireUnitDutyListOutput>(output);
        }
        /// <summary>
        /// （所有防火单位）超过7天没有巡查记录的单位列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public  Task<GetFireUnitPatrolListOutput> GetNoPatrol7DayFireUnitList(PagedRequestByUserIdDto input)
        {
            DateTime now = DateTime.Now;
            var output = new GetFireUnitPatrolListOutput();
            var workFireUnits = from a in _patrolRep.GetAll().Where(p => p.CreationTime >= now.Date.AddDays(-7))
                                    .GroupBy(p => p.FireUnitId).Select(p =>p.Key).ToList()
                                join b in _fireUnitRep.GetAll()
                                on a equals b.Id
                                select b;

            var noWorkFireUnits = _fireUnitRep.GetAll().Except(workFireUnits);
            output.NoWork7DayCount = noWorkFireUnits.Count();
            var query = from a in noWorkFireUnits
                        join b in _patrolRep.GetAll().Where(p => p.CreationTime >= now.Date.AddDays(-7)).GroupBy(p => p.FireUnitId).Select(p => new
                        {
                            FireUnitId = p.Key,
                            LastTime = p.Max(p1 => p1.CreationTime),
                            Day30Count = p.Where(p1 => p1.CreationTime >= now.Date.AddDays(-30)).Count()
                        })
                        on a.Id equals b.FireUnitId into g
                        from b2 in g.DefaultIfEmpty()
                        select new FireUnitManualOuput()
                        {
                            FireUnitId = a.Id,
                            FireUnitName = a.Name,
                            LastTime = b2 == null ? "" : b2.LastTime.ToString("yyyy-MM-dd"),
                            Recent30DayCount = b2 == null ? 0 : b2.Day30Count
                        };
            query = query.OrderByDescending(p => p.LastTime);
            output.PagedResultDto = new PagedResultDto<FireUnitManualOuput>(query.Count()
                , query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList());
            return Task.FromResult<GetFireUnitPatrolListOutput>(output);
        }
        /// <summary>
        /// （所有防火单位）超过1天没有值班记录的单位列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<GetFireUnitDutyListOutput> GetNoDuty1DayFireUnitList(PagedRequestByUserIdDto input)
        {
            DateTime now = DateTime.Now;
            var output = new GetFireUnitDutyListOutput();
            var workFireUnits = from a in _dutyRep.GetAll().Where(p => p.CreationTime >= now.Date.AddDays(-1)).GroupBy(p => p.FireUnitId)
                                .Select(p=>p.Key).ToList()
                                join b in _fireUnitRep.GetAll()
                                on a equals b.Id
                                select b;
            var noWorkFireUnits = _fireUnitRep.GetAll().Except(workFireUnits);
            output.NoWork1DayCount = noWorkFireUnits.Count();
            var query = from a in noWorkFireUnits
                        join b in _dutyRep.GetAll().Where(p => p.CreationTime >= now.Date.AddDays(-1)).GroupBy(p => p.FireUnitId).Select(p => new
                        {
                            FireUnitId = p.Key,
                            LastTime = p.Max(p1 => p1.CreationTime),
                            Day30Count = p.Where(p1 => p1.CreationTime >= now.Date.AddDays(-30)).Count()
                        })
                        on a.Id equals b.FireUnitId into g
                        from b2 in g.DefaultIfEmpty()
                        select new FireUnitManualOuput()
                        {
                            FireUnitId = a.Id,
                            FireUnitName = a.Name,
                            LastTime = b2 == null ? "" : b2.LastTime.ToString("yyyy-MM-dd"),
                            Recent30DayCount = b2 == null ? 0 : b2.Day30Count
                        };
            query = query.OrderByDescending(p => p.LastTime);
            output.PagedResultDto = new PagedResultDto<FireUnitManualOuput>(query.Count()
                , query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList());
            return Task.FromResult<GetFireUnitDutyListOutput>(output);
        }
    }
}
