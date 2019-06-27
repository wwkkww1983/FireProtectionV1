using FireProtectionV1.User.Dto;
using FireProtectionV1.User.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    public class FireUnitUserAppService : AppServiceBase
    {
        IFireUnitUserManager _fireUnitAccountManager;

        public FireUnitUserAppService(IFireUnitUserManager fireUnitAccountManager)
        {
            _fireUnitAccountManager = fireUnitAccountManager;
        }

        /// <summary>
        /// 添加账号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> Add(FireUnitUserInput input)
        {
            return await _fireUnitAccountManager.Add(input);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<FireUnitUserLoginOutput> UserLogin(LoginInput input)
        {
            return await _fireUnitAccountManager.UserLogin(input);
        }

       
    }
}
