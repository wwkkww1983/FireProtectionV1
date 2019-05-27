﻿using FireProtectionV1.DataReportCore.Dto;
using FireProtectionV1.DataReportCore.Manager;
using FireProtectionV1.Dto;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    public class DataReportAppService : AppServiceBase
    {
        IDutyManager _dutyManager;
        IPatrolManager _patrolManager;
        IDataReportManager _manager;
        IFireWorkingManager _fireWorkingManager;

        public DataReportAppService(
            IDutyManager dutyManager,
            IPatrolManager patrolManager,
            IDataReportManager manager, IFireWorkingManager fireWorkingManager)
        {
            _dutyManager = dutyManager;
            _patrolManager = patrolManager;
            _manager = manager;
            _fireWorkingManager = fireWorkingManager;
        }

        /// <summary>
        /// 数据监控页
        /// </summary>
        /// <returns></returns>
        public async Task<List<DataMinotorOutput>> GetDataMinotor()
        {
            return await _manager.GetDataMinotor();
        }
        /// <summary>
        /// 安全用电数据分析
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<FireWorking.Dto.GetAreasAlarmElectricOutput> GetAreasAlarmElectric(GetAreasAlarmElectricInput input)
        {
            return await _fireWorkingManager.GetAreasAlarmElectric(input);
        }
        /// <summary>
        /// 火警预警数据分析
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetAreasAlarmFireOutput> GetAreasAlarmFire(GetAreasAlarmFireInput input)
        {
            return await _fireWorkingManager.GetAreasAlarmFire(input);
        }
        /// <summary>
        /// 值班巡查数据分析
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public async Task<GetAreasPatrolDutyOutput> GetAreasPatrolDuty(int UserId)
        {
            DateTime now = DateTime.Now;
            DateTime nowMonDay1 = now.Date.AddDays(1 - now.Day);
            var output = new GetAreasPatrolDutyOutput();
            await Task.Run(() =>
            {
                output.PatrolCount = _patrolManager.GetPatrolDataAll().Count();
                output.PatrolMonthCounts = new List<MonthCount>();
                for (int i = 3; i >= 1; i--)
                {
                    DateTime mon = now.AddMonths(-i);
                    output.PatrolMonthCounts.Add(new MonthCount()
                    {
                        Month = mon.ToString("yyyy-MM"),
                        Count = _patrolManager.GetPatrolDataMonth(mon.Year, mon.Month).Count()
                    });
                }
                output.NoWork7DayCount = _patrolManager.GetNoPatrol7DayFireUnits().Count();
                output.PatrolFireUnitManualOuputs = _patrolManager.GetNoPatrol7DayFireUnits().OrderByDescending(p => p.LastTime).Take(10).ToList();

                output.DutyCount = _dutyManager.GetDutyDataAll().Count();
                output.DutyMonthCounts = new List<MonthCount>();
                for (int i = 3; i >= 1; i--)
                {
                    DateTime mon = now.AddMonths(-i);
                    output.DutyMonthCounts.Add(new MonthCount()
                    {
                        Month = mon.ToString("yyyy-MM"),
                        Count = _dutyManager.GetDutyDataMonth(mon.Year, mon.Month).Count()
                    });
                }
                output.NoWork1DayCount = _dutyManager.GetNoDuty1DayFireUnits().Count();
                output.DutyFireUnitManualOuputs = _dutyManager.GetNoDuty1DayFireUnits().OrderByDescending(p => p.LastTime).Take(10).ToList();
            });
            return output;
        }
    }
}
