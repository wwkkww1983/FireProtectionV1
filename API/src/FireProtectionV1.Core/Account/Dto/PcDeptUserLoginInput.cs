using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Account.Dto
{
    public class PcDeptUserLoginInput:DeptUserLoginInput
    {
        /// <summary>
        /// 验证码
        /// </summary>
        [Required]
        public string VerifyCode { get; set; }

    }
}
