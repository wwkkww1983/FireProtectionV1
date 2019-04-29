using System;
using FireProtectionV1.Account.Manager;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FireProtectionV1.Account.Dto;
using FireProtectionV1.Extensions;
using Microsoft.AspNetCore.Http;

namespace FireProtectionV1.Account
{
    public class FireDeptUserAppService:AppServiceBase
    {
        IFireDeptUserManager _fireDeptUserManager;
        IHttpContextAccessor _httpContext;

        public FireDeptUserAppService(IFireDeptUserManager fireDeptUserManager, IHttpContextAccessor httpContext)
        {
            _fireDeptUserManager = fireDeptUserManager;
            _httpContext= httpContext;
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<UserLoginOutput> UserLogin(UserLoginInput input)
        {
            if (string.IsNullOrEmpty(input.Password))
            {
                return new UserLoginOutput() { Success = false, Result = "请输入验证码" };
            }
            byte[] verifyValue ;
            _httpContext.HttpContext.Session.TryGetValue("VerifyCode",out verifyValue);
            if(null==verifyValue)
                return new UserLoginOutput() { Success = false, Result = "验证码错误" };
            string verifyCode = Encoding.Default.GetString(verifyValue);
            if (!verifyCode.Equals(input.VerifyCode))
                return new UserLoginOutput() { Success = false, Result = "验证码错误" };
            return await _fireDeptUserManager.UserLogin(input); 
        }
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<UserRegistOutput> UserRegist(UserRegistInput input)
        {
            return await _fireDeptUserManager.UserRegist(input);
        }
        //public async Task Test()
        //{
        //    await Task.Delay(1);
        //}
     }
}
