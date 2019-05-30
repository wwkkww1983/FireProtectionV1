using FireProtectionV1.AppService;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.DeviceService
{
    public class DeviceAppService: AppServiceBase
    {
        IDeviceManager _detectorManager;
        IAlarmManager _alarmManager;
        public DeviceAppService(IDeviceManager detectorManager,IAlarmManager alarmManager)
        {
            _detectorManager = detectorManager;
            _alarmManager = alarmManager;
        }
        /// <summary>
        /// 新增探测器部件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddDetector(AddDetectorInput input)
        {
            await _detectorManager.AddDetector(input);
        }
        /// <summary>
        /// 新增网关设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddGateway(AddGatewayInput input)
        {
            await _detectorManager.AddGateway(input);
        }
    }
}
