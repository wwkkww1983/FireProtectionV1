using FireProtectionV1.FireWorking.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto.FireDevice
{
    public class AddFireWaterDeviceInput
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
        /// 监控类型
        /// </summary>
        public MonitorType MonitorType { get; set; }
        /// <summary>
        /// 监控范围最小值
        /// </summary>
        public double MinThreshold { get; set; }
        /// <summary>
        /// 监控范围最大值
        /// </summary>
        public double MaxThreshold { get; set; }
        /// <summary>
        /// 启用云端报警
        /// </summary>
        public bool EnableCloudAlarm { get; set; }
    }
}
