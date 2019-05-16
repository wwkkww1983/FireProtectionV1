using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.StreetGridCore.Model
{
    /// <summary>
    /// 网格员
    /// </summary>
    public class StreetGridUser : EntityBase
    {
        /// <summary>
        /// 网格员姓名
        /// </summary>
        [MaxLength(StringType.Normal)]
        public string Name { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [Phone]
        [MaxLength(20)]
        public string Phone { get; set; }
        /// <summary>
        /// 网格名称
        /// </summary>
        [Required]
        [MaxLength(StringType.Normal)]
        public string GridName { get; set; }
        /// <summary>
        /// 所属街道
        /// </summary>
        [Required]
        public string Street { get; set; }
        /// <summary>
        /// 所属社区
        /// </summary>
        public string Community { get; set; }
        
    }
}
