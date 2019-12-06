using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto.FireDevice
{
    public class GetFireOrtherDeviceExpireOutput
    {
        /// <summary>
        /// 已过期数量
        /// </summary>
        public int ExpireNum { get; set; }
        /// <summary>
        /// 即将过期数量
        /// </summary>
        public int WillExpireNum { get; set; }
    }
}
