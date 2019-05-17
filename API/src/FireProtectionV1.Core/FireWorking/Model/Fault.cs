using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    /// <summary>
    /// 设备设施故障
    /// </summary>
    public class Fault : EntityBase
    {
        /// <summary>
        /// 探测器Id
        /// </summary>
        [Required]
        public int DetectorId { get; set; }
        /// <summary>
        /// 故障标题
        /// </summary>
        [Required]
        [MaxLength(StringType.Normal)]
        public string FaultTitle { get; set; }
        /// <summary>
        /// 故障描述
        /// </summary>
        [MaxLength(StringType.Long)]
        public string FaultRemark { get; set; }
        /// <summary>
        /// 处理状态 0:待处理,1:已处理
        /// </summary>
        [Required]
        public byte ProcessState { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }
    }
}
