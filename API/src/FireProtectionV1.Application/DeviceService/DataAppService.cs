using FireProtectionV1.AppService;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Manager;
using FireProtectionV1.SettingCore.Manager;
using FireProtectionV1.SettingCore.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.DeviceService
{
    public class DataAppService : AppServiceBase
    {
        IPatrolManager _patrolManager;
        IDutyManager _dutyManager;
        IFireSettingManager _fireSettingManager;
        IDeviceManager _deviceManager;
        IAlarmManager _alarmManager;
        IFaultManager _faultManager;
        public DataAppService(
            IPatrolManager patrolManager,
            IDutyManager dutyManager,
            IFaultManager faultManager,
            IFireSettingManager fireSettingManager,
            IDeviceManager deviceManager,IAlarmManager alarmManager)
        {
            _patrolManager = patrolManager;
            _dutyManager = dutyManager;
            _faultManager = faultManager;
            _fireSettingManager = fireSettingManager;
            _deviceManager = deviceManager;
            _alarmManager = alarmManager;
        }
        //public async Task<AddDataOutput> AddOnlineDetector(AddOnlineDetectorInput input)
        //{
        //    return await _deviceManager.AddOnlineDetector(input);
        //}
        /// <summary>
        /// 新增 网关在线离线事件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //public async Task AddOnlineGateway(AddOnlineGatewayInput input)
        //{
        //    await _deviceManager.AddOnlineGateway(input);
        //}

        /// <summary>
        /// 新增火灾监控设备报警
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddAlarmFire(AddAlarmFireInput input)
        {
            //Console.WriteLine($"{DateTime.Now} 收到报警 AddAlarmFire 部件类型:{input.DetectorGBType.ToString()} 部件地址：{input.Identify} 网关地址：{input.GatewayIdentify}");
            await _alarmManager.AddAlarmFire(input);
        }
        /// <summary>
        /// 添加故障
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddNewDetectorFault(AddNewDetectorFaultInput input)
        {
            await _faultManager.AddNewDetectorFault(input);
        }

    }
}
