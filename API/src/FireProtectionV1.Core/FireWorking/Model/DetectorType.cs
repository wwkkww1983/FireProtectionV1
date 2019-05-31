using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    /// <summary>
    /// 探测器类型
    /// </summary>
    public class DetectorType : EntityBase
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public byte GBType { get; set; }
    }
}
