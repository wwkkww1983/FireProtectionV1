using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.TsjDevice.Dto
{
    public class NewOverflowInput
    {
        /// <summary>
        /// 探测器标识
        /// </summary>
        public string Identify { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// 模拟量值
        /// </summary>
        public string Value { get; set; }
    }
}
