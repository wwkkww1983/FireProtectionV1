using FireProtectionV1.Common.Enum;
using FireProtectionV1.FireWorking.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto.FireDevice
{
    public class GetFireWaterDeviceListOutput
    {
        /// <summary>
        /// 设备Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 设备地址
        /// </summary>
        public string DeviceAddress { get; set; }
        /// <summary>
        /// 设备安装位置
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 网关型号
        /// </summary>
        public string Gateway_Model { get; set; }
        /// <summary>
        /// 网关编号
        /// </summary>
        public string Gateway_Sn { get; set; }
        /// <summary>
        /// 网关安装位置
        /// </summary>
        public string Gateway_Location { get; set; }
        /// <summary>
        /// 设备状态：良好/超限/离线
        /// </summary>
        public FireWaterDeviceState State { get; set; }
        /// <summary>
        /// 监控类型
        /// </summary>
        public MonitorType MonitorType { get; set; }
        /// <summary>
        /// 最小值
        /// </summary>
        public double MinThreshold { get; set; }
        /// <summary>
        /// 最大值
        /// </summary>
        public double MaxThreshold { get; set; }
        /// <summary>
        /// 当前数值
        /// </summary>
        public string CurrentValue { get; set; }
    }

    public class GetFireWaterDeviceList_DeptOutput : GetFireWaterDeviceListOutput
    {
        /// <summary>
        /// 防火单位名称
        /// </summary>
        public string FireUnitName { get; set; }
    }
}
