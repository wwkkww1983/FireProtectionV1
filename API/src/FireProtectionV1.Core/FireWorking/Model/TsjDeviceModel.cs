using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    public class TsjDeviceModel : EntityBase
    {
        /// <summary>
        /// 型号
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// 设施类型
        /// </summary>
        public TsjDeviceType DeviceType { get; set; }
    }
}
