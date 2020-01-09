using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FireProtectionV1.Common.Enum
{
    /// <summary>
    /// 消防水管网设备状态
    /// </summary>
    [Export("消防水管网设备状态")]
    public enum FireWaterDeviceState
    {
        /// <summary>
        /// 良好
        /// </summary>
        [Description("良好")]
        Good = 2,
        /// <summary>
        /// 超限
        /// </summary>
        [Description("超限")]
        Transfinite = -2,
        /// <summary>
        /// 离线
        /// </summary>
        [Description("离线")]
        Offline = -1
    }
    /// <summary>
    /// 警情类型
    /// </summary>
    [Export("警情类型")]
    public enum AlarmType
    {
        /// <summary>
        /// 火警联网
        /// </summary>
        [Description("火警联网")]
        Fire = 1,
        /// <summary>
        /// 电气火灾
        /// </summary>
        [Description("电气火灾")]
        Electric = 2,
        /// <summary>
        /// 消防管网
        /// </summary>
        [Description("消防管网")]
        Water = 3
    }
    /// <summary>
    /// 访问接口的来源
    /// </summary>
    [Export("访问接口的来源")]
    public enum VisitSource
    {
        /// <summary>
        /// PC端
        /// </summary>
        [Description("PC端")]
        PC = 1,
        /// <summary>
        /// 手机端
        /// </summary>
        [Description("手机端")]
        Phone = 2
    }
    /// <summary>
    /// 天树聚物联设备类型
    /// </summary>
    [Export("天树聚物联设施类型")]
    public enum TsjDeviceType
    {
        /// <summary>
        /// 火警联网设施
        /// </summary>
        [Description("火警联网设施")]
        FireAlarm = 1,
        /// <summary>
        /// 电气火灾监测设施
        /// </summary>
        [Description("电气火灾监测设施")]
        FireElectric = 2,
        /// <summary>
        /// 消防水管网监测设施
        /// </summary>
        [Description("消防水管网监测设施")]
        FireWater = 3
    }
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
        /// <summary>
        /// 单相
        /// </summary>
        [Description("单相")]
        Single = 1,
        /// <summary>
        /// 三相
        /// </summary>
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
        /// 良好
        /// </summary>
        [Description("良好")]
        Good = 2,
        /// <summary>
        /// 隐患
        /// </summary>
        [Description("隐患")]
        Danger = -3,
        /// <summary>
        /// 超限
        /// </summary>
        [Description("超限")]
        Transfinite = -2,
        /// <summary>
        /// 离线
        /// </summary>
        [Description("离线")]
        Offline = -1,
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
        /// 未核警
        /// </summary>
        [Description("未核警")]
        UnCheck = 1,
        /// <summary>
        /// 误报
        /// </summary>
        [Description("误报")]
        False = 2,
        /// <summary>
        /// 测试
        /// </summary>
        [Description("测试")]
        Test = 3,
        /// <summary>
        /// 真实火警
        /// </summary>
        [Description("真实火警")]
        True = 4,
        /// <summary>
        /// 已过期
        /// </summary>
        [Description("已过期")]
        Expire = 5
    }
    /// <summary>
    /// 描述类型
    /// </summary>
    [Export("描述类型")]
    public enum RemakeType
    {
        /// <summary>
        /// 文字
        /// </summary>
        [Description("文字")]
        Text = 1,
        /// <summary>
        /// 语音
        /// </summary>
        [Description("语音")]
        Voice = 2
    }
    /// <summary>
    /// 值班记录或巡查记录的状态
    /// </summary>
    [Export("值班记录或巡查记录的状态")]
    public enum DutyOrPatrolStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 1,
        /// <summary>
        /// 绿色故障(已现场解决)
        /// </summary>
        [Description("绿色故障(已现场解决)")]
        Repaired = 2,
        /// <summary>
        /// 橙色故障(未现场解决)
        /// </summary>
        [Description("橙色故障(未现场解决)")]
        DisRepaired = 3,
        /// <summary>
        /// 未提交（仅用于巡查主记录）
        /// </summary>
        [Description("未提交（仅用于巡查主记录）")]
        NoSubmit = 4
    }
    /// <summary>
    /// 巡查方式
    /// </summary>
    [Export("巡查方式")]
    public enum PatrolType
    {
        /// <summary>
        /// 普通巡查
        /// </summary>
        [Description("普通巡查")]
        NormalPatrol = 1,
        /// <summary>
        /// 扫码巡查
        /// </summary>
        [Description("扫码巡查")]
        ScanPatrol = 2
    }
}
