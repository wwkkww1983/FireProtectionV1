using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class AddOnlineDetectorInput : DeviceBaseInput
    {
        /// <summary>
        /// true在线 false 离线
        /// </summary>
        public bool IsOnline { get; set; }
        /// <summary>
        /// 网关设备标识
        /// </summary>
        public string GatewayIdentify { get; set; }
    }
}
