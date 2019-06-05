using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class AddDataElecInput : DeviceBaseInput
    {
        /// <summary>
        /// 部件国标类型
        /// </summary>
        public byte DetectorGBType { get; set; }

        /// <summary>
        /// 网关设备标识
        /// </summary>
        public string GatewayIdentify { get; set; }
        /// <summary>
        /// 模拟量值
        /// </summary>
        [Required]
        public double Analog { get; set; }
        /// <summary>
        /// 计量单位
        /// </summary>
        [Required]
        [MaxLength(4)]
        public string Unit { get; set; }
    }
}
