using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.StreetGridCore.Model
{
    public class StreetGrid : EntityBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(StringType.Normal)]
        public string Name { get; set; }
        /// <summary>
        /// 所属街道
        /// </summary>
        [Required]
        public string Street { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        [MaxLength(StringType.Normal)]
        public string ContractName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [Phone]
        [MaxLength(20)]
        public string ContractPhone { get; set; }
    }
}
