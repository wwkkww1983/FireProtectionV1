using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FireProtectionV1.Common.Enum
{
    [Export("火警联网部件状态")]
    public enum FireAlarmDetectorState
    {
        [Description("正常")]
        Normal = 1,
        [Description("故障")]
        Fault = 2
    }
    [Export("电缆温度监测相数")]
    public enum PhaseType
    {
        [Description("单相")]
        Single = 1,
        [Description("三相")]
        Third = 2
    }
    [Export("电气火灾设备状态")]
    public enum FireElectricDeviceState
    {
        /// <summary>
        /// 离线
        /// </summary>
        [Description("离线")]
        Offline = 0,
        /// <summary>
        /// 良好
        /// </summary>
        [Description("良好")]
        Good = 1,
        /// <summary>
        /// 隐患
        /// </summary>
        [Description("隐患")]
        Danger = 2,
        /// <summary>
        /// 超限
        /// </summary>
        [Description("超限")]
        Transfinite = 3
    }
    [Export("电气火灾记录类型")]
    public enum FireElectricDataType
    {
        [Description("剩余电流")]
        Ampere = 1,
        [Description("电缆温度")]
        Temperature = 2
    }
}
