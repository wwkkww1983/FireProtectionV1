using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using FireProtectionV1.Common.Enum;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Dto.FireDevice;
using FireProtectionV1.FireWorking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.FireWorking.Manager
{
    public interface IDeviceManager : IDomainService
    {
        /// <summary>
        /// 新增火警联网设施
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddFireAlarmDevice(FireAlarmDeviceDto input);
        /// <summary>
        /// 新增探测器部件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //Task AddDetector(AddDetectorInput input);
        /// <summary>
        /// 新增网关设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //Task AddGateway(AddGatewayInput input);
        /// <summary>
        /// 获取指定防火单位的电气火灾设施列表
        /// </summary>
        /// <param name="fireUnitId">防火单位ID</param>
        /// <param name="state">设备状态：null/""、在线、离线、良好、隐患、超限</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagedResultDto<FireElectricDeviceItemDto>> GetFireElectricDeviceList(int fireUnitId, string state, PagedResultRequestDto dto);
        //Detector GetDetector(string identify,string origin);
        //Gateway GetGateway(string identify, string origin);
        /// <summary>
        /// 获取指定防火单位ID的火警联网设施列表
        /// </summary>
        /// <param name="fireUnitId">防火单位ID</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagedResultDto<FireAlarmDeviceItemDto>> GetFireAlarmDeviceList(int fireUnitId, PagedResultRequestDto dto);
        DetectorType GetDetectorType(byte GBtype);
        //IQueryable<Detector> GetDetectorElectricAll();
        //Task<AddDataOutput> AddOnlineDetector(AddOnlineDetectorInput input);
        /// <summary>
        /// 查询指定防火单位和防火系统的所有探测器
        /// </summary>
        /// <param name="fireunitid"></param>
        /// <param name="fireSysType"></param>
        /// <returns></returns>
        //IQueryable<Detector> GetDetectorAll(int fireunitid, FireSysType fireSysType);
        //Task AddOnlineGateway(AddOnlineGatewayInput input);
        /// <summary>
        /// 添加电气火灾设施
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddFireElectricDevice(FireElectricDeviceDto input);
        /// <summary>
        /// 在线/离线事件接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateDeviceState(UpdateDeviceStateInput input);
        /// <summary>
        /// 根据Id获取火警联网设施详情
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        Task<GetFireAlarmDeviceDto> GetFireAlarmDevice(int deviceId);
        /// <summary>
        /// 获取火警联网设施部件类型数组
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetFireAlarmDetectorTypes();
        /// <summary>
        /// 获取防火单位的终端状态
        /// </summary>
        /// <param name="fireUnitId"></param>
        /// <returns></returns>
        //Task<PagedResultDeviceDto<EndDeviceStateOutput>> GetFireUnitEndDeviceState(int fireUnitId, int option, PagedResultRequestDto dto);
        /// <summary>
        /// 修改火警联网设施
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateFireAlarmDevice(UpdateFireAlarmDeviceDto input);
        /// <summary>
        /// 新增其他消防设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> AddFireOrtherDevice(FireOrtherDeviceDto input);
        /// <summary>
        /// 添加电气火灾监测数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddElecRecord(AddDataElecInput input);
        /// <summary>
        /// 修改电气火灾设施
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateFireElectricDevice(UpdateFireElectricDeviceDto input);
        /// <summary>
        /// 删除火警联网部件
        /// </summary>
        /// <param name="detectorId"></param>
        /// <returns></returns>
        Task DeleteDetector(int detectorId);
        /// <summary>
        /// 根据Id获取单个火警联网部件详情
        /// </summary>
        /// <param name="detectorId"></param>
        /// <returns></returns>
        Task<FireAlarmDetector> GetDetectorById(int detectorId);
        /// <summary>
        /// 修改火警联网设施的联网部件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateFireAlarmDetector(UpdateDetectorDto detectorDto);
        /// <summary>
        /// 修改其它消防设施
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> UpdateFireOrtherDevice(UpdateFireOrtherDeviceDto input);
        Task<AddDeviceDetectorOutput> AddFireAlarmDetector(AddDetectorDto detectorDto, string tianshuju);
        /// <summary>
        /// 获取电气火灾监测单个项目的模拟量趋势
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetRecordElectricOutput> GetRecordElectric(GetRecordElectricInput input);
        /// <summary>
        /// 获取某个火警联网设施下的故障部件列表
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagedResultDto<FaultDetectorOutput>> GetFireAlarmFaultDetectorList(int deviceId, PagedResultRequestDto dto);

        /// <summary>
        /// 获得防火单位终端历史记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //Task<RecordAnalogOutput> GetRecordAnalog(GetRecordDetectorInput input);
        //Task<RecordUnAnalogOutput> GetRecordUnAnalog(GetRecordDetectorInput input);
        //Task<PagedResultDto<GatewayStatusOutput>> GetFireUnitGatewaysStatus(int fireSysType, int fireUnitId, PagedResultRequestDto dto);
        /// <summary>
        /// 获取火警联网设施最近30天的火警列表数据
        /// </summary>
        /// <param name="fireAlarmDeviceId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagedResultDto<GetFireAlarm30DayDto>> GetFireAlarm30DayList(int fireAlarmDeviceId, PagedResultRequestDto dto);
        /// <summary>
        /// 删除火警联网设施
        /// </summary>
        /// <param name="DeviceId"></param>
        /// <returns></returns>
        Task DeleteFireAlarmDevice(int deviceId);
        /// <summary>
        /// 根据火警联网设备Id获取其下的部件列表
        /// </summary>
        /// <param name="fireAlarmDeviceId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagedResultDto<FireAlarmDetectorDto>> GetDeviceDetectorList(int fireAlarmDeviceId, PagedResultRequestDto dto);
        /// <summary>
        /// 获取指定火警联网设施ID的高频报警部件列表
        /// </summary>
        /// <param name="fireAlarmDeviceId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagedResultDto<GetFireAlarmHighDto>> GetFireAlarmHighList(int fireAlarmDeviceId, PagedResultRequestDto dto);
        /// <summary>
        /// 删除电气火灾设施
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        Task DeleteFireElectricDevice(int deviceId);
        /// <summary>
        /// 删除其它消防设施
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        Task DeleteFireOrtherDevice(int deviceId);
        /// <summary>
        /// 获取其它消防设施列表
        /// </summary>
        /// <param name="fireUnitId"></param>
        /// <param name="ExpireType"></param>
        /// <param name="FireUnitArchitectureName"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagedResultDto<FireOrtherDeviceItemDto>> GetFireOrtherDeviceList(int FireUnitId, string ExpireType, string FireUnitArchitectureName, PagedResultRequestDto dto);
        /// <summary>
        /// 获取火警联网设备型号数组
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetFireAlarmDeviceModels();
        /// <summary>
        /// 获取电气火灾设备型号数组
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetFireElectricDeviceModels();
        /// <summary>
        /// 获取火灾报警控制器协议数组
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetFireAlarmDeviceProtocols();
        /// <summary>
        /// 获取火警设备串口参数
        /// </summary>
        /// <param name="deviceId">火警联网设备ID</param>
        /// <returns></returns>
        Task<SerialPortParamDto> GetSerialPortParam(int deviceId);
        /// <summary>
        /// 根据Id获取电气火灾设施
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        Task<GetFireElectricDeviceOutput> GetFireElectricDevice(int deviceId);
        /// <summary>
        /// 通过Id获取其它消防设施
        /// </summary>
        /// <param name="deviceid"></param>
        /// <returns></returns>
        Task<GetFireOrtherDeviceOutput> GetFireOrtherDevice(int deviceid);
        Task<GetFireElectricDeviceStateOutput> GetFireElectricDeviceState(int fireUnitId);
        /// <summary>
        /// 获取防火单位的其它消防设施的即将过期数量和已过期数量
        /// </summary>
        /// <param name="fireUnitId"></param>
        /// <returns></returns>
        Task<GetFireOrtherDeviceExpireOutput> GetFireOrtherDeviceExpire(int fireUnitId);
        /// <summary>
        /// 导入火警联网网关对应消防主机的联网部件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> ImportFireAlarmDetector(FireAlarmDetectorImportDto input);
        /// <summary>
        /// 导入其它消防设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> ImportOrtherDevice(FireOtherDeviceImportDto input);
        /// <summary>
        /// 添加消防管网设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddFireWaterDevice(AddFireWaterDeviceInput input);
        /// <summary>
        /// 修改消防管网设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateFireWaterDevice(UpdateFireWaterDeviceInput input);
        /// <summary>
        /// 删除消防管网设备
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        Task DeleteFireWaterDevice(int deviceId);
        /// <summary>
        /// 获取单个消防管网设备信息
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        Task<UpdateFireWaterDeviceInput> GetFireWaterDeviceById(int deviceId);
        /// <summary>
        /// 获取消防管网设备列表
        /// </summary>
        /// <param name="fireUnitId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagedResultDto<GetFireWaterDeviceListOutput>> GetFireWaterDeviceList(int fireUnitId, PagedResultRequestDto dto);
        /// <summary>
        /// 获取消防管网联网网关设备型号列表
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetFireWaterDeviceModels();
        /// <summary>
        /// 添加消防水管网监测数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddFireWaterRecord(AddFireWaterRecordInput input);
        /// <summary>
        /// 用于数据大屏：获取各类消防物联网设施的各种状态及数量
        /// </summary>
        /// <param name="fireUnitId"></param>
        /// <returns></returns>
        Task<List<GetDeviceStatusForDataScreenOutput>> GetDeviceStatusForDataScreen(int fireUnitId);
        /// <summary>
        /// 获取固件更新列表
        /// </summary>
        /// <returns></returns>
        Task<List<GetFirmwareUpdateListOutput>> GetFirmwareUpdateList();
    }
}
