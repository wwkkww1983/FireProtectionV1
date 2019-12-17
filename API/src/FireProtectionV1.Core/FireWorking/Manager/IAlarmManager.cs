using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.FireWorking.Manager
{
    public interface IAlarmManager : IDomainService
    {
        /// <summary>
        /// 新增安全用电报警
        /// </summary>
        /// <param name="input"></param>
        /// <param name="alarmLimit"></param>
        /// <returns></returns>
        Task<AddDataOutput> AddAlarmElec(AddDataElecInput input, string alarmLimit);
        /// <summary>
        /// 新增火灾监控设备报警
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AddDataOutput> AddAlarmFire(AddAlarmFireInput input);
        /// <summary>
        /// 查询指定时间以后的最新电气火灾报警数据
        /// </summary>
        /// <param name="startTime"></param>
        /// <returns></returns>
        IQueryable<AlarmToElectric> GetNewElecAlarm(DateTime startTime);
        /// <summary>
        /// 查询给定checkId的警情详细信息
        /// </summary>
        /// <param name="checkId"></param>
        /// <returns></returns>
        Task<AlarmCheckDetailOutput> GetAlarmCheckDetail(int checkId);
        void RepairData();
        /// <summary>
        /// 查询防火单位警情核警数据
        /// </summary>
        /// <param name="fireunitid"></param>
        /// <returns></returns>
        Task<PagedResultDto<AlarmCheckOutput>> GetAlarmChecks(int fireunitid, List<string> filter, PagedResultRequestDto dto);
        /// <summary>
        /// 保存核警信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task CheckAlarm(AlarmCheckDetailDto dto);
        IQueryable<AlarmToFire> GetAlarms(IQueryable<Detector> detectors, DateTime start, DateTime end);
        /// <summary>
        /// 获取数据大屏的火警联网实时达
        /// </summary>
        /// <param name="fireUnitId">防火单位Id</param>
        /// <param name="dataNum">需要的数据条数，不传的话默认为5条</param>
        /// <returns></returns>
        Task<List<FireAlarmForDataScreenOutput>> GetFireAlarmForDataScreen(int fireUnitId, int dataNum);
        /// <summary>
        /// 根据fireAlarmId获取单条火警数据详情
        /// </summary>
        /// <param name="fireAlarmId"></param>
        /// <returns></returns>
        Task<FireAlarmDetailOutput> GetFireAlarmById(int fireAlarmId);
        /// <summary>
        /// 获取火警联网数据列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagedResultDto<FireAlarmListOutput>> GetFireAlarmList(FireAlarmListInput input, PagedResultRequestDto dto);
    }
}
