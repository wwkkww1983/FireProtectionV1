using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
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
        /// 街道网格Id
        /// </summary>
        public int StreetGridId { get; set; }
        /// <summary>
        /// 所属社区
        /// </summary>
        public string Community { get; set; }
        /// <summary>
        /// 事件状态
        /// </summary>
        public EventStatus Status { get; set; }
    }

    public enum EventStatus
    {
        未指定 = 0,
        待处理 = 1,
        处理中 = 2,
        已办结 = 3
    }
}
