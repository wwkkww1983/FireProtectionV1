﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TsjDeviceServer.Data
{
    public class DataMonitor: TsjDetectorData
    {
        /// <summary>
        /// 模拟量
        /// </summary>
        public string value { get; set; }
    }
}
