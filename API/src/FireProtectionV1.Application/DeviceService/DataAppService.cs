using FireProtectionV1.AppService;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Manager;
using FireProtectionV1.SettingCore.Manager;
using FireProtectionV1.SettingCore.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.DeviceService
{
    public class DataAppService : AppServiceBase
    {
        IFireSettingManager _fireSettingManager;
        IDeviceManager _detectorManager;
        IAlarmManager _alarmManager;
        public DataAppService(
            IFireSettingManager fireSettingManager,
            IDeviceManager detectorManager,IAlarmManager alarmManager)
        {
            _fireSettingManager = fireSettingManager;
            _detectorManager = detectorManager;
            _alarmManager = alarmManager;
        }
        /// <summary>
        /// 新增安全用电数据温度(超限判断后新增报警)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddDataElecT(AddDataElecInput input)
        {
            var setting =await _fireSettingManager.GetByName("CableTemperature");
            if(input.Analog>=(decimal)setting.MaxValue)
                await _alarmManager.AddAlarmElec(input,$"{setting.MaxValue}{input.Unit}");
        }
        /// <summary>
        /// 新增安全用电数据电流(超限判断后新增报警)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddDataElecE(AddDataElecInput input)
        {
            var setting = await _fireSettingManager.GetByName("ResidualCurrent");
            if (input.Analog >= (decimal)setting.MaxValue)
                await _alarmManager.AddAlarmElec(input, $"{setting.MaxValue}{input.Unit}");
        }
        /// <summary>
        /// 新增火灾监控设备报警
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddAlarmFire(AddAlarmFireInput input)
        {
            await _alarmManager.AddAlarmFire(input);
        }
    }
}
