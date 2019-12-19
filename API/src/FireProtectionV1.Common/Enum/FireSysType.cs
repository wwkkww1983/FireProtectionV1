using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FireProtectionV1.Common.Enum
{
    [Export("巡查方式")]
    public enum Patrol
    {
        /// <summary>
        /// 普通巡查
        /// </summary>
        [Description("未设置")]
        UnSet = 0,
        /// <summary>
        /// 普通巡查
        /// </summary>
        [Description("普通巡查")]
        NormalPatrol = 1,
        /// <summary>
        /// 扫码巡查
        /// </summary>
        [Description("扫码巡查")]
        ScanPatrol = 2,

    }
}
