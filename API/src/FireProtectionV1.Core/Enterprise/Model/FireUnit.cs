using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Enterprise.Model
{
    /// <summary>
    /// 防火单位
    /// </summary>
    public class FireUnit : EntityBase
    {
        /// <summary>
        /// 防火单位名称
        /// </summary>
        [Required]
        [MaxLength(StringType.Normal)]
        public string Name { get; set; }
        /// <summary>
        /// 防火单位地址
        /// </summary>
        [Required]
        [MaxLength(StringType.Long)]
        public string Address { get; set; }
        /// <summary>
        /// 防火单位类型
        /// </summary>
        [Required]
        public int TypeId { get; set; }
        /// <summary>
        /// 所属区域
        /// </summary>
        [Required]
        public int AreaId { get; set; }
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
        /// 邀请码（自动生成）
        /// </summary>
        [MaxLength(StringType.Short)]
        public string InvitationCode { get; set; }
        /// <summary>
        /// 维保单位Id
        /// </summary>
        public int SafeUnitId { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public decimal Lng { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public decimal Lat { get; set; }
    }
}
