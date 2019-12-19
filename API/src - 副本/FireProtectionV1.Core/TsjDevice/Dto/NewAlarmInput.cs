using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.TsjDevice.Dto
{
    public class NewAlarmInput
    {
        /// <summary>
        /// 探测器标识
        /// </summary>
        public string Identify { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public string Time { get; set; }
    }
}
