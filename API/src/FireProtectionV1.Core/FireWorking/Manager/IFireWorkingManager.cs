using Abp.Application.Services.Dto;
using FireProtectionV1.FireWorking.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.FireWorking.Manager
{
    public interface IFireWorkingManager
    {
        /// <summary>
        /// 防火单位消防数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetFireUnitAlarmOutput> GetFireUnitAlarm(GetByFireUnitIdInput input);
        Task<GetAreasAlarmElectricOutput> GetAreasAlarmElectric(GetAreasAlarmElectricInput input);
        /// <summary>
        /// 安全用电最近30天报警记录查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<AlarmRecord>> GetFireUnit30DayAlarmEle(GetPageByFireUnitIdInput input);

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
        /// <returns></returns>
        Task<GetFireUnitHighFreqAlarmEleOutput> GetFireUnitHighFreqAlarmEle(GetByFireUnitIdInput input);

        /// <summary>
        /// 火警预警高频报警部件查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetFireUnitHighFreqAlarmFireOutput> GetFireUnitHighFreqAlarmFire(GetByFireUnitIdInput input);

        /// <summary>
        /// 设备设施故障待处理故障查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetFireUnitPendingFaultOutput> GetFireUnitPendingFault(GetByFireUnitIdInput input);
        /// <summary>
        /// 火警预警数据分析
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetAreasAlarmFireOutput> GetAreasAlarmFire(GetAreasAlarmFireInput input);
    }
}
