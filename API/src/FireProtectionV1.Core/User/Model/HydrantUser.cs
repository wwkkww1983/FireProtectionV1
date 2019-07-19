using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.User.Model
{
    public class HydrantUser : EntityBase
    {
        /// <summary>
        /// 账号（手机号）
        /// </summary>
        [Required]
        [Phone]
        [MaxLength(StringType.Normal)]
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
        /// 是否引导
        /// </summary>
        [Required]
        public bool GuideFlage { get; set; }
    }
}
