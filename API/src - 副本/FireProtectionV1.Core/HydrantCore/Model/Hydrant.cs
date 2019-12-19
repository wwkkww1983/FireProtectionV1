using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.HydrantCore.Model
{
    /// <summary>
    /// 市政消火栓网关
    /// </summary>
    public class Hydrant : EntityBase
    {
        /// <summary>
        /// 设施编号
        /// </summary>
        [Required]
        public string Sn { get; set; }
        /// <summary>
        /// 所属区域（街道）
        /// </summary>
        public int AreaId { get; set; }
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
        /// <summary>
        /// 网关状态
        /// </summary>
        public GatewayStatus Status { get; set; }
        /// <summary>
        /// 剩余电量
        /// </summary>
        [DefaultValue(0)]
        public decimal DumpEnergy { get; set; }
    }
}
