using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using FireProtectionV1.Account.Dto;
using FireProtectionV1.Enterprise.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.Enterprise.Manager
{
    public interface IFireUnitInfoManager : IDomainService
    {
        /// <summary>
        /// 添加防火单位（同时会添加防火单位管理员账号）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<int> Add(AddFireUnitInfoInput input);

        /// <summary>
        /// 防火单位分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<GetFireUnitListOutput>> GetList(GetFireUnitListInput input);
    }
}
