using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class AddFireWaterRecordInput
    {
        /// <summary>
        /// 消防水管网监测网关编号
        /// </summary>
        public string FireWaterGatewaySn { get; set; }
        /// <summary>
        /// 消防水管网监测设备地址编码
        /// </summary>
        public string FireWaterDeviceSn { get; set; }
        /// <summary>
        /// 数值
        /// </summary>
        public double Value { get; set; }
    }
}
