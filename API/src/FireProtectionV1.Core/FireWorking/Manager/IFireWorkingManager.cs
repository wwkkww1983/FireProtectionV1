using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using FireProtectionV1.Enterprise.Dto;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.FireWorking.Manager
{
    public interface IFireWorkingManager : IDomainService
    {
        /// <summary>
        /// 防火单位消防数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetFireUnitAlarmOutput> GetFireUnitAlarm(FireUnitIdInput input);
        /// <summary>
        /// 安全用电最近30天报警记录查询
        /// </summary>
        /// <param name="input"></param>
        /// <param name="detectorTypeId">探测器类型</param>
        /// <returns></returns>
        Task<PagedResultDto<AlarmRecord>> GetFireUnit30DayAlarmEle(GetPageByFireUnitIdInput input, int detectorTypeId);

        /// <summary>
        /// 火警预警最近30天报警记录查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<AlarmRecord>> GetFireUnit30DayAlarmFire(GetPageByFireUnitIdInput input);
        /// <summary>
        /// 安全用电高频报警部件查询
        /// </summary>
        /// <param name="input"></param>
        /// <param name="onlyElecOrTemp"></param>
        /// <returns></returns>
        Task<PagedResultDto<HighFreqAlarmDetector>> GetFireUnitHighFreqAlarmEle(GetPageByFireUnitIdInput input,string onlyElecOrTemp=null);

        /// <summary>
        /// 火警预警高频报警部件查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<HighFreqAlarmDetector>> GetFireUnitHighFreqAlarmFire(GetPageByFireUnitIdInput input);
        /// <summary>
        /// （所有防火单位）设备设施故障监控
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<FireUnitFaultOuput>> GetFireUnitFaultList(GetPagedFireUnitListInput input);

        /// <summary>
        /// 设备设施故障待处理故障查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<PendingFaultOutput>> GetFireUnitPendingFault(GetPageByFireUnitIdInput input);
        /// <summary>
        /// 安全用电数据分析
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetAreasAlarmElectricOutput> GetAreasAlarmElectric(GetAreasAlarmElectricInput input);
        /// <summary>
        /// 火警预警数据分析
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetAreasAlarmFireOutput> GetAreasAlarmFire(GetAreasAlarmFireInput input);
        /// <summary>
        /// （所有防火单位）火灾报警监控列表
        /// </summary>
        /// <returns></returns>
        Task<PagedResultDto<GetAreas30DayFireAlarmOutput>> GetAreas30DayFireAlarmList(GetPagedFireUnitListFilterTypeInput input);
        /// <summary>
        /// （所有防火单位）安全用电监控列表（电缆温度）
        /// </summary>
        /// <returns></returns>
        Task<PagedResultDto<GetAreas30DayFireAlarmOutput>> GetAreas30DayTempAlarmList(GetPagedFireUnitListFilterTypeInput input);
        /// <summary>
        /// （所有防火单位）安全用电监控列表（剩余电流）
        /// </summary>
        /// <returns></returns>
        Task<PagedResultDto<GetAreas30DayFireAlarmOutput>> GetAreas30DayElecAlarmList(GetPagedFireUnitListFilterTypeInput input);
        /// <summary>
        /// （所有防火单位）值班巡查监控（值班记录）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetFireUnitDutyListOutput> GetFireUnitDutyList(GetPagedFireUnitListInput input);
        ///// <summary>
        ///// （所有防火单位）超过7天没有巡查记录的单位列表
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //Task<GetFireUnitPatrolListOutput> GetNoPatrol7DayFireUnitList(PagedRequestByUserIdDto input);
        /// <summary>
        /// （所有防火单位）超过1天没有值班记录的单位列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetFireUnitDutyListOutput> GetNoDuty1DayFireUnitList(PagedRequestByUserIdDto input);
   }
}
