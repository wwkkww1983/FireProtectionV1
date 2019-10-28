using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class AddOnlineGatewayInput : DeviceBaseInput
    {
        /// <summary>
        /// true在线 false 离线
        /// </summary>
        public bool IsOnline { get; set; }
    }
}
