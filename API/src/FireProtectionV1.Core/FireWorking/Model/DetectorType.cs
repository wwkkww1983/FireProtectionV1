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
        /// <summary>
        /// 国标类型
        /// </summary>
        public byte GBType { get; set; }
        /// <summary>
        /// 是否应用于天树聚系统，在火警联网设施->联网部件，选择部件类型时使用
        /// </summary>
        public bool ApplyForTSJ { get; set; }
    }
}
