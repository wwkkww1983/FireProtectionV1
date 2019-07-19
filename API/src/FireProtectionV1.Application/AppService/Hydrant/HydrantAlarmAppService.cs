using FireProtectionV1.HydrantCore.Dto;
using FireProtectionV1.HydrantCore.Manager;
using FireProtectionV1.User.Dto;
using FireProtectionV1.User.Manager;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    public class HydrantAlarmAppService : AppServiceBase
    {
        IHydrantSystemManager _hydrantSystemManager;

        public HydrantAlarmAppService(IHydrantSystemManager hydrantSystemManager) 
        {
            _hydrantSystemManager = hydrantSystemManager;
        }
        /// <summary>
        /// 获取消火栓列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        public async Task<List<GetUserHydrantListOutput>> GetUserHydrant(GetUserHydrantInput input)
        {
            return await _hydrantSystemManager.GetUserHydrant(input);
        }

        /// <summary>
        /// 获取消火栓报警列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        public async Task<GetHydrantAlarmPagingOutput> GetHydrantAlarmlistForMobile(GetHydrantAlarmListInput input)
        {
            return await _hydrantSystemManager.GetHydrantAlarmlistForMobile(input);
        }

        /// <summary>
        /// 获取警情处理详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        public async Task<GetHydrantAlarmInfoOutput> GetHydrantAlarmInfo(GetHydrantAlarmInfoInput input)
        {
            return await _hydrantSystemManager.GetHydrantAlarmInfo(input);
        }

        /// <summary>
        /// 警情处理
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        public async Task<SuccessOutput> UpdtateHydrantAlarm([FromForm]UpdateHydrantAlarmInput input)
        {
            return await _hydrantSystemManager.UpdtateHydrantAlarm(input);
        }
    }

}
