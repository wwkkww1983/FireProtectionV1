using Abp.Domain.Repositories;
using Abp.Domain.Services;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Manager;
using FireProtectionV1.FireWorking.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.Schedule.Manager
{
    /// <summary>
    /// 计划任务服务
    /// </summary>
    public class ScheduleManager : IScheduleManager
    {
        ISqlRepository _sqlRepository;
        IAlarmManager _alarmManager;
        IDeviceManager _deviceManager;

        public ScheduleManager(
            ISqlRepository sqlRepository,
            IAlarmManager alarmManager,
            IDeviceManager deviceManager)
        {
            _sqlRepository = sqlRepository;
            _alarmManager = alarmManager;
            _deviceManager = deviceManager;
        }

        /// <summary>
        /// 执行时间：每1小时
        /// </summary>
        /// <returns></returns>
        public async Task EveryHour()
        {
            Random random = new Random();
            try
            {
                // 自动给演示的防火单位增加一条火警联网数据
                await _alarmManager.AddAlarmFire(new AddAlarmFireInput()
                {
                    DetectorSn = "0001区002号",
                    FireAlarmDeviceSn = "11.0.0.0.3.32"
                });
            }
            catch { }
            try
            {
                // 自动给演示的防火单位增加一条电气火灾数据
                string[] signs = new string[] { "A", "L", "N" };
                string sign = signs[random.Next(0, signs.Length)];
                double value = 0;
                if (sign.Equals("A")) value = random.Next(1, 350);
                else value = random.Next(20, 70);

                await _deviceManager.AddElecRecord(new AddDataElecInput()
                {
                    FireElectricDeviceSn = "TSJ-DQ101912001",
                    Sign = sign,
                    Analog = value
                });
            }
            catch { }
        }

        /// <summary>
        /// 执行时间：每1分钟
        /// </summary>
        /// <returns></returns>
        public Task EveryMinute()
        {
            // 将过期火警数据的核警状态改为已过期
            string sql = $"update alarmtofire set checkstate={(int)FireAlarmCheckState.Expire} where checkstate={(int)FireAlarmCheckState.UnCheck} and TIMESTAMPDIFF(MINUTE,CreationTime,NOW()) > 60";
            int result = _sqlRepository.Execute(sql);
            return Task.FromResult(result);
        }
    }
}
