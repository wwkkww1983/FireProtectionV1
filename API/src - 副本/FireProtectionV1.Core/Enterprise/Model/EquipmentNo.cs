using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Enterprise.Model
{
    /// <summary>
    /// 设施编码
    /// </summary>
    public class EquipmentNo : EntityBase
    {
        /// <summary>
        /// 防火单位Id
        /// </summary>
        public int FireUnitId { get; set; }
        /// <summary>
        /// 设施编码
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string EquiNo { get; set; }
        /// <summary>
        /// 具体地址
        /// </summary>
        [Required]
        [MaxLength(StringType.Long)]
        public string Address { get; set; }
        /// <summary>
        /// 消防系统ID
        /// </summary>
        [Required]
        public int FireSystemId { get; set; }
    }
}
