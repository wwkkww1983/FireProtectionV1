using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto.FireDevice
{
    public class UpdateFireElectricDeviceDto: FireElectricDeviceDto
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public int DeviceId { get; set; }
    }
    public class FireElectricDeviceDto
    {
        /// <summary>
        /// 防火单位ID
        /// </summary>
        public int FireUnitId { get; set; }
        /// <summary>
        /// 设备型号
        /// </summary>
        public string DeviceType { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string DeviceSn { get; set; }
        /// <summary>
        /// 所在建筑
        /// </summary>
        public int FireUnitArchitectureId { get; set; }
        /// <summary>
        /// 所在楼层
        /// </summary>
        public int FireUnitArchitectureFloorId { get; set; }
        /// <summary>
        /// 设备地点
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 监测类型数组
        /// </summary>
        public List<string> MonitorItem { get; set; }
        /// <summary>
        /// 启用监测类型数组
        /// </summary>
        public List<string> EnableAlarm { get; set; }
        /// <summary>
        /// 通讯方式
        /// </summary>
        public string NetComm { get; set; }

        /// <summary>
        /// 通信方式数组
        /// </summary>
        public List<string> NetComms { get; set; }
        /// <summary>
        /// 数据采集频率
        /// </summary>
        public string DataRate { get; set; }
        public string State { get; set; }
        /// <summary>
        /// "单项"/"三项"
        /// </summary>
        public string PhaseType { get; set; }
        /// <summary>
        /// 电流最大值
        /// </summary>
        public int Amax { get; set; }
        /// <summary>
        /// 电流最小值
        /// </summary>
        public int Amin { get; set; }
        /// <summary>
        /// 单项L温度最小值
        /// </summary>
        public int Lmin { get; set; }
        /// <summary>
        /// 单项L温度最大值
        /// </summary>
        public int Lmax { get; set; }
        /// <summary>
        /// N温度最小值
        /// </summary>
        public int Nmin { get; set; }
        /// <summary>
        /// N温度最大值
        /// </summary>
        public int Nmax { get; set; }
        /// <summary>
        /// 三项L1温度最小值
        /// </summary>
        public int L1min { get; set; }
        /// <summary>
        /// 三项L1温度最大值
        /// </summary>
        public int L1max { get; set; }
        /// <summary>
        /// 三项L2温度最小值
        /// </summary>
        public int L2min { get; set; }
        /// <summary>
        /// 三项L2温度最大值
        /// </summary>
        public int L2max { get; set; }
        /// <summary>
        /// 三项L3温度最小值
        /// </summary>
        public int L3min { get; set; }
        /// <summary>
        /// 三项L3温度最大值
        /// </summary>
        public int L3max { get; set; }
    }
}
