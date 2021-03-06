﻿using Abp.Domain.Repositories;
using Abp.Domain.Services;
using FireProtectionV1.Common.Helper;
using FireProtectionV1.SettingCore.Dto;
using FireProtectionV1.SettingCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.SettingCore.Manager
{
    public class FireSettingManager : DomainService, IFireSettingManager
    {
        IRepository<FireSetting> _settingRepository;

        public FireSettingManager(IRepository<FireSetting> settingRepository)
        {
            _settingRepository = settingRepository;
        }
        /// <summary>
        /// 获取所有设置
        /// </summary>
        /// <returns></returns>
        public async Task<List<FireSetting>> GetAllSetting()
        {
            List<FireSetting> settings = await _settingRepository.GetAllListAsync();
            return settings;
        }

        /// <summary>
        /// 根据设置名获取某一具体设置
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<FireSetting> GetByName(string name)
        {
            return await _settingRepository.SingleAsync(item => name.Equals(item.Name));
        }

        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public async Task SaveSetting(List<FireSettingInput> settings)
        {
            foreach (FireSettingInput setting in settings)
            {
                var set = _settingRepository.GetAll().Where(u=>u.Name==setting.Name).FirstOrDefault();
                if (set == null) continue;
                set.Name = setting.Name;
                set.MinValue = setting.MinValue;
                set.MaxValue = setting.MaxValue;
                await _settingRepository.InsertOrUpdateAsync(set);
            }
        }

        /// <summary>
        /// 设置初始化
        /// MinValue、MaxValue都是数值型，有的设置只有下限值或只有上限值
        /// 因此约定：MaxValue=10000表示无上限值，MinValue=-10000表示无下限值
        /// </summary>
        /// <returns></returns>
        public async Task InitSetting()
        {
            // 电缆温度℃
            var setlist = _settingRepository.GetAll();
            var setting = setlist.Where(item => "CableTemperature".Equals(item.Name)).Count();
            if (setting == 0)
            {
                await _settingRepository.InsertAsync(new FireSetting()
                {
                    Name = "CableTemperature",
                    MinValue = -20,
                    MaxValue = 100
                });
            }
            // 剩余电流mA
            setting = setlist.Where(item => "ResidualCurrent".Equals(item.Name)).Count();
            if (setting == 0)
            {
                await _settingRepository.InsertAsync(new FireSetting()
                {
                    Name = "ResidualCurrent",
                    MinValue = -10000,
                    MaxValue = 500
                });
            }
            // 消防水池水压KPa
            setting = setlist.Where(item => "PoolWaterPressure".Equals(item.Name)).Count();
            if (setting == 0)
            {
                await _settingRepository.InsertAsync(new FireSetting()
                {
                    Name = "PoolWaterPressure",
                    MinValue = 70,
                    MaxValue = 10000
                });
            }
            // 消防水池液位高度M
            setting = setlist.Where(item => "PoolWaterHeight".Equals(item.Name)).Count();
            if (setting == 0)
            {
                await _settingRepository.InsertAsync(new FireSetting()
                {
                    Name = "PoolWaterHeight",
                    MinValue = 0.5,
                    MaxValue = 5.8
                });
            }
            // 市政消火栓水压kPa
            setting = setlist.Where(item => "HydrantPressure".Equals(item.Name)).Count();
            if (setting == 0)
            {
                await _settingRepository.InsertAsync(new FireSetting()
                {
                    Name = "HydrantPressure",
                    MinValue = 100,
                    MaxValue = 10000
                });
            }
        }
    }
}
