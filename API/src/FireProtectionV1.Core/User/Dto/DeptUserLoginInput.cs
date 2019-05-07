using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.User.Dto
{
    public class DeptUserLoginInput
    {
        /// <summary>
        /// 账号(测试:test)
        /// </summary>
        [Required]
        public string Account { get; set; }
        /// <summary>
        /// 密码(测试:123)
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
