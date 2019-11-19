using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.MiniFireStationCore.Model
{
    public class MiniFireAction : EntityBase
    {
        /// <summary>
        /// 微型消防站Id
        /// </summary>
        public int MiniFireStationId { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [MaxLength(100)]
        public string Address { get; set; }
        /// <summary>
        /// 活动类别ID
        /// </summary>
        public int MiniFireActionTypeId { get; set; }
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
