using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class FireUnitFaultOuput
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
        /// 发生故障数量
        /// </summary>
        public int FaultCount { get; set; }
        /// <summary>
        /// 处理中故障数量
        /// </summary>
        public int ProcessedCount { get; set; }
        /// <summary>
        /// 待处理故障数量
        /// </summary>
        public int PendingCount { get; set; }
    }
}
