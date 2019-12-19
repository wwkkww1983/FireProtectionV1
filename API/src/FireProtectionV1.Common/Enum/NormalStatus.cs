using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FireProtectionV1.Common.Enum
{
    /// <summary>
    /// 常用的状态枚举
    /// </summary>
    [Export("常用状态枚举")]
    public enum NormalStatus
    {
        /// <summary>
        /// 已启用
        /// </summary>
        [Description("已启用")]
        Enabled = 1,
        /// <summary>
        /// 已停用
        /// </summary>
        [Description("已停用")]
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
        Unusual = -2,
        /// <summary>
        /// 部分离线
        /// </summary>
        [Description("部分离线")]
        PartOffline = -3
    }
    /// <summary>
    /// 故障来源
    /// </summary>
    [Export("故障来源")]
    public enum FaultSource
    {
        /// <summary>
        /// 未指定
        /// </summary>
        [Description("未指定")]
        UnKnow = 0,
        /// <summary>
        /// 值班
        /// </summary>
        [Description("值班")]
        Duty = 1,
        /// <summary>
        /// 巡查
        /// </summary>
        [Description("巡查")]
        Patrol = 2,
        /// <summary>
        /// 物联终端
        /// </summary>
        [Description("物联终端")]
        Terminal = 3
    }
    /// <summary>
    /// 处理状态
    /// </summary>
    [Export("处理状态")]
    public enum HandleStatus
    {
        /// <summary>
        /// 未处理
        /// </summary>
        [Description("未处理")]
        UnResolve = 1,
        /// <summary>
        /// 处理中，该值仅用于查询，实际数据库中不会存这个数值，处理中=自行处理中+维保叫修处理中+维保叫修已处理
        /// </summary>
        [Description("处理中")]
        Resolving = 2,
        /// <summary>
        /// 已解决
        /// </summary>
        [Description("已解决")]
        Resolved = 3,
        /// <summary>
        /// 自行处理中
        /// </summary>
        [Description("自行处理中")]
        SelfHandle = 4,
        /// <summary>
        /// 维保叫修处理中
        /// </summary>
        [Description("维保叫修处理中")]
        SafeResolving = 5,
        /// <summary>
        /// 维保叫修已处理
        /// </summary>
        [Description("维保叫修已处理")]
        SafeResolved = 6
    }
    /// <summary>
    /// 处理途径
    /// </summary>
    [Export("处理途径")]
    public enum HandleChannel
    {
        /// <summary>
        /// 自行处理
        /// </summary>
        [Description("自行处理")]
        Self = 1,
        /// <summary>
        /// 维保叫修
        /// </summary>
        [Description("维保叫修")]
        Maintenance = 2
    }
    public class GatewayStatusNames
    {
        static public string GetName(GatewayStatus status)
        {
            if (status == GatewayStatus.UnKnow)
                return "未指定";
            if (status == GatewayStatus.Online)
                return "在线";
            if (status == GatewayStatus.Offline)
                return "离线";
            if (status == GatewayStatus.Unusual)
                return "异常";
            if (status == GatewayStatus.PartOffline)
                return "部分离线";
            return "";
        }
    }
}
