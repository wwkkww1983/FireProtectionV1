using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class AddEquipmentNoInput
    {
        /// <summary>
        /// 设施编码
        /// </summary>
        [Required]
        public string EquiNo { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        public int FireUnitId { get; set; }
        /// <summary>
        /// 具体地址
        /// </summary>
        [Required]
        public string Address { get; set; }
        /// <summary>
        /// 消防系统ID
        /// </summary>
        [Required]
        public int FireSystemId { get; set; }
    }
}
