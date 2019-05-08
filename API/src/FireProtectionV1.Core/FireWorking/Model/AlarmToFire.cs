using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    public class AlarmToFire : EntityBase
    {
        /// <summary>
        /// 报警标题
        /// </summary>
        [Required]
        [MaxLength(StringType.Normal)]
        public string AlarmTitle { get; set; }
        /// <summary>
        /// 报警描述
        /// </summary>
        [MaxLength(StringType.Long)]
        public string AlarmRemark { get; set; }
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
