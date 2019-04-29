using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Account.Dto
{
    public class UserLoginInput
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required]
        public string Account { get; set; }
        /// <summary>
        /// 密码（默认6个0）
        /// </summary>
        [Required]
        public string Password { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [Required]
        public string VerifyCode { get; set; }

    }
}
