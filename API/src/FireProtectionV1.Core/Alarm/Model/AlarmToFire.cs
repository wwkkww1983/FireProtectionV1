using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Alarm.Model
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
        /// 数据采集设备Id
        /// </summary>
        [Required]
        public int CollectDeviceId { get; set; }
        /// <summary>
        /// 终端设备sn号
        /// </summary>
        [Required]
        [MaxLength(StringType.Short)]
        public string TerminalDeviceSn { get; set; }
    }
}
