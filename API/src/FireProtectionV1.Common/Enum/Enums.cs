using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FireProtectionV1.Common.Enum
{
    /// <summary>
    /// 火警联网部件状态
    /// </summary>
    [Export("火警联网部件状态")]
    public enum FireAlarmDetectorState
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 1,
        /// <summary>
        /// 故障
        /// </summary>
        [Description("故障")]
        Fault = 2
    }
    /// <summary>
    /// 电缆温度监测相数
    /// </summary>
    [Export("电缆温度监测相数")]
    public enum PhaseType
    {
        [Description("单相")]
        Single = 1,
        [Description("三相")]
        Third = 2
    }
    /// <summary>
    /// 电气火灾设备状态
    /// </summary>
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
    /// <summary>
    /// 电气火灾记录类型
    /// </summary>
    [Export("电气火灾记录类型")]
    public enum FireElectricDataType
    {
        /// <summary>
        /// 剩余电流
        /// </summary>
        [Description("剩余电流")]
        Ampere = 1,
        /// <summary>
        /// 电缆温度
        /// </summary>
        [Description("电缆温度")]
        Temperature = 2
    }
    /// <summary>
    /// 火警核警状态
    /// </summary>
    [Export("火警核警状态")]
    public enum FireAlarmCheckState
    {
        /// <summary>
        /// 未处理
        /// </summary>
        [Description("未处理")]
        UnCheck = 0,
        /// <summary>
        /// 误报
        /// </summary>
        [Description("误报")]
        False = 1,
        /// <summary>
        /// 测试
        /// </summary>
        [Description("测试")]
        Test = 2,
        /// <summary>
        /// 真实火警
        /// </summary>
        [Description("真实火警")]
        True = 3,
        /// <summary>
        /// 已过期
        /// </summary>
        [Description("已过期")]
        Expire = 4
    }

}
