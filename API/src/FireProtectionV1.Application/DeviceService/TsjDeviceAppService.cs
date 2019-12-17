using FireProtectionV1.AppService;
using FireProtectionV1.FireWorking.Manager;
using FireProtectionV1.TsjDevice.Dto;
using FireProtectionV1.TsjDevice.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.DeviceService
{
    public class TsjDeviceAppService: AppServiceBase
    {
        ITsjDeviceManager _tsjDeviceManager;
        IAlarmManager _alarmManager;
        IFaultManager _faultManager;
        public TsjDeviceAppService(
            IFaultManager faultManager,
            ITsjDeviceManager deviceManager, 
            IAlarmManager alarmManager)
        {
            _faultManager = faultManager;
            _tsjDeviceManager = deviceManager;
            _alarmManager = alarmManager;
        }
        /// <summary>
        /// 新增故障
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> NewFault(NewFaultInput input)
        {
            return await _tsjDeviceManager.NewFault(input);
        }
        /// <summary>
        /// 新增火警联网火警
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> NewAlarm(NewAlarmInput input)
        {
            return await _tsjDeviceManager.NewAlarm(input);
        }
        /// <summary>
        /// 新增模拟量值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> NewMonitor(NewMonitorInput input)
        {
            return await _tsjDeviceManager.NewMonitor(input);
        }
        /// <summary>
        /// 新增电气火灾报警(超限)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> NewOverflow(NewOverflowInput input)
        {
            return await _tsjDeviceManager.NewOverflow(input);
        }
        /// <summary>
        /// 获取固件更新列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<UpdateFirmwareOutput>> GetUpdateFirmwareList()
        {
            return await _tsjDeviceManager.GetUpdateFirmwareList();
        }
    }
}
