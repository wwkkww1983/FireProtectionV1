﻿using Abp.Domain.Services;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.Schedule.Manager
{
    /// <summary>
    /// 计划任务服务
    /// </summary>
    public class ScheduleManager : DomainService, IScheduleManager
    {
        ISqlRepository _sqlRepository;

        public ScheduleManager(
            ISqlRepository sqlRepository)
        {
            _sqlRepository = sqlRepository;
        }
        /// <summary>
        /// 执行时间：每1分钟
        /// </summary>
        /// <returns></returns>
        public Task EveryMinute()
        {
            // 将过期火警数据的核警状态改为已过期
            string sql = $"update alarmtofire set checkstate={FireAlarmCheckState.Expire} where checkstate={FireAlarmCheckState.UnCheck} and TIMESTAMPDIFF(MINUTE,CreationTime,NOW()) > 60";
            return Task.FromResult(_sqlRepository.Execute(sql));
        }
    }
}
