using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.User.Model
{
    /// <summary>
    /// 维保单位用户关联防火单位
    /// </summary>
    public class SafeUnitUserFireUnit : EntityBase
    {
        /// <summary>
        /// 维保人员ID
        /// </summary>
        [Required]
        public int SafeUnitUserId { get; set; }
        /// <summary>
        /// 防火单位ID
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }
    }
}
