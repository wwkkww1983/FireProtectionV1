using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    /// <summary>
    /// 探测器数据记录
    /// </summary>
    public class RecordAnalog : EntityBase
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
    }
}
