using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Account.Model
{
    public class FireUnitAccount : EntityBase
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
        /// 防火单位ID
        /// </summary>
        public int FireUnitInfoID { get; set; }
        /// <summary>
        /// 状态，默认为已启用
        /// </summary>
        public NormalStatus Status { get; set; } = NormalStatus.Enabled;
    }
}
