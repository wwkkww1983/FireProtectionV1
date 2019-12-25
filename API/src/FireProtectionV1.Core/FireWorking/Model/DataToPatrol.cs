using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
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
        /// 巡查人员Id
        /// </summary>
        [Required]
        public int UserId { get; set; }
        /// <summary>
        /// 巡查人员归属的单位Id。如果是本防火单位人员，则该值与FireUnitId一致，如果是维保人员，则该值存放维保单位的Id
        /// </summary>
        public int UserBelongUnitId { get; set; }
        /// <summary>
        /// 巡查模式
        /// </summary>
        public PatrolType PatrolType { get; set; }
        /// <summary>
        /// 记录状态
        /// </summary>
        public DutyOrPatrolStatus PatrolStatus { get; set; }
    }
}
