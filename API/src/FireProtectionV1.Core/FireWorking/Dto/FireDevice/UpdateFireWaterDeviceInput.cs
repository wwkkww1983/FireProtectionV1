using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto.FireDevice
{
    public class UpdateFireWaterDeviceInput : AddFireWaterDeviceInput
    {
        /// <summary>
        /// 设备Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public FireWaterDeviceState State { get; set; }
    }
}
