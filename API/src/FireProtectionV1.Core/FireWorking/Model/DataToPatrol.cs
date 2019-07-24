using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    /// <summary>
    /// 巡查记录
    /// </summary>
    public class DataToPatrol : EntityBase
    {
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }
        /// <summary>
        /// 防火单位巡查人员Id
        /// </summary>
        [Required]
        public int FireUnitUserId { get; set; }
        /// <summary>
        /// 记录状态（1、正常；2：绿色故障；3：橙色故障）//根据detail实时变化
        /// </summary>
        public byte PatrolStatus { get; set; }
    }
}
