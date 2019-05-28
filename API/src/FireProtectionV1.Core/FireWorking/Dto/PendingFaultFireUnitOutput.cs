using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    /// <summary>
    /// 待处理故障单位
    /// </summary>
    public class PendingFaultFireUnitOutput
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
        /// 故障数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 待处理故障数量
        /// </summary>
        public int PendingCount { get; set; }
    }
}
