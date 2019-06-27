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
        Task<AlarmCheckInput> GetAlarmCheckDetail(int checkId);

        /// <summary>
        /// 查询防火单位警情核警数据
        /// </summary>
        /// <param name="fireunitid"></param>
        /// <returns></returns>
        Task<List<AlarmCheckOutput>> GetAlarmChecks(int fireunitid);
    }
}
