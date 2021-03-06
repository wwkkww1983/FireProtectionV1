﻿using Abp.Application.Services.Dto;
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

namespace FireProtectionV1.DeviceService
{
    public class DeviceAppService: AppServiceBase
    {
        IDeviceManager _deviceManager;
        IAlarmManager _alarmManager;
        public DeviceAppService(IDeviceManager detectorManager,IAlarmManager alarmManager)
        {
            _deviceManager = detectorManager;
            _alarmManager = alarmManager;
        }

        /// <summary>
        /// 新增探测器部件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddDetector(AddDetectorInput input)
        {
            try
            {
                await _deviceManager.AddDetector(input);
            }catch(Exception e)
            {

            }
        }
        /// <summary>
        /// 新增网关设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddGateway(AddGatewayInput input)
        {
            await _deviceManager.AddGateway(input);
        }
        /// <summary>
        /// 获取终端设备筛选选项
        /// </summary>
        /// <returns></returns>
        public Task<List<EndDeviceOptionDto>> GetEndDeviceOptions()
        {
            var output = new List<EndDeviceOptionDto>();
            output.Add(new EndDeviceOptionDto()
            {
                Value = 0,
                Name = "全部终端"
            });
            output.Add(new EndDeviceOptionDto()
            {
                Value = 1,
                Name = "在线"
            });
            output.Add(new EndDeviceOptionDto()
            {
                Value = -1,
                Name = "离线"
            });
            return Task.FromResult<List<EndDeviceOptionDto>>(output);
        }
        /// <summary>
        /// 获取防火单位的终端状态
        /// </summary>
        /// <param name="FireUnitId">防火单位Id</param>
        /// <param name="Option">筛选选项值</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDeviceDto<EndDeviceStateOutput>> GetFireUnitEndDeviceState([Required]int FireUnitId,int Option,PagedResultRequestDto dto)
        {
            return await _deviceManager.GetFireUnitEndDeviceState(FireUnitId, Option,dto);
        }
        /// <summary>
        /// 获得防火单位模拟量终端历史记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<RecordAnalogOutput> GetRecordAnalog(GetRecordDetectorInput input)
        {
            return await _deviceManager.GetRecordAnalog(input);
        }
        /// <summary>
        /// 获取电气火灾监测项目模拟量趋势
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetRecordElectricOutput> GetRecordElectric(GetRecordElectricInput input)
        {
            return await _deviceManager.GetRecordElectric(input);
        }
        /// <summary>
        /// 获得防火单位非模拟量终端历史记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<RecordUnAnalogOutput> GetRecordUnAnalog(GetRecordDetectorInput input)
        {
            return await _deviceManager.GetRecordUnAnalog(new GetRecordDetectorInput()
            {
                DetectorId=input.DetectorId,
                FireUnitId=input.FireUnitId,
                End=DateTime.Now.Date.AddSeconds(-1),
                Start=DateTime.Now.Date.AddDays(-7)
            });
        }
        /// <summary>
        /// 查询防火单位网关状态列表
        /// </summary>
        /// <param name="FireSysType">1:安全用电，2:火灾报警</param>
        /// <param name="FireUnitId">防火单位Id</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GatewayStatusOutput>> GetFireUnitGatewaysStatus([Required]int FireSysType, [Required]int FireUnitId, PagedResultRequestDto dto)
        {
            return await _deviceManager.GetFireUnitGatewaysStatus(FireSysType,FireUnitId, dto);
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
    }
}
