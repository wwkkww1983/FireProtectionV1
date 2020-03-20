using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Infrastructure.Model
{
    /// <summary>
    /// 区域表
    /// </summary>
    public class Area: EntityBase
    {
        /// <summary>
        /// 区域编码
        /// </summary>
        [Required]
        [MaxLength(StringType.Short)]
        public string AreaCode { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        [Required]
        [MaxLength(StringType.Normal)]
        public string Name { get; set; }
        /// <summary>
        /// 上级区域ID
        /// </summary>
        [Required]
        public int ParentId { get; set; }
        /// <summary>
        /// 区域路径
        /// </summary>
        [Required]
        [MaxLength(StringType.Normal)]
        public string AreaPath { get; set; }
        /// <summary>
        /// 区域级别
        /// </summary>
        [Required]
        public byte Grade { get; set; }
        /// <summary>
        /// 中心位置经度
        /// </summary>
        public decimal Lng { get; set; }
        /// <summary>
        /// 中心位置纬度
        /// </summary>
        public decimal Lat { get; set; }
    }
}
