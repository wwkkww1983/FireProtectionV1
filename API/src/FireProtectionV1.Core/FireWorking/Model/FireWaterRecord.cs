using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    public class FireWaterRecord : EntityBase
    {
        /// <summary>
        /// 消防水管网监测设备Id
        /// </summary>
        public int FireWaterDeviceId { get; set; }
        /// <summary>
        /// 数值
        /// </summary>
        public double Value { get; set; }
    }
}
