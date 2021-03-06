﻿using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    public class AlarmToGas : EntityBase
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
        /// <summary>
        /// 终端设备sn号
        /// </summary>
        [Required]
        [MaxLength(StringType.Short)]
        public string TerminalDeviceSn { get; set; }
    }
}
