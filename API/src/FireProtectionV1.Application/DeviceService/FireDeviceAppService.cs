using Abp.Application.Services.Dto;
using FireProtectionV1.AppService;
using FireProtectionV1.Dto;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Dto.FireDevice;
using FireProtectionV1.FireWorking.Manager;
using FireProtectionV1.FireWorking.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.DeviceService
{
    public class FireDeviceAppService : AppServiceBase
    {
        IDeviceManager _deviceManager;
        IAlarmManager _alarmManager;
        public FireDeviceAppService(IDeviceManager detectorManager, IAlarmManager alarmManager)
        {
            _deviceManager = detectorManager;
            _alarmManager = alarmManager;
        }
        /// <summary>
        /// 获取指定防火单位ID的火警联网设施列表
        /// </summary>
        /// <param name="fireUnitId">防火单位ID</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<FireAlarmDeviceItemDto>> GetFireAlarmDeviceList(int fireUnitId, PagedResultRequestDto dto)
        {
            return await _deviceManager.GetFireAlarmDeviceList(fireUnitId, dto);
        }
        /// <summary>
        /// 获取火警联网设备详情
        /// </summary>
        /// <param name="DeviceId"></param>
        /// <returns></returns>
        public async Task<GetFireAlarmDeviceDto> GetFireAlarmDevice(int DeviceId)
        {
            return await _deviceManager.GetFireAlarmDevice(DeviceId);
        }
        /// <summary>
        /// 新增火警联网设施
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddFireAlarmDevice(FireAlarmDeviceDto input)
        {
            await _deviceManager.AddFireAlarmDevice(input);
        }
        /// <summary>
        /// 删除火警联网设备
        /// </summary>
        /// <param name="deviceId">火警联网设备ID</param>
        /// <returns></returns>
        public async Task DeleteFireAlarmDevice(int deviceId)
        {
            await _deviceManager.DeleteFireAlarmDevice(deviceId);
        }
        /// <summary>
        /// 修改火警联网设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateFireAlarmDevice(UpdateFireAlarmDeviceDto input)
        {
            await _deviceManager.UpdateFireAlarmDevice(input);
        }
        /// <summary>
        /// 获取火警联网设施部件类型数组
        /// </summary>
        /// <returns></returns>
        public List<string> GetFireAlarmDetectorTypes()
        {
            return new List<string>()
            {
                "火灾报警控制器","感烟式火灾探测器","感温式火灾探测器",
                "感光式火灾探测器","可燃气体火灾探测器","复合式火灾探测器","手动火灾报警按钮","其它"
            };
        }
        /// <summary>
        /// 获取指定设备ID的故障部件列表
        /// </summary>
        /// <param name="DeviceId">设备ID</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<FaultDetectorOutput>> GetFireAlarmFaultDetectorList(int DeviceId, PagedResultRequestDto dto)
        {
            return await _deviceManager.GetFireAlarmFaultDetectorList(DeviceId, dto);
        }
        /// <summary>
        /// 获取火警联网设施最近30天的火警列表数据
        /// </summary>
        /// <param name="fireAlarmDeviceId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetFireAlarm30DayDto>> GetFireAlarm30DayList(int fireAlarmDeviceId, PagedResultRequestDto dto)
        {
            return await _deviceManager.GetFireAlarm30DayList(fireAlarmDeviceId, dto);
        }
        /// <summary>
        /// 获取指定火警联网设施ID的高频报警部件列表
        /// </summary>
        /// <param name="fireAlarmDeviceId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetFireAlarmHighDto>> GetFireAlarmHighList(int fireAlarmDeviceId, PagedResultRequestDto dto)
        {
            return await _deviceManager.GetFireAlarmHighList(fireAlarmDeviceId, dto);
        }
        /// <summary>
        /// 根据火警联网设备Id获取其下的部件列表
        /// </summary>
        /// <param name="fireAlarmDeviceId">设备SN</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<FireAlarmDetectorDto>> GetDeviceDetectorList(int fireAlarmDeviceId, PagedResultRequestDto dto)
        {
            return await _deviceManager.GetDeviceDetectorList(fireAlarmDeviceId, dto);
        }
        /// <summary>
        /// 根据Id获取单个火警联网部件详情
        /// </summary>
        /// <param name="detectorId"></param>
        /// <returns></returns>
        public async Task<FireAlarmDetector> GetDetectorById(int detectorId)
        {
            return await _deviceManager.GetDetectorById(detectorId);
        }
        /// <summary>
        /// 添加火警联网部件
        /// </summary>
        /// <param name="detectorDto"></param>
        /// <returns></returns>
        public async Task<AddDeviceDetectorOutput> AddFireAlarmDetector(AddDetectorDto detectorDto)
        {
            return await _deviceManager.AddFireAlarmDetector(detectorDto, Origin.Tianshuju);
        }
        /// <summary>
        /// 导入火警联网网关对应消防主机的联网部件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> ImportFireAlarmDetector([FromForm]FireAlarmDetectorImportDto input)
        {
            return await _deviceManager.ImportFireAlarmDetector(input);
        }
        /// <summary>
        /// 删除火警联网部件
        /// </summary>
        /// <param name="detectorId"></param>
        /// <returns></returns>
        public async Task DeleteDetector(int detectorId)
        {
            await _deviceManager.DeleteDetector(detectorId);
        }
        /// <summary>
        /// 修改火警联网设施的联网部件
        /// </summary>
        /// <param name="detectorDto"></param>
        /// <returns></returns>
        public async Task UpdateFireAlarmDetector(UpdateDetectorDto detectorDto)
        {
            await _deviceManager.UpdateFireAlarmDetector(detectorDto);
        }
        /// <summary>
        /// 获取电气火灾设备各状态数量
        /// </summary>
        /// <param name="FireUnitId"></param>
        /// <returns></returns>
        public async Task<GetFireElectricDeviceStateOutput> GetFireElectricDeviceState(int FireUnitId)
        {
            return await _deviceManager.GetFireElectricDeviceState(FireUnitId);
        }
        /// <summary>
        /// 获取指定防火单位的电气火灾设施列表
        /// </summary>
        /// <param name="fireUnitId">防火单位ID</param>
        /// <param name="state">设备状态：null/""、在线、离线、良好、隐患、超限</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<FireElectricDeviceItemDto>> GetFireElectricDeviceList(int fireUnitId, string state, PagedResultRequestDto dto)
        {
            return await _deviceManager.GetFireElectricDeviceList(fireUnitId, state, dto);
        }
        /// <summary>
        /// 获取电气火灾设备详情
        /// </summary>
        /// <param name="DeviceId"></param>
        /// <returns></returns>
        public async Task<FireElectricDevice> GetFireElectricDevice(int DeviceId)
        {
            return await _deviceManager.GetFireElectricDevice(DeviceId);
        }
        /// <summary>
        /// 新增电气火灾设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddFireElectricDevice(FireElectricDeviceDto input)
        {
            await _deviceManager.AddFireElectricDevice(input);
        }
        /// <summary>
        /// 删除电气火灾设备
        /// </summary>
        /// <param name="DeviceId">电气火灾设备ID</param>
        /// <returns></returns>
        public async Task DeleteFireElectricDevice(int DeviceId)
        {
            await _deviceManager.DeleteFireElectricDevice(DeviceId);
        }
        /// <summary>
        /// 修改电气火灾设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateFireElectricDevice(UpdateFireElectricDeviceDto input)
        {
            await _deviceManager.UpdateFireElectricDevice(input);
        }
        /// <summary>
        /// 获取指定其他消防设备ID的详情
        /// </summary>
        /// <param name="Deviceid">其他消防设备ID</param>
        /// <returns></returns>
        public async Task<GetFireOrtherDeviceOutput> GetFireOrtherDevice(int Deviceid)
        {
            return await _deviceManager.GetFireOrtherDevice(Deviceid);
        }
        /// <summary>
        /// 获取其他消防设施过期/即将过期数量
        /// </summary>
        /// <param name="FireUnitId"></param>
        /// <returns></returns>
        public async Task<GetFireOrtherDeviceExpireOutput> GetFireOrtherDeviceExpire(int FireUnitId)
        {
            return await _deviceManager.GetFireOrtherDeviceExpire(FireUnitId);
        }
        /// <summary>
        /// 获取其他消防设备列表
        /// </summary>
        /// <param name="FireUnitId">防火单位ID</param>
        /// <param name="ExpireType">选项：即将过期、已过期</param>
        /// <param name="FireUnitArchitectureName">建筑名称</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<FireOrtherDeviceItemDto>> GetFireOrtherDeviceList(int FireUnitId, string ExpireType, string FireUnitArchitectureName, PagedResultRequestDto dto)
        {
            return await _deviceManager.GetFireOrtherDeviceList(FireUnitId, ExpireType, FireUnitArchitectureName, dto);
        }
        /// <summary>
        /// 新增其他消防设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> AddFireOrtherDevice(FireOrtherDeviceDto input)
        {
            return await _deviceManager.AddFireOrtherDevice(input);
        }
        /// <summary>
        /// 导入其它消防设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> ImportOrtherDevice([FromForm]FireOtherDeviceImportDto input)
        {
            return await _deviceManager.ImportOrtherDevice(input);
        }
        /// <summary>
        /// 修改其他消防设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> UpdateFireOrtherDevice(UpdateFireOrtherDeviceDto input)
        {
            return await _deviceManager.UpdateFireOrtherDevice(input);
        }
        /// <summary>
        /// 删除其他消防设备
        /// </summary>
        /// <param name="deviceId">其他消防设备ID</param>
        /// <returns></returns>
        public async Task DeleteFireOrtherDevice(int deviceId)
        {
            await _deviceManager.DeleteFireOrtherDevice(deviceId);
        }
        /// <summary>
        /// 获取火警联网设备型号数组
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetFireAlarmDeviceModels()
        {
            return await _deviceManager.GetFireAlarmDeviceModels();
        }
        /// <summary>
        /// 获取电气火灾设备型号数组
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetFireElectricDeviceModels()
        {
            return await _deviceManager.GetFireElectricDeviceModels();
        }
        /// <summary>
        /// 获取火警联网设备协议数组
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetFireAlarmDeviceProtocols()
        {
            return await _deviceManager.GetFireAlarmDeviceProtocols();
        }
        /// <summary>
        /// 获取火警设备串口参数
        /// </summary>
        /// <param name="deviceId">火警联网设备ID</param>
        /// <returns></returns>
        public async Task<SerialPortParamDto> GetSerialPortParam(int deviceId)
        {
            return await _deviceManager.GetSerialPortParam(deviceId);
        }
        /// <summary>
        /// 添加消防管网设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddFireWaterDevice(AddFireWaterDeviceInput input)
        {
            await _deviceManager.AddFireWaterDevice(input);
        }
        /// <summary>
        /// 修改消防管网设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateFireWaterDevice(UpdateFireWaterDeviceInput input)
        {
            await _deviceManager.UpdateFireWaterDevice(input);
        }
        /// <summary>
        /// 删除消防管网设备
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public async Task DeleteFireWaterDevice(int deviceId)
        {
            await _deviceManager.DeleteFireWaterDevice(deviceId);
        }
        /// <summary>
        /// 获取单个消防管网设备信息
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public async Task<UpdateFireWaterDeviceInput> GetFireWaterDeviceById(int deviceId)
        {
            return await _deviceManager.GetFireWaterDeviceById(deviceId);
        }
        /// <summary>
        /// 获取消防管网设备列表
        /// </summary>
        /// <param name="fireUnitId"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<FireWaterDevice>> GetFireWaterDeviceList(int fireUnitId, PagedResultRequestDto dto)
        {
            return await _deviceManager.GetFireWaterDeviceList(fireUnitId, dto);
        }
        /// <summary>
        /// 获取消防管网联网网关设备型号列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetFireWaterDeviceTypes()
        {
            return await _deviceManager.GetFireWaterDeviceTypes();
        }
        /// <summary>
        /// 获取固件更新列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<GetFirmwareUpdateListOutput>> GetFirmwareUpdateList()
        {
            return await _deviceManager.GetFirmwareUpdateList();
        }
    }
}
