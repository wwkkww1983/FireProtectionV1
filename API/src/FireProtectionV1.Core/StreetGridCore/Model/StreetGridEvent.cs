using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FireProtectionV1.StreetGridCore.Model
{
    public class StreetGridEvent : EntityBase
    {
        /// <summary>
        /// 事件标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 事件类型
        /// </summary>
        public string EventType { get; set; }
        /// <summary>
        /// 街道网格员Id
        /// </summary>
        public int StreetGridUserId { get; set; }
        /// <summary>
        /// 事件状态
        /// </summary>
        public EventStatus Status { get; set; }
    }

    [Export("网格事件状态")]
    public enum EventStatus
    {
        [Description("未指定")]
        未指定 = 0,
        [Description("待处理")]
        待处理 = 1,
        [Description("处理中")]
        处理中 = 2,
        [Description("已办结")]
        已办结 = 3
    }
}
