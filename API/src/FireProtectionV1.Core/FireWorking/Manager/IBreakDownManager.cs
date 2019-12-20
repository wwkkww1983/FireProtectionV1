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
    public interface IBreakDownManager : IDomainService
    {
        /// <summary>
        /// 获取设施故障列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<GetBreakDownOutput>> GetBreakDownlist(GetBreakDownInput input, PagedResultRequestDto dto);
        /// <summary>
        /// 获取设施故障详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetBreakDownInfoOutput> GetBreakDownInfo(GetBreakDownInfoInput input);
        /// <summary>
        /// 更新设施故障详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> UpdateBreakDownInfo(UpdateBreakDownInfoInput input);
        /// <summary>
        /// 获取设施故障处理情况
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetBreakDownTotalOutput> GetBreakDownTotal(GetBreakDownTotalInput input);
    }
}
