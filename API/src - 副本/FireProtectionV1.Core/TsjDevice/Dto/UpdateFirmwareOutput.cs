using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.TsjDevice.Dto
{
    public class UpdateFirmwareOutput
    {
        /// <summary>
        /// 设备SN
        /// </summary>
        public string Sn { get; set; }
        /// <summary>
        /// 更新版本url
        /// </summary>
        public string Url { get; set; }
    }
}
