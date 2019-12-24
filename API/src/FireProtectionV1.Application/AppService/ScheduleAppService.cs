using FireProtectionV1.Schedule.Manager;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    /// <summary>
    /// 计划任务服务接口
    /// </summary>
    public class ScheduleAppService : AppServiceBase
    {
        IScheduleManager _scheduleManager;

        public ScheduleAppService(
            IScheduleManager scheduleManager)
        {
            _scheduleManager = scheduleManager;
        }

        /// <summary>
        /// 执行时间：每1分钟
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task EveryMinute()
        {
            await _scheduleManager.EveryMinute();
        }
        /// <summary>
        /// 执行时间：每1小时
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task EveryHour()
        {
            await _scheduleManager.EveryHour();
        }
    }
}
