using Abp.Application.Services.Dto;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Manager;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    public class AlarmAppService : AppServiceBase
    {
        IAlarmManager _alarmManager;

        public AlarmAppService(IAlarmManager alarmManager)
        {
            _alarmManager = alarmManager;
        }
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
        /// 获取数据大屏的火警联网实时达
        /// </summary>
        /// <param name="fireUnitId">防火单位Id</param>
        /// <param name="dataNum">需要的数据条数，不传的话默认为5条</param>
        /// <returns></returns>
        public async Task<List<FireAlarmForDataScreenOutput>> GetFireAlarmForDataScreen(int fireUnitId, int dataNum = 5)
        {
            return await _alarmManager.GetFireAlarmForDataScreen(fireUnitId, dataNum);
        }
        /// <summary>
        /// 根据fireAlarmId获取单条火警数据详情
        /// </summary>
        /// <param name="fireAlarmId"></param>
        /// <returns></returns>
        public async Task<FireAlarmDetailOutput> GetFireAlarmById(int fireAlarmId)
        {
            return await _alarmManager.GetFireAlarmById(fireAlarmId);
        }
        /// <summary>
        /// 获取火警联网数据列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<FireAlarmListOutput>> GetFireAlarmList(FireAlarmListInput input, PagedResultRequestDto dto)
        {
            return await _alarmManager.GetFireAlarmList(input, dto);
        }
        /// <summary>
        /// 获取电气火灾警情数据列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<ElectricAlarmListOutput>> GetElectricAlarmList(GetElectricAlarmListInput input, PagedResultRequestDto dto)
        {
            return await _alarmManager.GetElectricAlarmList(input, dto);
        }
        /// <summary>
        /// 获取消防管网警情数据列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<WaterAlarmListOutput>> GetWaterAlarmList(GetWaterAlarmListInput input, PagedResultRequestDto dto)
        {
            return await _alarmManager.GetWaterAlarmList(input, dto);
        }
        /// <summary>
        /// 火警联网核警
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CheckFireAlarm([FromForm]AlarmCheckInput input)
        {
            await _alarmManager.CheckFirmAlarm(input);
        }
        /// <summary>
        /// 获取区域内各防火单位火警联网的真实火警报119数据
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        public async Task<List<GetTrueFireAlarmListOutput>> GetAlarmTo119List(int fireDeptId)
        {
            return await _alarmManager.GetAlarmTo119List(fireDeptId);
        }
        /// <summary>
        /// 获取防火单位未读警情类型及数量
        /// </summary>
        /// <param name="fireUnitId"></param>
        /// <returns></returns>
        public async Task<List<GetNoReadAlarmNumOutput>> GetNoReadAlarmNumList(int fireUnitId)
        {
            return await _alarmManager.GetNoReadAlarmNumList(fireUnitId);
        }
    }
}
