using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class UpdateDeviceStateInput
    {
        /// <summary>
        /// 设备网关类型
        /// </summary>
        public TsjDeviceType GatewayType { get; set; }
        /// <summary>
        /// 设备网关编号
        /// </summary>
        public string GatewaySn { get; set; }
        /// <summary>
        /// 在线/离线
        /// </summary>
        public GatewayStatus GatewayStatus { get; set; }
    }
}
