using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using FireProtectionV1.Account.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.Account.Manager
{
    public interface IFireUnitUserManager : IDomainService
    {
        /// <summary>
        /// 添加防火单位账号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<int> Add(FireUnitAccountInput input);
    }
}
