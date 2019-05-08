using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Device.Model
{
    public class AlarmToElectric: EntityBase
    {
        /// <summary>
        /// 实时数据
        /// </summary>
        [Required]
        public decimal CurrentData { get; set; }
        /// <summary>
        /// 安全范围
        /// </summary>
        [MaxLength(StringType.Normal)]
        public string SafeRange { get; set; }
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
