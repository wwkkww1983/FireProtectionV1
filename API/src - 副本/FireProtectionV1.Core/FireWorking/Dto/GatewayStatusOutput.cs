using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GatewayStatusOutput
    {
        /// <summary>
        /// 设备网关
        /// </summary>
        public string Gateway { get; set; }
        /// <summary>
        /// 安装位置
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 在线状态
        /// </summary>
        public string Status { get; set; }
    }
}
