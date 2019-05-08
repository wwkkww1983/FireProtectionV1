using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Device.Model
{
    /// <summary>
    /// 巡查记录
    /// </summary>
    public class Patrol : EntityBase
    {
        /// <summary>
        /// 故障标题
        /// </summary>
        [Required]
        [MaxLength(StringType.Normal)]
        public string FaultTitle { get; set; }
        /// <summary>
        /// 巡查描述
        /// </summary>
        [MaxLength(StringType.Long)]
        public string MatterRemark { get; set; }
        /// <summary>
        /// 处理状态
        /// </summary>
        [Required]
        public byte ProcessState { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }
        /// <summary>
        /// 设备Id
        /// </summary>
        [Required]
        public int DeviceId { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        [Required]
        public byte DeviceType { get; set; }
    }
}
