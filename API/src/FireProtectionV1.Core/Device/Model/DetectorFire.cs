using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Device.Model
{
    /// <summary>
    /// 火警预警探测器
    /// </summary>
    public class DetectorFire : EntityBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(20)]
        public string Name { get; set; }
        /// <summary>
        /// 火警预警控制器Id
        /// </summary>
        [Required]
        public int ControllerId { get; set; }
    }
}
