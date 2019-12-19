using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.Schedule.Manager
{
    /// <summary>
    /// 计划任务接口
    /// </summary>
    public interface IScheduleManager : IDomainService
    {
        /// <summary>
        /// 执行时间：每1分钟
        /// 将过期火警数据的核警状态改为已过期
        /// </summary>
        /// <returns></returns>
        Task UpdateFireAlarmCheckState();
    }
}
