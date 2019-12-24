using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    /// <summary>
    /// 消防管网
    /// </summary>
    public class FireWaterDevice : EntityBase
    {
        /// <summary>
        /// 防火单位Id
        /// </summary>
        public int FireUnitId { get; set; }
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
        /// 网关通讯方式
        /// </summary>
        public string Gateway_NetComm { get; set; }
        /// <summary>
        /// 网关数据采集频率
        /// </summary>
        public string Gateway_DataRate { get; set; }
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
        /// 启用云端报警
        /// </summary>
        public bool EnableCloudAlarm { get; set; }
        /// <summary>
        /// 当前数值
        /// </summary>
        public double CurrentValue { get; set; }
    }

    [Export("监控类型")]
    public enum MonitorType
    {
        /// <summary>
        /// 液位
        /// </summary>
        [Description("液位")]
        Height = 1,
        /// <summary>
        /// 水压
        /// </summary>
        [Description("水压")]
        Press = 2
    }

    public class HeightPhase
    {
        public float MinHeight { get; set; }
        public float MaxHeight { get; set; }
    }
    public class PressPhase
    {
        public float MinPress { get; set; }
        public float MaxPress { get; set; }
    }
}
