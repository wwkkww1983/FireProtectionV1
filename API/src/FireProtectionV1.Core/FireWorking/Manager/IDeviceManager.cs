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
        Task<SuccessOutput> AddFireAlarmDevice(FireAlarmDeviceDto input, string origin);
        /// <summary>
        /// 新增探测器部件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddDetector(AddDetectorInput input);
        /// <summary>
        /// 新增网关设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddGateway(AddGatewayInput input);

        Task<Detector> GetDetectorAsync(int id);
        Task<PagedResultDto<FireElectricDeviceItemDto>> GetFireElectricDeviceList(int fireUnitId, string state, PagedResultRequestDto dto);
        Detector GetDetector(string identify,string origin);
        Gateway GetGateway(string identify, string origin);
        Task<PagedResultDto<FireAlarmDeviceItemDto>> GetFireAlarmDeviceList(int fireUnitId, PagedResultRequestDto dto);
        DetectorType GetDetectorType(byte GBtype);
        Task<DetectorType> GetDetectorTypeAsync(int id);
        IQueryable<Detector> GetDetectorElectricAll();
        IQueryable<DetectorType> GetDetectorTypeAll();
        Task<AddDataOutput> AddOnlineDetector(AddOnlineDetectorInput input);

        /// <summary>
        /// 查询指定防火单位和防火系统的所有探测器
        /// </summary>
        /// <param name="fireunitid"></param>
        /// <param name="fireSysType"></param>
        /// <returns></returns>
        IQueryable<Detector> GetDetectorAll(int fireunitid, FireSysType fireSysType);
        Task AddOnlineGateway(AddOnlineGatewayInput input);
        Task<SuccessOutput> AddFireElectricDevice(FireElectricDeviceDto input, string tianshuju);
        Task<GetFireAlarmDeviceDto> GetFireAlarmDevice(int deviceId);

        /// <summary>
        /// 获取防火单位的终端状态
        /// </summary>
        /// <param name="fireUnitId"></param>
        /// <returns></returns>
        Task<PagedResultDeviceDto<EndDeviceStateOutput>> GetFireUnitEndDeviceState(int fireUnitId, int option, PagedResultRequestDto dto);
        Task<SuccessOutput> UpdateFireAlarmDevice(UpdateFireAlarmDeviceDto input);
        Task<SuccessOutput> AddFireOrtherDevice(FireOrtherDeviceDto input);
        Task<AddDataOutput> AddRecordAnalog(AddDataElecInput input);
        Task<SuccessOutput> UpdateFireElectricDevice(UpdateFireElectricDeviceDto input);
        Task<SuccessOutput> DeleteDetector(int detectorId);
        Task<DetectorDto> GetDeviceDetector(int detectorId);
        Task<SuccessOutput> UpdateFireAlarmDetector(UpdateDetectorDto detectorDto);
        Task<SuccessOutput> UpdateFireOrtherDevice(UpdateFireOrtherDeviceDto input);
        Task<AddDeviceDetectorOutput> AddFireAlarmDetector(AddDetectorDto detectorDto, string tianshuju);
        Task<GetRecordElectricOutput> GetRecordElectric(GetRecordElectricInput input);
        Task<PagedResultDto<FaultDetectorOutput>> GetFireAlarmFaultDetectorList(int DeviceId, PagedResultRequestDto dto);

        /// <summary>
        /// 获得防火单位终端历史记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<RecordAnalogOutput> GetRecordAnalog(GetRecordDetectorInput input);
        Task<RecordUnAnalogOutput> GetRecordUnAnalog(GetRecordDetectorInput input);
        Task<PagedResultDto<GatewayStatusOutput>> GetFireUnitGatewaysStatus(int fireSysType, int fireUnitId, PagedResultRequestDto dto);
        Task<PagedResultDto<GetFireAlarm30DayDto>> GetFireAlarm30DayList(int deviceId, PagedResultRequestDto dto);
        Task<SuccessOutput> DeleteFireAlarmDevice(int deviceId);
        Task<PagedResultDto<DetectorDto>> GetDeviceDetectorList(string deviceSn, PagedResultRequestDto dto);
        Task<PagedResultDto<GetFireAlarmHighDto>> GetFireAlarmHighList(int deviceId, PagedResultRequestDto dto);
        Task<SuccessOutput> DeleteFireElectricDevice(int deviceId);
        Task<SuccessOutput> DeleteFireOrtherDevice(int deviceId);
        Task<PagedResultDto<FireOrtherDeviceItemDto>> GetFireOrtherDeviceList(int FireUnitId, string ExpireType, string FireUnitArchitectureName, PagedResultRequestDto dto);
        Task<List<string>> GetFireAlarmDeviceTypes();
        Task<List<string>> GetFireElectricDeviceTypes();
        Task<List<string>> GetFireAlarmDeviceProtocols();
        Task<SerialPortParamDto> GetSerialPortParam(int deviceId);
        Task<UpdateFireElectricDeviceDto> GetFireElectricDevice(int deviceId);
        Task<GetFireOrtherDeviceOutput> GetFireOrtherDevice(int deviceid);
        Task<GetFireElectricDeviceStateOutput> GetFireElectricDeviceState(int fireUnitId);
        Task<GetFireOrtherDeviceExpireOutput> GetFireOrtherDeviceExpire(int fireUnitId);

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
        /// 获取单个设备信息
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        Task<UpdateFireWaterDeviceInput> GetById(int deviceId);
        /// <summary>
        /// 获取消防管网设备列表
        /// </summary>
        /// <param name="fireUnitId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagedResultDto<FireWaterDevice>> GetFireWaterDeviceList(int fireUnitId);
        /// <summary>
        /// 获取消防管网联网网关设备型号列表
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetFireWaterDeviceTypes();
    }
}
