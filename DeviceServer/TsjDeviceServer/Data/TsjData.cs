using System;
using System.Collections.Generic;
using System.Text;

namespace TsjDeviceServer.Data
{
    public abstract class TsjData
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public int at { get; set; }
    }
    public abstract class TsjDetectorData : TsjData
    {
        /// <summary>
        /// 探测器标识
        /// </summary>
        public string id { get; set; }
    }
}
