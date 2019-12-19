using FireProtectionV1.Schedule.Manager;
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
        /// 将过期火警数据的核警状态改为已过期
        /// </summary>
        /// <returns></returns>
        public async Task UpdateFireAlarmCheckState()
        {
            await _scheduleManager.UpdateFireAlarmCheckState();
        }
    }
}
