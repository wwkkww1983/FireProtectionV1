using FireProtectionV1.FireWorking.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Dto
{
    public class GetAreasFaultOutput
    {
        /// <summary>
        /// 设施故障累计数量
        /// </summary>
        public int FaultCount { get; set; }
        /// <summary>
        /// 设施故障待处理累计数量
        /// </summary>
        public int FaultPendingCount { get; set; }
        /// <summary>
        /// 最近月份故障数量
        /// </summary>
        public List<MonthFaultCount> MonthFaultCounts { get; set; }
        /// <summary>
        /// 待处理故障单位前10位
        /// </summary>
        public List<PendingFaultFireUnitOutput> PendingFaultFireUnits { get; set; }
    }
    /// <summary>
    /// 月故障数量
    /// </summary>
    public class MonthFaultCount
    {
        /// <summary>
        /// 月份
        /// </summary>
        public string Month { get; set; }
        /// <summary>
        /// 故障数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 待处理故障数量
        /// </summary>
        public int PendingCount { get; set; }
    }
}
