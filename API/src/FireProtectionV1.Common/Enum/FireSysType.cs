using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Common.Enum
{
    /// <summary>
    /// 消防报警系统类型
    /// </summary>
    public enum FireSysType:byte
    {
        /// <summary>
        /// 电气火灾
        /// </summary>
        Electric = 1,
        /// <summary>
        /// 火警预警
        /// </summary>
        Fire = 2
    }
}
