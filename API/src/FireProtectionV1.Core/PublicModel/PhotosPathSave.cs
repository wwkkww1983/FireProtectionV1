using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    /// <summary>
    /// 照片路径存储
    /// </summary>
    public class PhotosPathSave : EntityBase
    {
        /// <summary>
        /// 表名
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string TableName { get; set; }
        /// <summary>
        /// 数据ID
        /// </summary>
        [Required]
        public int DataId { get; set; }
        /// <summary>
        /// 照片地址
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string PhotoPath { get; set; }
    }
}
