using FireProtectionV1.User.Dto;
using FireProtectionV1.User.Manager;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    public class FireUnitUserAppService : HttpContextAppService
    {
        IFireUnitUserManager _fireUnitAccountManager;

        public FireUnitUserAppService(IFireUnitUserManager fireUnitAccountManager, IHttpContextAccessor httpContext) : base(httpContext)
        {
            _fireUnitAccountManager = fireUnitAccountManager;
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task GetVerifyCode()
        {
            await GetBaseVerifyCode();
        }
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> UserRegist(UserRegistInput input)
        {
            return await _fireUnitAccountManager.UserRegist(input);
        }
        /// <summary>
        /// 添加账号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //public async Task<int> Add(FireUnitUserInput input)
        //{
        //    return await _fireUnitAccountManager.Add(input);
        //}

        /// <summary>
        /// 用户登录(移动端)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<FireUnitUserLoginOutput> UserLoginForMobile(LoginInput input)
        {
            return await Login(input);
        }

        /// <summary>
        /// 用户登录(PC端)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<FireUnitUserLoginOutput> UserLogin(PcDeptUserLoginInput input)
        {
            //判断验证码
            if (string.IsNullOrEmpty(input.VerifyCode))
            {
                return new FireUnitUserLoginOutput() { Success = false, FailCause = "请输入验证码" };
            }
            byte[] verifyValue;
            _httpContext.HttpContext.Session.TryGetValue("VerifyCode", out verifyValue);
            if (null == verifyValue)
                return new FireUnitUserLoginOutput() { Success = false, FailCause = "验证码错误" };
            string verifyCode = Encoding.Default.GetString(verifyValue);
            if (!verifyCode.Equals(input.VerifyCode))
                return new FireUnitUserLoginOutput() { Success = false, FailCause = "验证码错误" };
            //登录判断
            var output= await Login(input);
            if (output.Rolelist == null || !output.Rolelist.Contains(User.Model.FireUnitRole.FireUnitManager))
            {
                output.Success = false;
                output.FailCause = "只有管理员才能登录";
            }
            return output;
        }
        private async Task<FireUnitUserLoginOutput> Login(LoginInput input)
        {
            //用户名密码验证
            var output = await _fireUnitAccountManager.UserLogin(input);
            if (!output.Success)
                return output;
            if (!await Authentication(input.Account, output.Name, input.Password, input.IsPersistent))
            {
                output.Success = false;
                output.FailCause = "认证失败";
            }
            return output;
        }
        /// <summary>
        /// 注销用户
        /// </summary>
        /// <returns></returns>
        public async Task<SuccessOutput> UserLogout()
        {
            return await Logout();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> ChangePassword(ChangeUserPassword input)
        {
            return await _fireUnitAccountManager.ChangePassword(input);
        }
    }
}
