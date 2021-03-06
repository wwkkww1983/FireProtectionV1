﻿using Abp.Domain.Services;
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
        /// </summary>
        /// <returns></returns>
        Task EveryMinute();
        /// <summary>
        /// 执行时间：每1小时
        /// </summary>
        /// <returns></returns>
        Task EveryHour();
    }
}
