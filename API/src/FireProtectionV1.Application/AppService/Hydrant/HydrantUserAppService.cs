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
    public class HydrantUserAppService : HttpContextAppService
    {
        IHydrantUserManager _hydrantUserManager;

        public HydrantUserAppService(IHydrantUserManager hydrantUserManager, IHttpContextAccessor httpContext) : base(httpContext)
        {
            _hydrantUserManager = hydrantUserManager;
        }
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> UserRegist(GetHydrantUserRegistInput input)
        {
            return await _hydrantUserManager.UserRegist(input);
        }
        /// <summary>
        /// 用户登录(移动端)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<PutHydrantUserLoginOutput> UserLoginForMobile(LoginInput input)
        {
            return await Login(input);
        }

        private async Task<PutHydrantUserLoginOutput> Login(LoginInput input)
        {
            //用户名密码验证
            var output = await _hydrantUserManager.UserLogin(input);
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
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> ChangePassword(DeptChangePassword input)
        {
            return await _hydrantUserManager.ChangePassword(input);
        }

        /// <summary>
        /// 获取已有管辖区
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<GetHyrantAreaOutput>> GetUserArea(GetUserAreaInput input)
        {
            return await _hydrantUserManager.GetUserArea(input);
        }

        /// <summary>
        /// 获取未拥有管辖区
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<GetHyrantAreaOutput>> GetArea(GetUserAreaInput input)
        {
            return await _hydrantUserManager.GetArea(input);
        }

        /// <summary>
        /// 修改用户管辖区
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> PutUserArea(PutUserAreaInput input)
        {
            return await _hydrantUserManager.PutUserArea(input);
        }
    }

}
