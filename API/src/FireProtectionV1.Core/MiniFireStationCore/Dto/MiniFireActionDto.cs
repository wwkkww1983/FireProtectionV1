using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.MiniFireStationCore.Dto
{
    public class MiniFireActionDto
    {
        /// <summary>
        /// 活动ID
        /// </summary>
        public int MiniFireActionId { get; set; }
        /// <summary>
        /// 活动日期(格式：yyyy-MM-dd)
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 活动类别
        /// </summary>
        public string Type { get; set; }

    }
    public class MiniFireActionDetailDto: MiniFireActionDto
    {
        /// <summary>
        /// 活动内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 活动问题
        /// </summary>
        public string Problem { get; set; }
    }
    public class MiniFireActionAddDto
    {
        /// <summary>
        /// 微型消防站ID
        /// </summary>
        public int MiniFireStationId { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 活动类别
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 活动内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 活动问题
        /// </summary>
        public string Problem { get; set; }
    }
}
