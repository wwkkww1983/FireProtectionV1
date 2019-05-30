﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class AddDataElecInput
    {
        /// <summary>
        /// 部件类型
        /// </summary>
        public byte DetectorType { get; set; }
        /// <summary>
        /// 设备标识
        /// </summary>
        [MaxLength(20)]
        public string Identify { get; set; }
        /// <summary>
        /// 网关设备标识
        /// </summary>
        public string GatewayIdentify { get; set; }
        /// <summary>
        /// 模拟量值
        /// </summary>
        [Required]
        public decimal Analog { get; set; }
        /// <summary>
        /// 计量单位
        /// </summary>
        [Required]
        [MaxLength(4)]
        public string Unit { get; set; }
    }
}
