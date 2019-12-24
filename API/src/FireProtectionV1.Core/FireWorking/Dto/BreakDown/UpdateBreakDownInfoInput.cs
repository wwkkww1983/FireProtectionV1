using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class UpdateBreakDownInfoInput
    {
        /// <summary>
        /// 故障ID
        /// </summary>
        public int BreakDownId { get; set; }
        /// <summary>
        /// 处理状态
        /// </summary>
        public HandleStatus HandleStatus { get; set; }
        /// <summary>
        /// 问题处理途径（1.自行处理 2.维保叫修）
        /// </summary>
        public HandleChannel SolutionWay { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string SolutionRemark { get; set; }
    }
}