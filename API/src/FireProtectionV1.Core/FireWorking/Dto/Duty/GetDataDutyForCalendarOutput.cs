using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetDataDutyForCalendarOutput
    {
        /// <summary>
        /// 值班ID
        /// </summary>
        public int DutyId { get; set; }
        /// <summary>
        /// 创建日期（yyyy-MM-dd）
        /// </summary>
        public string CreationTime { get; set; }
        /// <summary>
        /// 记录状态
        /// </summary>
        public DutyOrPatrolStatus DutyStatus { get; set; }
    }
}
