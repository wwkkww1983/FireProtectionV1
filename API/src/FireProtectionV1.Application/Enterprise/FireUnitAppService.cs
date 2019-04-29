using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using FireProtectionV1.Account;
using FireProtectionV1.Account.Dto;
using FireProtectionV1.Account.Manager;
using FireProtectionV1.Enterprise.Dto;
using FireProtectionV1.Enterprise.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.Enterprise
{
    /// <summary>
    /// 
    /// </summary>
    public class FireUnitAppService : AppServiceBase
    {
        IFireUnitManager _fireUnitInfoManager;

        public FireUnitAppService(IFireUnitManager fireUnitInfoManager)
        {
            _fireUnitInfoManager = fireUnitInfoManager;
        }

        /// <summary>
        /// 添加防火单位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> Add(AddFireUnitInput input)
        {
            return await _fireUnitInfoManager.Add(input);
        }

        /// <summary>
        /// 防火单位分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetFireUnitListOutput>> GetList(GetFireUnitListInput input)
        {
            return await _fireUnitInfoManager.GetList(input);
        }
    }
}
