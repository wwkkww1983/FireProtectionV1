using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetPatrolListOutput
    {
        /// <summary>
        /// 记录ID
        /// </summary>
        public int PatrolId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 巡查人员姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 巡查人员手机
        /// </summary>
        public string UserPhone { get; set; }
        /// <summary>
        /// 记录状态
        /// </summary>
        public DutyOrPatrolStatus PatrolStatus { get; set; }
        /// <summary>
        /// 巡查人员Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        public int FireUnitId { get; set; }
        /// <summary>
        /// 巡查人员归属的单位Id。如果是本防火单位人员，则该值与FireUnitId一致，如果是维保人员，则该值存放维保单位的Id
        /// </summary>
        public int UserBelongUnitId { get; set; }
    }
}
