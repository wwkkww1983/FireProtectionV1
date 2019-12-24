using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetDataDutyForWebOutput
    {
        /// <summary>
        /// 年
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// 月
        /// </summary>
        public int Month { get; set; }
        /// <summary>
        /// 值班记录
        /// </summary>
        public List<DataDutyForWeb> DataDutyForWebList { get; set; }
    }

    public class DataDutyForWeb
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
