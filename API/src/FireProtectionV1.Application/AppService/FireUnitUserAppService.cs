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
            return await Login(input);
        }
        private async Task<FireUnitUserLoginOutput> Login(LoginInput input)
        {
            //用户名密码验证
            var output = await _fireUnitAccountManager.UserLogin(input);
            if (!output.Success)
                return output;
            //用户认证
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Sid, input.Account));
            identity.AddClaim(new Claim(ClaimTypes.Name, output.Name));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, input.Password));
            //identity.AddClaim(new Claim(ClaimTypes.Role, user.Role));
            var principal = new ClaimsPrincipal(identity);
            var authProp = new AuthenticationProperties();
            if (input.IsPersistent)
                authProp.IsPersistent = true;
            else
            {
                authProp.IsPersistent = false;
                double expMin = 20;//绝对到期时间30分钟
                var authorizeExpires = Configuration.ConfigHelper.Configuration["AuthorizeExpires"];
                if (authorizeExpires != null)
                    expMin = double.Parse(authorizeExpires);
                authProp.ExpiresUtc = DateTime.UtcNow.AddMinutes(expMin);
            }
            await _httpContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProp);
            //验证是否认证成功
            if (!principal.Identity.IsAuthenticated)
            {
                output.Success = false;
                output.FailCause = "认证失败";
            }

            //var ci = new ClaimsIdentity(); 
            //ci.AddClaim(new Claim(ClaimTypes.NameIdentifier, input.Account));
            ////ci.AddClaim(new Claim("Account", input.Account));
            ////ci.AddClaim(new Claim("Password", input.Password));
            //var accessToken = CreateAccessToken(CreateJwtClaims(ci));
            ////output.Token = "test";
            //output.Token = accessToken;
            return output;
        }


        /// <summary>
        /// 注销用户
        /// </summary>
        /// <returns></returns>
        public async Task<SuccessOutput> UserLogout()
        {
            var output = new SuccessOutput();
            if (!_httpContext.HttpContext.User.Identity.IsAuthenticated)
            {
                output.Success = false;
                output.FailCause = "未认证";
                return output;
            }
            await _httpContext.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            output.Success = true;
            return output;
        }
    }
}
