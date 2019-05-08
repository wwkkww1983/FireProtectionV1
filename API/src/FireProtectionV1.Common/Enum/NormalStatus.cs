using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Common.Enum
{
    /// <summary>
    /// 常用的状态枚举
    /// </summary>
    public enum NormalStatus
    {
        /// <summary>
        /// 已启用
        /// </summary>
        Enabled = 1,
        /// <summary>
        /// 已停用
        /// </summary>
        Disabled = 2
    }

    /// <summary>
    /// 网关状态
    /// </summary>
    public enum GatewayStatus
    {
        /// <summary>
        /// 离线
        /// </summary>
        Offline = 0,
        /// <summary>
        /// 在线
        /// </summary>
        Online = 1
    }
}
