using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.TsjDevice.Dto
{
    public class NewMonitorInput
    {
        /// <summary>
        /// 电气火灾设施编号
        /// </summary>
        public string fireElectricDeviceSn { get; set; }
        /// <summary>
        /// 探测器标识
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 模拟量值
        /// </summary>
        public string analog { get; set; }
    }
}
