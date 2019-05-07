using FireProtectionV1.Account.Dto;
using FireProtectionV1.Account.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    public class FireUnitUserAppService : AppServiceBase
    {
        IFireUnitAccountManager _fireUnitAccountManager;

        public FireUnitUserAppService(IFireUnitAccountManager fireUnitAccountManager)
        {
            _fireUnitAccountManager = fireUnitAccountManager;
        }

        /// <summary>
        /// 添加账号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> Add(FireUnitAccountInput input)
        {
            return await _fireUnitAccountManager.Add(input);
        }
    }
}
