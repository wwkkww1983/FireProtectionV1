using Abp.Domain.Services;
using FireProtectionV1.SettingCore.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.SettingCore.Manager
{
    public interface IFireSettingManager : IDomainService
    {
        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        Task SaveSetting(List<FireSetting> settings);
        /// <summary>
        /// 获取所有设置
        /// </summary>
        /// <returns></returns>
        Task<List<FireSetting>> GetAllSetting();
        /// <summary>
        /// 根据设置名获取某一具体设置
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<FireSetting> GetByName(string name);
        /// <summary>
        /// 设置初始化
        /// MinValue、MaxValue都是数值型，有的设置只有下限值或只有上限值
        /// 因此约定：MaxValue=10000表示无上限值，MinValue=-10000表示无下限值
        /// </summary>
        /// <returns></returns>
        Task InitSetting();
    }
}
