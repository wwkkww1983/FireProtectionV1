using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    public class AlarmToElectric: EntityBase
    {
        /// <summary>
        /// 探测器Id
        /// </summary>
        [Required]
        public int DetectorId { get; set; }
        /// <summary>
        /// 实时数据
        /// </summary>
        [Required]
        public decimal CurrentData { get; set; }
        /// <summary>
        /// 安全范围
        /// </summary>
        [MaxLength(StringType.Normal)]
        public string SafeRange { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }
    }
}
