using FireProtectionV1.User.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class AddFireUnitInput
    {
        /// <summary>
        /// 防火单位名称
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 防火单位类型Id
        /// </summary>
        [Required]
        public int TypeId { get; set; }
        /// <summary>
        /// 区域Id
        /// </summary>
        [Required]
        public int AreaId { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContractName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContractPhone { get; set; }
        /// <summary>
        /// 维保单位Id
        /// </summary>
        public int SafeUnitId { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
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
