using Abp.Application.Services.Dto;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Manager;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    public class BreakDownService : AppServiceBase
    {
        IBreakDownManager _breakDownManager;
        
        public BreakDownService(IBreakDownManager breakDownManager)
        {
            _breakDownManager = breakDownManager;
        }
        /// <summary>
        /// 获取设施故障列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetBreakDownPagingOutput> GetBreakDownlist(GetBreakDownInput input)
        {
            return await _breakDownManager.GetBreakDownlist(input);
        }
        /// <summary>
        /// 获取设施故障详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetBreakDownInfoOutput> GetBreakDownInfo(GetBreakDownInfoInput input)
        {
            return await _breakDownManager.GetBreakDownInfo(input);
        }
        /// <summary>
        /// 更新设施故障详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> UpdateBreakDownInfo(UpdateBreakDownInfoInput input)
        {
            return await _breakDownManager.UpdateBreakDownInfo(input);
        }
        /// <summary>
        /// 获取设施故障处理情况
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetBreakDownTotalOutput> GetBreakDownTotal(GetBreakDownTotalInput input)
        {
            return await _breakDownManager.GetBreakDownTotal(input);
        }
    }
}
