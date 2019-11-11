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
        public byte Source { get; set; }
        /// <summary>
        /// 发现人员
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 发现人员联系方式
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 发现时间
        /// </summary>
        public string CreationTime { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public string SolutionTime { get; set; }
        /// <summary>
        /// 派单时间
        /// </summary>
        public string DispatchTime { get; set; }
    }
}
