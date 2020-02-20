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
        IFaultManager _faultManager;
        public FireDeviceAppService(IDeviceManager detectorManager, IFaultManager faultManager)
        {
            _deviceManager = detectorManager;
            _faultManager = faultManager;
        }
        /// <summary>
        /// 添加火警联网部件故障数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddDetectorFault(AddNewDetectorFaultInput input)
        {
            await _faultManager.AddDetectorFault(input);
        }
        /// <summary>
        /// 添加电气火灾监测数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddElecRecord(AddDataElecInput input)
        {
            await _deviceManager.AddElecRecord(input);
        }
        /// <summary>
        /// 添加消防水管网监测数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddFireWaterRecord(AddFireWaterRecordInput input)
        {
            await _deviceManager.AddFireWaterRecord(input);
        }
        /// <summary>
        /// 在线/离线事件接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateDeviceState(UpdateDeviceStateInput input)
        {
            await _deviceManager.UpdateDeviceState(input);
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
        /// 获取辖区内各防火单位的火警联网设施列表
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <param name="fireUnitName"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<FireAlarmDevice_DeptDto>> GetFireAlarmDeviceList_Dept(int fireDeptId, string fireUnitName, PagedResultRequestDto dto)
        {
            return await _deviceManager.GetFireAlarmDeviceList_Dept(fireDeptId, fireUnitName, dto);
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
        /// 获取楼层的火警点位图设置
        /// </summary>
        /// <param name="floorId"></param>
        /// <returns></returns>
        public async Task<GetBitMapSetOutput> GetBitMapSet(int floorId)
        {
            return await _deviceManager.GetBitMapSet(floorId);
        }
        /// <summary>
        /// 修改部件在点位图上的坐标
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateDetectorCoordinate(UpdateDetectorCoordinateInput input)
        {
            await _deviceManager.UpdateDetectorCoordinate(input);
        }
        /// <summary>
        /// 获取某条火警对应的部件的点位图数据
        /// </summary>
        /// <param name="fireAlarmId"></param>
        /// <returns></returns>
        public async Task<GetDetectorBitMapOutput> GetDetectorBitMap(int fireAlarmId)
        {
            return await _deviceManager.GetDetectorBitMap(fireAlarmId);
        }
        /// <summary>
        /// 获取电气火灾监测单个项目的模拟量趋势
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetRecordElectricOutput> GetRecordElectric(GetRecordElectricInput input)
        {
            return await _deviceManager.GetRecordElectric(input);
        }
        /// <summary>
        /// 用于数据大屏：获取各类消防物联网设施的各种状态及数量
        /// </summary>
        /// <param name="fireUnitId"></param>
        /// <returns></returns>
        public async Task<List<GetDeviceStatusForDataScreenOutput>> GetDeviceStatusForDataScreen(int fireUnitId)
        {
            return await _deviceManager.GetDeviceStatusForDataScreen(fireUnitId);
        }
        /// <summary>
        /// 获取火警联网设施部件类型数组
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetFireAlarmDetectorTypes()
        {
            return await _deviceManager.GetFireAlarmDetectorTypes();
            //return new List<string>()
            //{
            //    "火灾报警控制器","感烟式火灾探测器","感温式火灾探测器",
            //    "感光式火灾探测器","可燃气体火灾探测器","复合式火灾探测器","手动火灾报警按钮","其它"
            //};
        }
        /// <summary>
        /// 新增探测器部件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //public async Task AddDetector(AddDetectorInput input)
        //{
        //    try
        //    {
        //        await _deviceManager.AddDetector(input);
        //    }catch(Exception e)
        //    {

        //    }
        //}

        /// <summary>
        /// 获取防火单位的终端状态
        /// </summary>
        /// <param name="FireUnitId">防火单位Id</param>
        /// <param name="Option">筛选选项值</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        //public async Task<PagedResultDeviceDto<EndDeviceStateOutput>> GetFireUnitEndDeviceState([Required]int FireUnitId,int Option,PagedResultRequestDto dto)
        //{
        //    return await _deviceManager.GetFireUnitEndDeviceState(FireUnitId, Option,dto);
        //}
        /// <summary>
        /// 获得防火单位模拟量终端历史记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //public async Task<RecordAnalogOutput> GetRecordAnalog(GetRecordDetectorInput input)
        //{
        //    return await _deviceManager.GetRecordAnalog(input);
        //}
        /// <summary>
        /// 获得防火单位非模拟量终端历史记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //public async Task<RecordUnAnalogOutput> GetRecordUnAnalog(GetRecordDetectorInput input)
        //{
        //    return await _deviceManager.GetRecordUnAnalog(new GetRecordDetectorInput()
        //    {
        //        DetectorId=input.DetectorId,
        //        FireUnitId=input.FireUnitId,
        //        End=DateTime.Now.Date.AddSeconds(-1),
        //        Start=DateTime.Now.Date.AddDays(-7)
        //    });
        //}
        /// <summary>
        /// 查询防火单位网关状态列表
        /// </summary>
        /// <param name="FireSysType">1:安全用电，2:火灾报警</param>
        /// <param name="FireUnitId">防火单位Id</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        //public async Task<PagedResultDto<GatewayStatusOutput>> GetFireUnitGatewaysStatus([Required]int FireSysType, [Required]int FireUnitId, PagedResultRequestDto dto)
        //{
        //    return await _deviceManager.GetFireUnitGatewaysStatus(FireSysType,FireUnitId, dto);
        //}
        /// <summary>
        /// 获取指定设备ID的故障部件列表
        /// </summary>
        /// <param name="fireAlarmDeviceId">设备ID</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<FaultDetectorOutput>> GetFireAlarmFaultDetectorList(int fireAlarmDeviceId, PagedResultRequestDto dto)
        {
            return await _deviceManager.GetFireAlarmFaultDetectorList(fireAlarmDeviceId, dto);
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
        /// 根据区域Id获取工程手机端的电气火灾设备各种状态的数量
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        public async Task<GetFireElectricDeviceStateOutput> GetEngineerElectricDeviceState(int areaId)
        {
            return await _deviceManager.GetEngineerElectricDeviceState(areaId);
        }
        /// <summary>
        /// 刷新某一电气火灾设备的当前数值
        /// </summary>
        /// <param name="electricDeviceId"></param>
        /// <returns></returns>
        public async Task<GetSingleElectricDeviceDataOutput> GetSingleElectricDeviceData(int electricDeviceId)
        {
            return await _deviceManager.GetSingleElectricDeviceData(electricDeviceId);
        }
        /// <summary>
        /// 发送断电信号
        /// </summary>
        /// <param name="electricDeviceId"></param>
        /// <returns></returns>
        public async Task BreakoffPower(int electricDeviceId)
        {
            await _deviceManager.BreakoffPower(electricDeviceId);
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
        /// 获取辖区内各防火单位的电气火灾设施列表
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <param name="fireUnitName"></param>
        /// <param name="state">设备状态：null/""、在线、离线、良好、隐患、超限</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<FireElectricDevice_DeptDto>> GetFireElectricDeviceList_Dept(int fireDeptId, string fireUnitName, string state, PagedResultRequestDto dto)
        {
            return await _deviceManager.GetFireElectricDeviceList_Dept(fireDeptId, fireUnitName, state, dto);
        }
        /// <summary>
        /// 获取电气火灾设备详情
        /// </summary>
        /// <param name="DeviceId"></param>
        /// <returns></returns>
        public async Task<GetFireElectricDeviceOutput> GetFireElectricDevice(int DeviceId)
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
        /// 获取工程人员端各防火单位的电气火灾设施列表
        /// </summary>
        /// <param name="areaId"></param>
        /// <param name="deviceSn"></param>
        /// <param name="state"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<FireElectricDevice_EngineerDto>> GetFireElectricDeviceList_Engineer(int areaId, string deviceSn, string state, PagedResultRequestDto dto)
        {
            return await _deviceManager.GetFireElectricDeviceList_Engineer(areaId, deviceSn, state, dto);
        }
        /// <summary>
        /// 获取C端电气火灾设施列表
        /// </summary>
        /// <param name="fireUnitId"></param>
        /// <param name="deviceSn"></param>
        /// <param name="state"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<FireElectricDevice_EngineerDto>> GetFireElectricDeviceList_Resident(int fireUnitId, string deviceSn, string state, PagedResultRequestDto dto)
        {
            return await _deviceManager.GetFireElectricDeviceList_Resident(fireUnitId, deviceSn, state, dto);
        }
        /// <summary>
        /// 根据Id获取电气火灾设施参数详情
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public async Task<GetFireElectricDeviceParaOutput> GetFireElectricDevicePara(int deviceId)
        {
            return await _deviceManager.GetFireElectricDevicePara(deviceId);
        }
        /// <summary>
        /// 修改电气火灾设施参数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateFireElectricDevicePara(UpdateFireElectricDeviceParaInput input)
        {
            await _deviceManager.UpdateFireElectricDevicePara(input);
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
        public async Task<PagedResultDto<GetFireWaterDeviceListOutput>> GetFireWaterDeviceList(int fireUnitId, PagedResultRequestDto dto)
        {
            return await _deviceManager.GetFireWaterDeviceList(fireUnitId, dto);
        }
        /// <summary>
        /// 获取辖区各防火单位的消防管网设施列表
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <param name="fireUnitName"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetFireWaterDeviceList_DeptOutput>> GetFireWaterDeviceList_Dept(int fireDeptId, string fireUnitName, PagedResultRequestDto dto)
        {
            return await _deviceManager.GetFireWaterDeviceList_Dept(fireDeptId, fireUnitName, dto);
        }
        /// <summary>
        /// 获取消防管网联网网关设备型号列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetFireWaterDeviceModels()
        {
            return await _deviceManager.GetFireWaterDeviceModels();
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
