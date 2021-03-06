﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using FireProtectionV1.User.Manager;
using FireProtectionV1.User.Dto;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using FireProtectionV1.Common.Helper;

namespace FireProtectionV1.AppService
{
    /// <summary>
    /// 消防部门用户接口
    /// </summary>
    public class FireDeptUserAppService: HttpContextAppService
    {
        IFireDeptUserManager _fireDeptUserManager;

        public FireDeptUserAppService(IFireDeptUserManager fireDeptUserManager, IHttpContextAccessor httpContext)
            :base(httpContext)
        {
            _fireDeptUserManager = fireDeptUserManager;
        }
        /// <summary>
        /// 注销用户
        /// </summary>
        /// <returns></returns>
        public async Task<SuccessOutput> UserLogout()
        {
            return await Logout();
        }
        #region PC端接口
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
        /// 用户登录(PC端)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<DeptUserLoginOutput> UserLogin(PcDeptUserLoginInput input)
        {
            //判断验证码
            if (string.IsNullOrEmpty(input.VerifyCode))
            {
                return new DeptUserLoginOutput() { Success = false, FailCause = "请输入验证码" };
            }
            byte[] verifyValue;
            _httpContext.HttpContext.Session.TryGetValue("VerifyCode", out verifyValue);
            if (null == verifyValue)
                return new DeptUserLoginOutput() { Success = false, FailCause = "验证码错误" };
            string verifyCode = Encoding.Default.GetString(verifyValue);
            if (!verifyCode.Equals(input.VerifyCode))
                return new DeptUserLoginOutput() { Success = false, FailCause = "验证码错误" };
            //登录判断
            return await Login(input);
        }
        #endregion PC端接口

        #region 移动端接口
        /// <summary>
        /// 用户登录(移动端)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<DeptUserLoginOutput> UserLoginForMobile(LoginInput input)
        {
            return await Login(input);
        }
        private async Task<DeptUserLoginOutput> Login(LoginInput input)
        {
            //用户名密码验证
            var output = await _fireDeptUserManager.UserLogin(input);
            if (!output.Success)
                return output;
            if (!await Authentication(input.Account, output.Name, input.Password, input.IsPersistent))
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
        #endregion
        private static List<Claim> CreateJwtClaims(ClaimsIdentity identity)
        {
            var claims = identity.Claims.ToList();
            var nameIdClaim = claims.First(c => c.Type == ClaimTypes.NameIdentifier);

            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, nameIdClaim.Value),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            });

            return claims;
        }

        private string CreateAccessToken(IEnumerable<Claim> claims, TimeSpan? expiration = null)
        {
            var now = DateTime.UtcNow;

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: "FireProtectionV1",
                audience: "FireProtectionV1",
                claims: claims,
                notBefore: now,
                expires: now.Add(expiration ?? new TimeSpan(24,0,0)),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("FireProtectionV1_C421AAEE0D114E9C")), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //public string UserRegist(UserRegistInput input)
        //{
        //    string md5 = MD5Encrypt.Encrypt(input.Password + input.UserName, 16);
        //    return md5;
        //}


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> ChangePassword(ChangeUserPassword input)
        {
            return await _fireDeptUserManager.ChangePassword(input);
        }
    }
}
