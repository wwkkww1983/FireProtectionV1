using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [MaxLength(StringType.Long)]
        public string Address { get; set; }
        /// <summary>
        /// 防火单位类型
        /// </summary>
        [Required]
        public int TypeId { get; set; }
        /// <summary>
        /// 法人
        /// </summary>
        public string LegalPerson { get; set; }
        /// <summary>
        /// 法人联系电话
        /// </summary>
        public string LegalPersonPhone { get; set; }
        /// <summary>
        /// 专兼职消防员数量
        /// </summary>
        public int FiremanNum { get; set; }
        /// <summary>
        /// 职工人数
        /// </summary>
        public int WorkerNum { get; set; }
        /// <summary>
        /// 所属区域
        /// </summary>
        [Required]
        public int AreaId { get; set; }
        /// <summary>
        /// 联系人（消防管理负责人）
        /// </summary>
        [MaxLength(StringType.Normal)]
        public string ContractName { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
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
        /// <summary>
        /// 巡查方式
        /// </summary>
        public PatrolType Patrol { get; set; }
        /// <summary>
        /// 归口消防监管部门
        /// </summary>
        public int FireDeptId { get; set; }
        /// <summary>
        /// 归口部门联系人
        /// </summary>
        public string FireDeptContractName { get; set; }
        /// <summary>
        /// 归口部门联系人电话
        /// </summary>
        public string FireDeptContractPhone { get; set; }
        /// <summary>
        /// 总平图存放路径
        /// </summary>
        public string ZP_Picture { get; set; }
    }
}
