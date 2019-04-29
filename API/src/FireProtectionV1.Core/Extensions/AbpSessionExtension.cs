using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace FireProtectionV1.Extensions
{
    public static class AbpSessionExtension
    {

        static readonly
            ConditionalWeakTable<IAbpSession, AbpSessionValue>
            SecurityCode = new ConditionalWeakTable<IAbpSession, AbpSessionValue>();

        public static string GetSecurityCode(this IAbpSession session)
        {
            return SecurityCode.GetOrCreateValue(session).SecurityCode;
        }

        public static void SetSecurityCode(this IAbpSession session, string code)
        {
            SecurityCode.GetOrCreateValue(session).SecurityCode = code;
        }

        private class AbpSessionValue
        {
            public string SecurityCode { get; set; }
        }

        //public static void SetSecurityCode(IAbpSession session,string code)
        //{
        //    var claimsPrincipal = DefaultPrincipalAccessor.Instance.Principal;
        //    claimsPrincipal.AddIdentity(new ClaimsIdentity())
        //}

        //public static string GetUserCode(this IAbpSession session)
        //{
        //    return GetClaimValue(ClaimTypes.Email);
        //}

        //private static string GetClaimValue(string claimType)
        //{
        //    var claimsPrincipal = DefaultPrincipalAccessor.Instance.Principal;

        //    var claim = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == claimType);
        //    if (string.IsNullOrEmpty(claim?.Value))
        //        return null;

        //    return claim.Value;
        //}
    }
}
