using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetAreasAlarmElectricOutput
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
        /// 联网监控点位数量
        /// </summary>
        public int JoinPointCount { get; set; }
        /// <summary>
        /// 联网监控点位（单位类型柱状图）
        /// </summary>
        public JoinTypePointCount[] JoinTypePointCounts { get; set; }
        /// <summary>
        /// 网关离线单位数量
        /// </summary>
        public int OfflineFireUnitsCount { get; set; }
        /// <summary>
        /// 网关离线单位
        /// </summary>
        public OfflineFireUnit[] OfflineFireUnits { get; set; }
        /// <summary>
        /// 安全用电累计预警（最近月份流量图）
        /// </summary>
        public MonthAlarmCount[] MonthAlarmCounts { get; set; }
        /// <summary>
        /// 最近30天报警次数Top10
        /// </summary>
        public Top10FireUnit[] Top10FireUnits { get; set; }
    }

    public class JoinTypeCount
    {
        /// <summary>
        /// 防火单位类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 防火单位类型数量
        /// </summary>
        public int Count { get; set; }
    }
    public class JoinTypePointCount
    {
        /// <summary>
        /// 防火单位类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 监控点位数量
        /// </summary>
        public int Count { get; set; }
    }
    public class OfflineFireUnit
    {
        /// <summary>
        /// 防火单位名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 离线时间
        /// </summary>
        public string Time { get; set; }
    }
    public class MonthAlarmCount
    {
        /// <summary>
        /// 月份
        /// </summary>
        public string Month { get; set; }
        /// <summary>
        /// 预警数量
        /// </summary>
        public int Count { get; set; }
    }
    public class Top10FireUnit
    {
        /// <summary>
        /// 防火单位名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 管控点位数量
        /// </summary>
        public int PointCount { get; set; }
        /// <summary>
        /// 预警次数
        /// </summary>
        public int AlarmCount { get; set; }
    }
}
