using FireProtectionV1.User.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.User.Dto
{
    public class PutHydrantUserLoginOutput : SuccessOutput
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 账号（手机号）
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 是否需要引导（true需要引导false不需要）
        /// </summary>
        public bool GuideFlage { get; set; }
    }
}
