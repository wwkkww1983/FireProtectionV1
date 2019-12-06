using Abp.Application.Services.Dto;
using FireProtectionV1.AppService;
using FireProtectionV1.Dto;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Dto.FireDevice;
using FireProtectionV1.FireWorking.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using TsjWebApi;

namespace FireProtectionV1.DeviceService
{
    public class FireDeviceAppService : AppServiceBase
    {
        IDeviceManager _deviceManager;
        IAlarmManager _alarmManager;
        public FireDeviceAppService(IDeviceManager detectorManager,IAlarmManager alarmManager)
        {
            _deviceManager = detectorManager;
            _alarmManager = alarmManager;
        }
        /// <summary>
        /// 获取指定防火单位ID的火警联网设施列表
        /// </summary>
        /// <param name="FireUnitId">防火单位ID</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<FireAlarmDeviceItemDto>> GetFireAlarmDeviceList(int FireUnitId, PagedResultRequestDto dto)
        {
            return await _deviceManager.GetFireAlarmDeviceList(FireUnitId, dto);
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
        /// 新增火警联网设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> AddFireAlarmDevice(FireAlarmDeviceDto input)
        {
            return await _deviceManager.AddFireAlarmDevice(input, Origin.Tianshuju);
        }
        /// <summary>
        /// 删除火警联网设备
        /// </summary>
        /// <param name="DeviceId">火警联网设备ID</param>
        /// <returns></returns>
        public async Task<SuccessOutput> DeleteFireAlarmDevice(int DeviceId)
        {
            return await _deviceManager.DeleteFireAlarmDevice(DeviceId);
        }
        /// <summary>
        /// 修改火警联网设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> UpdateFireAlarmDevice(UpdateFireAlarmDeviceDto input)
        {
            return await _deviceManager.UpdateFireAlarmDevice(input);
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
            return await _deviceManager.GetFireAlarmFaultDetectorList( DeviceId, dto);
        }
        /// <summary>
        /// 获取指定设备ID的30天报警记录
        /// </summary>
        /// <param name="DeviceId">设备ID</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetFireAlarm30DayDto>> GetFireAlarm30DayList(int DeviceId, PagedResultRequestDto dto)
        {
            return await _deviceManager.GetFireAlarm30DayList(DeviceId, dto);
        }
        /// <summary>
        /// 获取指定设备ID的高频报警部件列表
        /// </summary>
        /// <param name="DeviceId">设备ID</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetFireAlarmHighDto>> GetFireAlarmHighList(int DeviceId, PagedResultRequestDto dto)
        {
            return await _deviceManager.GetFireAlarmHighList(DeviceId, dto);
        }
        /// <summary>
        /// 获取指定设备SN的部件列表
        /// </summary>
        /// <param name="DeviceSn">设备SN</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<DetectorDto>> GetDeviceDetectorList(string DeviceSn, PagedResultRequestDto dto)
        {
            return await _deviceManager.GetDeviceDetectorList(DeviceSn, dto);
        }
        /// <summary>
        /// 获取部件详情
        /// </summary>
        /// <param name="DetectorId"></param>
        /// <returns></returns>
        public async Task<DetectorDto> GetDeviceDetector(int DetectorId)
        {
            return await _deviceManager.GetDeviceDetector(DetectorId);
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
        /// 删除部件
        /// </summary>
        /// <param name="DetectorId"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> DeleteDetector(int DetectorId)
        {
            return await _deviceManager.DeleteDetector(DetectorId);
        }
        /// <summary>
        /// 修改火警联网部件
        /// </summary>
        /// <param name="detectorDto"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> UpdateFireAlarmDetector(UpdateDetectorDto detectorDto)
        {
            return await _deviceManager.UpdateFireAlarmDetector(detectorDto);
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
        /// 获取指定防火单位ID的电气火灾设施列表
        /// </summary>
        /// <param name="FireUnitId">防火单位ID</param>
        /// <param name="State">设备状态：null/""、在线、离线、良好、隐患、超限</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<FireElectricDeviceItemDto>> GetFireElectricDeviceList(int FireUnitId,string State, PagedResultRequestDto dto)
        {
            return await _deviceManager.GetFireElectricDeviceList(FireUnitId,State, dto);
        }
        /// <summary>
        /// 获取电气火灾设备详情
        /// </summary>
        /// <param name="DeviceId"></param>
        /// <returns></returns>
        public async Task<UpdateFireElectricDeviceDto> GetFireElectricDevice(int DeviceId)
        {
            return await _deviceManager.GetFireElectricDevice(DeviceId);
        }
        /// <summary>
        /// 新增电气火灾设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> AddFireElectricDevice(FireElectricDeviceDto input)
        {
            return await _deviceManager.AddFireElectricDevice(input, Origin.Tianshuju);
        }
        /// <summary>
        /// 删除电气火灾设备
        /// </summary>
        /// <param name="DeviceId">电气火灾设备ID</param>
        /// <returns></returns>
        public async Task<SuccessOutput> DeleteFireElectricDevice(int DeviceId)
        {
            return await _deviceManager.DeleteFireElectricDevice(DeviceId);
        }
        /// <summary>
        /// 修改电气火灾设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> UpdateFireElectricDevice(UpdateFireElectricDeviceDto input)
        {
            return await _deviceManager.UpdateFireElectricDevice(input);
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
        /// 获取其他消防设备列表
        /// </summary>
        /// <param name="FireUnitId">防火单位ID</param>
        /// <param name="ExpireType">选项：即将过期、已过期</param>
        /// <param name="FireUnitArchitectureName">建筑名称</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<FireOrtherDeviceItemDto>> GetFireOrtherDeviceList(int FireUnitId, string ExpireType, string FireUnitArchitectureName, PagedResultRequestDto dto)
        {
            return await _deviceManager.GetFireOrtherDeviceList(FireUnitId,ExpireType,FireUnitArchitectureName, dto);
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
        /// <param name="DeviceId">其他消防设备ID</param>
        /// <returns></returns>
        public async Task<SuccessOutput> DeleteFireOrtherDevice(int DeviceId)
        {
            return await _deviceManager.DeleteFireOrtherDevice(DeviceId);
        }
        /// <summary>
        /// 获取火警联网设备型号数组
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetFireAlarmDeviceTypes()
        {
            return await _deviceManager.GetFireAlarmDeviceTypes();
        }
        /// <summary>
        /// 获取电气火灾设备型号数组
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetFireElectricDeviceTypes()
        {
            return await _deviceManager.GetFireElectricDeviceTypes();
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
        /// <param name="DeviceId">火警联网设备ID</param>
        /// <returns></returns>
        public async Task<SerialPortParamDto> GetSerialPortParam(int DeviceId)
        {
            return await _deviceManager. GetSerialPortParam(DeviceId);
        }
    }
}
