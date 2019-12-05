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
        /// 设备标识
        /// </summary>
        [MaxLength(50)]
        public string Identify { get; set; }
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
        /// <summary>
        /// 设备地点
        /// </summary>
        [MaxLength(100)]
        public string Location { get; set; }
        /// <summary>
        /// 设备来源(厂家)
        /// </summary>
        [MaxLength(50)]
        public string Origin { get; set; }

        /// <summary>
        /// 探测器状态，在线/离线/当前模拟量值
        /// </summary>
        [MaxLength(10)]
        public string State { get; set; }
        /// <summary>
        /// 所在楼层
        /// </summary>
        public int FireUnitArchitectureFloorId { get; set; }
        /// <summary>
        /// 故障数量
        /// </summary>
        public int FaultNum { get; set; }
        /// <summary>
        /// 最后一次故障ID
        /// </summary>
        public int LastFaultId { get; set; }
    }
}
