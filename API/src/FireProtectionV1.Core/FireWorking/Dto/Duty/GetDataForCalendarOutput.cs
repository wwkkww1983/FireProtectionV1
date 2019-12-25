using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetDataForCalendarOutput
    {
        /// <summary>
        /// 记录ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 创建日期（yyyy-MM-dd）
        /// </summary>
        public string CreationTime { get; set; }
        /// <summary>
        /// 记录状态
        /// </summary>
        public DutyOrPatrolStatus Status { get; set; }
    }
}
