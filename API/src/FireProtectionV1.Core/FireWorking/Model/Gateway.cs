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
        /// 设备标识
        /// </summary>
        [MaxLength(20)]
        public string Identify { get; set; }
        /// <summary>
        /// 消防系统类型(1:安全用电,2:火警预警)appsetings.json"FireDomain:FireSysType"
        /// </summary>
        public byte FireSysType { get; set; }
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
        /// <summary>
        /// 设备地点
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Location { get; set; }
    }
}
