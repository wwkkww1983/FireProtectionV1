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
        /// 联网防火单位类型数量分布
        /// </summary>
        public JoinTypeCount[] JoinTypeCounts { get; set; }
        /// <summary>
        /// 网关离线单位
        /// </summary>
        public OfflineFireUnit[] OfflineFireUnits { get; set; }
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
}
