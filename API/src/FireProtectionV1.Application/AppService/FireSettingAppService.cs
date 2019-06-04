using FireProtectionV1.SettingCore.Dto;
using FireProtectionV1.SettingCore.Manager;
using FireProtectionV1.SettingCore.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    public class FireSettingAppService : AppServiceBase
    {
        IFireSettingManager _manager;

        public FireSettingAppService(IFireSettingManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// 获取所有设置
        /// CableTemperature:电缆温度℃ | ResidualCurrent:剩余电流mA | PoolWaterPressure:消防水池水压KPa | PoolWaterHeight:消防水池液位高度M | HydrantPressure:市政消火栓水压kPa
        /// </summary>
        /// <returns></returns>
        public async Task<List<FireSetting>> GetAllSetting()
        {
            return await _manager.GetAllSetting();
        }

        /// <summary>
        /// 根据设置名获取某一具体设置
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<FireSetting> GetByName(string name)
        {
            return await _manager.GetByName(name);
        }

        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public async Task SaveSetting(List<FireSettingInput> settings)
        {
            await _manager.SaveSetting(settings);
        }

        /// <summary>
        /// 设置初始化
        /// MinValue、MaxValue都是数值型，有的设置只有下限值或只有上限值
        /// 因此约定：MaxValue=10000表示无上限值，MinValue=-10000表示无下限值
        /// </summary>
        /// <returns></returns>
        public async Task InitSetting()
        {
            await _manager.InitSetting();
        }
    }
}
