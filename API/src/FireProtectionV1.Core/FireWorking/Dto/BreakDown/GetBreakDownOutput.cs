using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetBreakDownOutput
    {
        /// <summary>
        /// ID
        /// </summary>
        public int BreakDownId { get; set; }
        /// <summary>
        /// 问题来源
        /// </summary>
        public FaultSource Source { get; set; }
        /// <summary>
        /// 记录人员ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 记录人员姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 记录人员联系方式
        /// </summary>
        public string UserPhone { get; set; }
        /// <summary>
        /// 问题记录人员归属的单位Id。如果是本防火单位人员，则该值与FireUnitId一致，如果是维保人员，则该值存放维保单位的Id
        /// </summary>
        public int UserBelongUnitId { get; set; }
        /// <summary>
        /// 发现时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 处理状态
        /// </summary>
        public HandleStatus HandleStatus { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime SolutionTime { get; set; }
        /// <summary>
        /// 问题处理途径
        /// </summary>
        public HandleChannel SolutionWay { get; set; }
        /// <summary>
        /// 派单时间
        /// </summary>
        public DateTime DispatchTime { get; set; }
        /// <summary>
        /// 维保处理完成时间
        /// </summary>
        public DateTime SafeCompleteTime { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        public int FireUnitId { get; set; }
    }
}
