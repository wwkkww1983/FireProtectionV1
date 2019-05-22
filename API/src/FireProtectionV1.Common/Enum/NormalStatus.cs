﻿using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    [Export("网关状态")]
    public enum GatewayStatus
    {
        /// <summary>
        /// 未指定
        /// </summary>
        [Description("未指定")]
        UnKnow = 0,
        /// <summary>
        /// 正常在线
        /// </summary>
        [Description("正常在线")]
        Online = 1,
        /// <summary>
        /// 离线
        /// </summary>
        [Description("离线")]
        Offline = -1,
        /// <summary>
        /// 异常（表示当前在线但有故障，例如消火栓的水压偏低）
        /// </summary>
        [Description("异常（表示当前在线但有故障，例如消火栓的水压偏低）")]
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
