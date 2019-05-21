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
        /// 未指定
        /// </summary>
        UnKnow = 0,
        /// <summary>
        /// 正常在线
        /// </summary>
        Online = 1,
        /// <summary>
        /// 离线
        /// </summary>
        Offline = -1,
        /// <summary>
        /// 异常（表示当前在线但有故障，例如消火栓的水压偏低）
        /// </summary>
        Unusual = -2
    }
    public enum DeviceType
    {
        ControllerElectric=1,
        ControllerFire,
        DetectorElectric,
        DetectorFire
    }
}
