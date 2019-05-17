using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    /// <summary>
    /// 预警探测器
    /// </summary>
    public class Detector : EntityBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(20)]
        public string Name { get; set; }
        /// <summary>
        /// 探测器类型
        /// </summary>
        [Required]
        public int DetectorTypeId { get; set; }
        /// <summary>
        /// 消防系统类型(1:安全用电,2:火警预警)appsetings.json"FireDomain:FireSysType"
        /// </summary>
        public byte FireSysType { get; set; }
        /// <summary>
        /// 网关Id
        /// </summary>
        [Required]
        public int GatewayId { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }
    }
}
