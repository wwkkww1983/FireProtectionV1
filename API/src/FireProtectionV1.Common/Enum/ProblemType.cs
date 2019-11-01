using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FireProtectionV1.Common.Enum
{
    [Export("问题描述类型")]
    public enum ProblemType:byte
    {
        /// <summary>
        /// 未指定
        /// </summary>
        [Description("未指定")]
        unselect = 0,
        /// <summary>
        /// 文本
        /// </summary>
        [Description("文本")]
        text = 1,
        /// <summary>
        /// 语音
        /// </summary>
        [Description("语音")]
        voice = 2
    }

    [Export("故障类型")]
    public enum ProblemStatusType
    {
        /// <summary>
        /// 所有记录
        /// </summary>
        [Description("未指定")]
        alldate = 0,
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        noraml = 1,
        /// <summary>
        /// 绿色故障(已现场解决)
        /// </summary>
        [Description("绿色故障(已现场解决)")]
        Repaired = 2,
        /// <summary>
        /// 橙色故障(未现场解决)
        /// </summary>
        [Description("橙色故障(未现场解决)")]
        DisRepaired = 3
    }
}
