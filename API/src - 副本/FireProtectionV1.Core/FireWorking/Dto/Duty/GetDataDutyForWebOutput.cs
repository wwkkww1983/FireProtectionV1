using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetDataDutyForWebOutput
    {
        /// <summary>
        /// 值班ID
        /// </summary>
        public int DutyId { get; set; }
        /// <summary>
        /// 巡查时间
        /// </summary>
        public string CreationTime { get; set; }
        /// <summary>
        /// 记录状态
        /// </summary>
        public int DutyStatus { get; set; }
    }
}
