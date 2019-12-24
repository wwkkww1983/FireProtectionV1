using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetDeviceStatusForDataScreenOutput
    {
        /// <summary>
        /// 设施类型
        /// </summary>
        public string DeviceType { get; set; }
        /// <summary>
        /// 各种状态及数量
        /// </summary>
        public List<NumOfStatus> DeviceStatusList { get; set; }
    }
    public class NumOfStatus
    {
        /// <summary>
        /// 状态名称
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 数值
        /// </summary>
        public int Num { get; set; }
    }
}
