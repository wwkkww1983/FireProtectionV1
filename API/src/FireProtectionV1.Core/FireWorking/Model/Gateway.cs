using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    /// <summary>
    /// 预警探测器
    /// </summary>
    public class Gateway : EntityBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; }
        /// <summary>
        /// 网关类型
        /// </summary>
        [Required]
        public byte GatewayType { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }
        /// <summary>
        /// 网关状态
        /// </summary>
        public GatewayStatus Status { get; set; }
        /// <summary>
        /// 网关状态改变时间
        /// </summary>
        public DateTime StatusChangeTime { get; set; }
    }
}
