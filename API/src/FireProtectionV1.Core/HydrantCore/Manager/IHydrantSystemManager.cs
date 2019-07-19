using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using FireProtectionV1.HydrantCore.Dto;
using FireProtectionV1.HydrantCore.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.HydrantCore.Manager
{
    public interface IHydrantSystemManager : IDomainService
    {
        /// <summary>
        /// 获取消火栓列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        Task< List<GetUserHydrantListOutput>> GetUserHydrant(GetUserHydrantInput input);

        /// <summary>
        /// 获取消火栓报警列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        Task<GetHydrantAlarmPagingOutput> GetHydrantAlarmlistForMobile(GetHydrantAlarmListInput input);


        /// <summary>
        /// 获取警情处理详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        Task<GetHydrantAlarmInfoOutput> GetHydrantAlarmInfo(GetHydrantAlarmInfoInput input);

        /// <summary>
        /// 警情处理
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        Task<SuccessOutput> UpdtateHydrantAlarm(UpdateHydrantAlarmInput input);
    }
}
