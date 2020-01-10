using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto.FireDevice
{
    public class GetSingleElectricDeviceDataOutput
    {
        /// <summary>
        /// 值为1表示刷新成功，可以去取DeviceData中的值去刷新表格对应行数据，否则提示“刷新数值超时，请稍后再试”
        /// </summary>
        public int Result { get; set; }
        /// <summary>
        /// 设备的当前数值
        /// </summary>
        public FireElectricDeviceItemDto DeviceData { get; set; }
    }
}
