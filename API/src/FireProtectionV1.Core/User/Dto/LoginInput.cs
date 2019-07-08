using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.User.Dto
{
    public class LoginInput
    {
        /// <summary>
        /// 账号(测试:123)
        /// </summary>
        [Required]
        public string Account { get; set; }
        /// <summary>
        /// 密码(测试:666666)
        /// </summary>
        [Required]
        public string Password { get; set; }
        /// <summary>
        /// 是否会话不限时
        /// </summary>
        public bool IsPersistent { get; set; }
    }
}
