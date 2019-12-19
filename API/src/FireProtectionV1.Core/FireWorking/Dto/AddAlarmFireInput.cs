using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class AddAlarmFireInput
    {
        /// <summary>
        /// 部件地址
        /// </summary>
        [Required]
        public string DetectorSn { get; set; }
        /// <summary>
        /// 火警联网设施Sn
        /// </summary>
        [Required]
        public string FireAlarmDeviceSn { get; set; }
    }
}
