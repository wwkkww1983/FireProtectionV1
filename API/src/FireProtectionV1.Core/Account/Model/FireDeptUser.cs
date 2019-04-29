using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Account.Model
{
    /// <summary>
    /// 监管部门用户
    /// </summary>
    public class FireDeptUser: EntityBase
    {
        /// <summary>
        /// 账号（手机号）
        /// </summary>
        [Required]
        [Phone]
        [MaxLength(20)]
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [MaxLength(StringType.Normal)]
        public string Password { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        [MaxLength(StringType.Normal)]
        public string Name { get; set; }
        /// <summary>
        /// 监管部门ID
        /// </summary>
        [Required]
        public int FireDeptId { get; set; }

    }
}
