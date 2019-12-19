using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Enterprise.Model
{
    public class SafeUnit : EntityBase
    {
        /// <summary>
        /// 维保单位名称
        /// </summary>
        [Required]
        [MaxLength(StringType.Normal)]
        public string Name { get; set; }
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
        /// <summary>
        /// 工程资质（一、二、三级）
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 邀请码（自动生成）
        /// </summary>
        [MaxLength(StringType.Short)]
        public string InvitationCode { get; set; }
    }
}
