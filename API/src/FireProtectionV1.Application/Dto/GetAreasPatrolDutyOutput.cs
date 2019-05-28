using FireProtectionV1.FireWorking.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Dto
{
    public class GetAreasPatrolDutyOutput
    {
        /// <summary>
        /// 消防巡查累计记录
        /// </summary>
        public int PatrolCount { get; set; }
        /// <summary>
        /// 最近月份巡查数量
        /// </summary>
        public List<MonthCount> PatrolMonthCounts { get; set; }
        /// <summary>
        /// 超过7天没有巡查记录的单位数量
        /// </summary>
        public int NoWork7DayCount { get; set; }
        /// <summary>
        /// 超过7天没有巡查记录的单位前10位
        /// </summary>
        public List<FireUnitManualOuput> PatrolFireUnitManualOuputs { get; set; }
        /// <summary>
        /// 消防值班累计记录
        /// </summary>
        public int DutyCount { get; set; }
        /// <summary>
        /// 最近月份值班数量
        /// </summary>
        public List<MonthCount> DutyMonthCounts { get; set; }
        /// <summary>
        /// 超过1天没有值班记录的单位数量
        /// </summary>
        public int NoWork1DayCount { get; set; }
        /// <summary>
        /// 超过1天没有值班记录的单位前10位
        /// </summary>
        public List<FireUnitManualOuput> DutyFireUnitManualOuputs { get; set; }
    }
}
