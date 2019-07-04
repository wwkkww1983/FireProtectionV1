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
    public class RecordOnline : EntityBase
    {
        /// <summary>
        /// 探测器Id
        /// </summary>
        [Required]
        public int DetectorId { get; set; }
        /// <summary>
        /// 探测器状态
        /// </summary>
        [Required]
        public sbyte State { get; set; }
    }
}
