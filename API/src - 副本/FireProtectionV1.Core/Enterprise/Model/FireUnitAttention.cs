using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Enterprise.Model
{
    /// <summary>
    /// 监管人员关注防火单位
    /// </summary>
    public class FireUnitAttention : EntityBase
    {
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }
        /// <summary>
        /// 消防部门用户Id
        /// </summary>
        [Required]
        public int FireDeptUserId { get; set; }
    }
}
