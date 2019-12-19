using FireProtectionV1.Common.Helper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.DrawingCore.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace FireProtectionV1.AppService
{
    public abstract class HttpContextAppService : AppServiceBase
    {
        protected readonly IHttpContextAccessor _httpContext;
        public HttpContextAppService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        protected async Task GetBaseVerifyCode()
        {
            await Task.Run(() =>
            {
                string code = VerifyCodeHelper.GetSingleObj().CreateVerifyCode(VerifyCodeHelper.VerifyCodeType.NumberVerifyCode);
                _httpContext.HttpContext.Session.Set("VerifyCode", Encoding.Default.GetBytes(code));
                var bitmap = VerifyCodeHelper.GetSingleObj().CreateBitmapByImgVerifyCode(code, 100, 40);
                using (MemoryStream stream = new MemoryStream())
                {
                    bitmap.Save(stream, ImageFormat.Gif);
                    HttpResponse Response = _httpContext.HttpContext.Response;
                    Response.ContentType = "image/gif";
                    Response.ContentLength = stream.Length;
                   //Response.Headers.Add("Content-Disposition", string.Format("attachment;filename=VerifyCode.gif"));
                   Response.Body.Write(stream.ToArray());
                    Response.Body.Flush();

                }
            });
        }
        /// <summary>
        /// 注销用户
        /// </summary>
        /// <returns></returns>
        protected async Task<SuccessOutput> Logout()
        {
            var output = new SuccessOutput();
            if (!_httpContext.HttpContext.User.Identity.IsAuthenticated)
            {
                output.Success = false;
                output.FailCause = "未认证";
                return output;
            }
            await _httpContext.HttpContext.SignOutAsync(Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme);
            output.Success = true;
            return output;
        }
        protected async Task<bool> Authentication(string account,string name,string password,bool isPersistent)
        {
            //用户认证
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Sid, account));
            identity.AddClaim(new Claim(ClaimTypes.Name, name));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, password));
            //identity.AddClaim(new Claim(ClaimTypes.Role, user.Role));
            var principal = new ClaimsPrincipal(identity);
            var authProp = new AuthenticationProperties();
            if (isPersistent)
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
            return principal.Identity.IsAuthenticated;
        }

    }
}
