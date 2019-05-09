using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    /// <summary>
    /// 安全用电控制器
    /// </summary>
    public class ControllerElectric : EntityBase
    {
        /// <summary>
        /// 编号
        /// </summary>
        [MaxLength(20)]
        public string Sn { get; set; }
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
        public DateTime StatusChangeTime{get;set;}
    }
}
