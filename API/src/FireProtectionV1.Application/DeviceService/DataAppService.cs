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
        public async Task<AddDataOutput> AddDataElecT(AddDataElecInput input)
        {
            var setting =await _fireSettingManager.GetByName("CableTemperature");
            Console.WriteLine($"{DateTime.Now} 收到模拟量值 AddDataElecT Analog:{input.Analog}{input.Unit} 部件地址：{input.Identify} 网关地址：{input.GatewayIdentify}");
            if (input.Analog >= (decimal)setting.MaxValue)
            {
                Console.WriteLine($"{DateTime.Now} 触发报警 Analog:{input.Analog}{input.Unit} 报警限值:{setting.MaxValue} 部件地址：{input.Identify} 网关地址：{input.GatewayIdentify}");
                return await _alarmManager.AddAlarmElec(input,$"{setting.MaxValue}{input.Unit}");
            }
            return await Task.FromResult<AddDataOutput>(new AddDataOutput() { IsDetectorExit = true });
        }
        /// <summary>
        /// 新增安全用电数据电流(超限判断后新增报警)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AddDataOutput> AddDataElecE(AddDataElecInput input)
        {
            var setting = await _fireSettingManager.GetByName("ResidualCurrent");
            Console.WriteLine($"{DateTime.Now} 收到模拟量值 AddDataElecE Analog:{input.Analog}{input.Unit} 部件地址：{input.Identify} 网关地址：{input.GatewayIdentify}");
            if (input.Analog >= (decimal)setting.MaxValue)
            {
                Console.WriteLine($"{DateTime.Now} 触发报警 Analog:{input.Analog}{input.Unit} 报警限值:{setting.MaxValue} 部件地址：{input.Identify} 网关地址：{input.GatewayIdentify}");
                return await _alarmManager.AddAlarmElec(input, $"{setting.MaxValue}{input.Unit}");
            }
            return await Task.FromResult<AddDataOutput>(new AddDataOutput() { IsDetectorExit = true });
        }
        /// <summary>
        /// 新增火灾监控设备报警
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AddDataOutput> AddAlarmFire(AddAlarmFireInput input)
        {
            Console.WriteLine($"{DateTime.Now} 收到报警 AddAlarmFire 部件类型:{input.DetectorGBType.ToString()} 部件地址：{input.Identify} 网关地址：{input.GatewayIdentify}");
            return await _alarmManager.AddAlarmFire(input);
        }
    }
}
