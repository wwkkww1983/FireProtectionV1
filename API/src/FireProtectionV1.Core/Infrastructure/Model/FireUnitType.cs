using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Infrastructure.Model
{
    /// <summary>
    /// 防火单位类型
    /// </summary>
    public class FireUnitType : EntityBase
    {
        /// <summary>
        /// 防火单位类型名称
        /// </summary>
        [Required]
        [MaxLength(StringType.Short)]
        public string Name { get; set; }
    }
}
