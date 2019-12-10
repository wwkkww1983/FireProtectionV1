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
        [MaxLength(20)]
        public string Name { get; set; }
        /// <summary>
        /// 所属防火单位Id
        /// </summary>
        public int FireUnitId { get; set; }
        /// <summary>
        /// 站点等级
        /// </summary>
        public int Level { get; set; }
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
        /// 微型消防站站长人员ID
        /// </summary>
        public int StationUserId { get; set; }
        /// <summary>
        /// 人员配备数量
        /// </summary>
        public int PersonNum { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [MaxLength(50)]
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
        /// <summary>
        /// 外观图
        /// </summary>
        public string PhotoBase64 { get; set; }
    }
}
