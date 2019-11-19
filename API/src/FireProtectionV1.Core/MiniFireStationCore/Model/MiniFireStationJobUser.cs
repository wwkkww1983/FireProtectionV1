using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.MiniFireStationCore.Model
{
    public class MiniFireStationJobUser : EntityBase
    {
        /// <summary>
        /// 微型消防站ID
        /// </summary>
        public int MiniFireStationId { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        [MaxLength(20)]
        public string ContactName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [MaxLength(11)]
        public string ContactPhone { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        [MaxLength(10)]
        public string Job { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadPhotoBase64 { get; set; }
    }
}
