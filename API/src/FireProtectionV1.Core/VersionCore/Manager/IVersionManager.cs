using Abp.Domain.Services;
using FireProtectionV1.VersionCore.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.VersionCore.Manager
{
    public interface IVersionManager: IDomainService
    {
        /// <summary>
        /// 添加建议
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<int> Add(AddSuggestInput input);
    }
}
