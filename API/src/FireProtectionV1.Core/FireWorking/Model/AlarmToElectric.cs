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
        /// 模拟量值
        /// </summary>
        [Required]
        public double Analog { get; set; }
        /// <summary>
        /// 计量单位
        /// </summary>
        [Required]
        [MaxLength(4)]
        public string Unit { get; set; }
        /// <summary>
        /// 预警限值
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string AlarmLimit { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }
    }
}
