using System;
using System.Collections.Generic;
using System.Text;

namespace TsjDeviceServer.Data
{
    /// <summary>
    /// 上报数据
    /// </summary>
    public class ReportData
    {
        /// <summary>
        /// 探测器标识
        /// </summary>
        public string Identify { get; set; }
        /// <summary>
        /// 探测器国标类型
        /// </summary>
        public byte GBType { get; set; }
        /// <summary>
        /// 模拟量值(可空)
        /// </summary>
        public string Analog { get; set; }
    }
}
