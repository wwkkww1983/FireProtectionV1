using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.MiniFireStationCore.Model
{
    public class MiniFireStation : EntityBase
    {
        /// <summary>
        /// 站点名称
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 站点等级
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactPhone { get; set; }
        /// <summary>
        /// 人员配备数量
        /// </summary>
        public int PersonNum { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        [Required]
        public decimal Lng { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        [Required]
        public decimal Lat { get; set; }
    }
}
