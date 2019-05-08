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
}
