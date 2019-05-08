using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    /// <summary>
    /// 巡查记录详情表
    /// </summary>
    public class DataToPatrolDetail : EntityBase
    {
        /// <summary>
        /// 巡查记录Id
        /// </summary>
        [Required]
        public int PatrolId { get; set; }
        /// <summary>
        /// 消防设施编号
        /// </summary>
        [MaxLength(20)]
        public string DeviceSn { get; set; }
        /// <summary>
        /// 记录状态（1、正常；2：绿色故障；3：橙色故障）
        /// </summary>
        public byte PatrolStatus { get; set; }
    }
}
