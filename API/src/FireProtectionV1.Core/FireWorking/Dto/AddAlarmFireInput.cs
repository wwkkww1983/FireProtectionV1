using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class AddAlarmFireInput
    {
        /// <summary>
        /// 部件国标类型
        /// </summary>
        public byte DetectorGBType { get; set; }
        /// <summary>
        /// 设备标识
        /// </summary>
        [MaxLength(20)]
        public string Identify { get; set; }
        /// <summary>
        /// 网关设备标识
        /// </summary>
        public string GatewayIdentify { get; set; }
    }
}
