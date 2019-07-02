using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetDataDutyOutput
    {
        /// <summary>
        /// 值班ID
        /// </summary>
        public int DutyId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreationTime { get; set; }
        /// <summary>
        /// 值班人员
        /// </summary>
        public string DutyUser { get; set; }
        /// <summary>
        /// 记录状态
        /// </summary>
        public ProblemStatusType DutyStatus { get; set; }
    }
}
