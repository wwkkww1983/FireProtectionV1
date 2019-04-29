using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Account.Dto
{
    public class UserRegistInput
    {        /// <summary>
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
        /// 姓名
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 监管部门ID
        /// </summary>
        [Required]
        public int FireDeptId { get; set; }

    }
}
