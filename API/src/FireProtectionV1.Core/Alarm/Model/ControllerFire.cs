using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Alarm.Model
{
    /// <summary>
    /// 火警预警控制器
    /// </summary>
    public class ControllerFire : EntityBase
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
    }
}
