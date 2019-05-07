using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Alarm.Model
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
        /// 数据采集设备Id
        /// </summary>
        [Required]
        public int CollectDeviceId { get; set; }
    }
}
