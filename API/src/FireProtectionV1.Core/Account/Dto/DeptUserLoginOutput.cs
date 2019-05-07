using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Account.Dto
{
    public class DeptUserLoginOutput
    {
        /// <summary>
        /// 登录是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 登录失败原因
        /// </summary>
        public string FailCause { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 身份令牌 
        /// </summary>
        public string Token { get; set; }
    }
}
