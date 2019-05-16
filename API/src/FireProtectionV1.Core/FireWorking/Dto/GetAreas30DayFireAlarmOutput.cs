using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetAreas30DayFireAlarmOutput
    {
        /// <summary>
        /// 防火单位Id
        /// </summary>
        public int FireUnitId { get; set; }
        /// <summary>
        /// 防火单位名称
        /// </summary>
        public string FireUnitName { get; set; }
        /// <summary>
        /// 防火单位类型Id
        /// </summary>
        public int TypeId { get; set; }
        /// <summary>
        /// 防火单位类型名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 最近报警时间
        /// </summary>
        public string AlarmTime { get; set; }
        /// <summary>
        /// 最近30天报警次数
        /// </summary>
        public int AlarmCount { get; set; }
        /// <summary>
        /// 高频报警部件数量
        /// </summary>
        public int HighFreqCount { get; set; }
        /// <summary>
        /// 网关状态值
        /// </summary>
        public GatewayStatus StatusValue { get; set; }
        /// <summary>
        /// 网关状态名称
        /// </summary>
        public string StatusName { get; set; }
    }
}
