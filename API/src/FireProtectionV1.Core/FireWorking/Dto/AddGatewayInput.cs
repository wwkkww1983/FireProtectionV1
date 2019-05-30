using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class AddGatewayInput
    {
        /// <summary>
        /// 消防系统类型(1:安全用电,2:火警预警)appsetings.json"FireDomain:FireSysType"
        /// </summary>
        public byte FireSysType { get; set; }
        /// <summary>
        /// 设备标识
        /// </summary>
        [MaxLength(20)]
        public string Identify { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }
        /// <summary>
        /// 设备地点
        /// </summary>
        [MaxLength(100)]
        public string Location { get; set; }
    }
}
