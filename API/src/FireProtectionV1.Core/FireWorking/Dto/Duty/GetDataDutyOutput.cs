using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetDataDutyOutput
    {
        /// <summary>
        /// 值班记录ID
        /// </summary>
        public int DutyId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 值班人员姓名
        /// </summary>
        public string DutyUserName { get; set; }
        /// <summary>
        /// 值班人员手机
        /// </summary>
        public string DutyUserPhone { get; set; }
        /// <summary>
        /// 记录状态
        /// </summary>
        public DutyOrPatrolStatus DutyStatus { get; set; }
    }
}
