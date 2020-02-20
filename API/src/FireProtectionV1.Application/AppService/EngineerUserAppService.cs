using FireProtectionV1.Common.Helper;
using FireProtectionV1.User.Dto;
using FireProtectionV1.User.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    public class EngineerUserAppService : HttpContextAppService
    {
        IEngineerUserManager _manager;
        public EngineerUserAppService(
            IHttpContextAccessor httpContext,
            IEngineerUserManager manager) : base(httpContext)
        {
            _manager = manager;
        }
        /// <summary>
        /// 登录注销
        /// </summary>
        /// <returns></returns>
        public async Task<SuccessOutput> UserLogout()
        {
            return await Logout();
        }
        /// <summary>
        /// 工程人员手机端登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<EngineerUserLoginOutput> LoginForMobile(LoginInput input)
        {
            var output = await _manager.LoginForMobile(input);
            Valid.Exception(!await Authentication(input.Account, output.Name, input.Password, input.IsPersistent), "认证失败");

            return output;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> ChangePassword(ChangeUserPassword input)
        {
            return await _manager.ChangePassword(input);
        }
    }
}
