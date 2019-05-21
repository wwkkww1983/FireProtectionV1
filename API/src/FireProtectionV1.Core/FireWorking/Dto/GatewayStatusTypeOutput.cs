using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    /// <summary>
    /// 网关状态类型
    /// </summary>
    public class GatewayStatusTypeOutput
    {
        /// <summary>
        /// 网关状态值
        /// </summary>
        public string GatewayStatusValue { get; set; }
        /// <summary>
        /// 网关状态名称
        /// </summary>
        public string GatewayStatusName { get; set; }
    }
}
