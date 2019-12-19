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
    public class DataToPatrolDetailFireSystem : EntityBase
    {
        /// <summary>
        /// 巡查记录详情Id
        /// </summary>
        [Required]
        public int PatrolDetailId { get; set; }
        /// <summary>
        /// 消防系统ID
        /// </summary>
        [Required]
        public int FireSystemID { get; set; }
    }
}
