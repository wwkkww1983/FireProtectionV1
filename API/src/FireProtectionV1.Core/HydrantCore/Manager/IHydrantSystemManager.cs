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
        /// 获取用户消火栓报警列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        Task<GetHydrantAlarmPagingOutput> GetHydrantAlarmlistForMobile(GetHydrantAlarmListInput input);


        /// <summary>
        /// 获取用户警情处理详情
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
        /// <summary>
        /// 全部标为已读
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        Task<SuccessOutput> UpdtateAlarmAlreadyRead(GetUserHydrantInput input);

        /// <summary>
        /// 获取区域消火栓列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        Task<GetAreaHydrantListOutput> GetAreaHydrant(GetAreaHydrantInput input);
        /// <summary>
        /// 获区域取消火栓报警列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        Task<GetAreaHydrantAlarmOutput> GetAreaHydrantAlarmlist(GetAreaHydrantInput input);
        /// <summary>
        /// 获区域管理人列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        Task<GetAreaUserListOutput> GetAreaUserlist(GetAreaHydrantInput input);
        /// <summary>
        /// 获区消火栓设置
        /// </summary>
        /// <returns></returns> 
        Task<GetHydrantSetOutput> GetHyrantSet();
        /// <summary>
        /// 更新消火栓设置
        /// </summary>
        /// <returns></returns> 
        Task<SuccessOutput> UpdateHyrantSet(GetHydrantSetOutput input);
        /// <summary>
        /// 新增报警信息（测试用）
        /// </summary>
        /// <returns></returns> 
        Task<SuccessOutput> AddAlarmInfo(AddAlarm input);
    }
}
