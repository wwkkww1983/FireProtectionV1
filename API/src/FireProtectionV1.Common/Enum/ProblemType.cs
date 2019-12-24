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

    
}
