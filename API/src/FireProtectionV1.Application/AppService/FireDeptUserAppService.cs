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
        #region PC端接口
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<DeptUserLoginOutput> UserLogin(PcDeptUserLoginInput input)
        {
            if (string.IsNullOrEmpty(input.VerifyCode))
            {
                return new DeptUserLoginOutput() { Success = false, FailCause = "请输入验证码" };
            }
            byte[] verifyValue ;
            _httpContext.HttpContext.Session.TryGetValue("VerifyCode",out verifyValue);
            if(null==verifyValue)
                return new DeptUserLoginOutput() { Success = false, FailCause = "验证码错误" };
            string verifyCode = Encoding.Default.GetString(verifyValue);
            if (!verifyCode.Equals(input.VerifyCode))
                return new DeptUserLoginOutput() { Success = false, FailCause = "验证码错误" };
            return await _fireDeptUserManager.UserLogin(input); 
        }
        #endregion PC端接口

        #region 移动端接口
        /// <summary>
        /// 用户登录(移动端)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<DeptUserLoginOutput> UserLoginForMobile(DeptUserLoginInput input)
        {
            var output=await _fireDeptUserManager.UserLogin(input);
            if (!output.Success)
                return output;
            var ci = new ClaimsIdentity(); 
            ci.AddClaim(new Claim(ClaimTypes.NameIdentifier, input.Account));
            //ci.AddClaim(new Claim("Account", input.Account));
            //ci.AddClaim(new Claim("Password", input.Password));
            var accessToken = CreateAccessToken(CreateJwtClaims(ci));
            //output.Token = "test";
            output.Token = accessToken;
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
        //public async Task<UserRegistOutput> UserRegist(UserRegistInput input)
        //{
        //    return await _fireDeptUserManager.UserRegist(input);
        //}
        //public async Task Test()
        //{
        //    await Task.Delay(1);
        //}
    }
}