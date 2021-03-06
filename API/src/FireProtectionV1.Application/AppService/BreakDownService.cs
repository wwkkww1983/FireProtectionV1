﻿using Abp.Application.Services.Dto;
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
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetBreakDownOutput>> GetBreakDownlist(GetBreakDownInput input, PagedResultRequestDto dto)
        {
            return await _breakDownManager.GetBreakDownlist(input, dto);
        }
        /// <summary>
        /// 获取设施故障详情
        /// </summary>
        /// <param name="breakDownId"></param>
        /// <returns></returns>
        public async Task<GetBreakDownInfoOutput> GetBreakDownInfo(int breakDownId)
        {
            return await _breakDownManager.GetBreakDownInfo(breakDownId);
        }
        /// <summary>
        /// 更新设施故障详情（平台端或手机端进行设施故障处理，点击提交时）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateBreakDownInfo(UpdateBreakDownInfoInput input)
        {
            await _breakDownManager.UpdateBreakDownInfo(input);
        }
        /// <summary>
        /// 获取设施故障统计情况（用于首页数据大屏）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetBreakDownTotalOutput> GetBreakDownTotal(GetBreakDownTotalInput input)
        {
            return await _breakDownManager.GetBreakDownTotal(input);
        }
    }
}
