using FireProtectionV1.FireWorking.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Dto
{
    public class FireUnitReportDataOutput: GetFireUnitAlarmOutput
    {
        /// <summary>
        /// 防火单位区域
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 防火单位地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 防火单位类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContractName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContractPhone { get; set; }
        /// <summary>
        /// 维保单位
        /// </summary>
        public string SafeUnit { get; set; }
        /// <summary>
        /// 最近消防站
        /// </summary>
        public string NearMinistation { get; set; }
        /// <summary>
        /// 消防栓数量
        /// </summary>
        public int MinistationCount { get; set; }
    }
}
