using FireProtectionV1.Account.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Account.Dto
{
    public class FireUnitAccountInput
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required]
        public string Account { get; set; }
        /// <summary>
        /// 密码（默认6个0）
        /// </summary>
        public string Password { get; set; } = "000000";
        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        [Required]
        public FireUnitRole Role { get; set; }
    }
}
