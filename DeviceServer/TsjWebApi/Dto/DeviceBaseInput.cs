using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public abstract class DeviceBaseInput
    {
        /// <summary>
        /// 设备标识
        /// </summary>
        [MaxLength(50)]
        public string Identify { get; set; }
        /// <summary>
        /// 设备来源(厂家)
        /// </summary>
        [MaxLength(50)]
        public string Origin { get; set; }
    }
}
