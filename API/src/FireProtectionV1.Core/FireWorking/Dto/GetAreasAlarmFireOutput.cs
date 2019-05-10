using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetAreasAlarmFireOutput
    {
        /// <summary>
        /// 联网防火单位数量
        /// </summary>
        public int JoinFireUnitCount { get; set; }
        /// <summary>
        /// 联网防火单位（单位类型饼状图）
        /// </summary>
        public JoinTypeCount[] JoinTypeCounts { get; set; }
        /// <summary>
        /// 网关离线单位
        /// </summary>
        public OfflineFireUnit[] OfflineFireUnits { get; set; }
        /// <summary>
        /// 火警累计预警（最近月份流量图）
        /// </summary>
        public MonthAlarmCount[] MonthAlarmCounts { get; set; }
        /// <summary>
        /// 最近30天报警次数Top10
        /// </summary>
        public Top10FireUnit[] Top10FireUnits { get; set; }
    }
}
