using FireProtectionV1.Common.Enum;
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
        public string DeviceModel { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string DeviceSn { get; set; }
        /// <summary>
        /// 所在建筑Id
        /// </summary>
        public int FireUnitArchitectureId { get; set; }
        /// <summary>
        /// 所在楼层Id
        /// </summary>
        public int FireUnitArchitectureFloorId { get; set; }
        /// <summary>
        /// 设备安装地点
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 监测类型数组（剩余电流、电缆温度）
        /// </summary>
        public List<string> MonitorItemList { get; set; }
        /// <summary>
        /// 超限动作数组（终端报警、云端报警、发送开关量信号）
        /// </summary>
        public List<string> EnableAlarmList { get; set; }
        /// <summary>
        /// 通讯方式
        /// </summary>
        public string NetComm { get; set; }
        /// <summary>
        /// "单相"/"三相"
        /// </summary>
        public PhaseType PhaseType { get; set; }
        /// <summary>
        /// 电流最大值
        /// </summary>
        public int MaxAmpere { get; set; }
        /// <summary>
        /// 电流最小值
        /// </summary>
        public int MinAmpere { get; set; }
        /// <summary>
        /// 单项L温度最小值
        /// </summary>
        public int MinL { get; set; }
        /// <summary>
        /// 单项L温度最大值
        /// </summary>
        public int MaxL { get; set; }
        /// <summary>
        /// N温度最小值
        /// </summary>
        public int MinN { get; set; }
        /// <summary>
        /// N温度最大值
        /// </summary>
        public int MaxN { get; set; }
        /// <summary>
        /// 三项L1温度最小值
        /// </summary>
        public int MinL1 { get; set; }
        /// <summary>
        /// 三项L1温度最大值
        /// </summary>
        public int MaxL1 { get; set; }
        /// <summary>
        /// 三项L2温度最小值
        /// </summary>
        public int MinL2 { get; set; }
        /// <summary>
        /// 三项L2温度最大值
        /// </summary>
        public int MaxL2 { get; set; }
        /// <summary>
        /// 三项L3温度最小值
        /// </summary>
        public int MinL3 { get; set; }
        /// <summary>
        /// 三项L3温度最大值
        /// </summary>
        public int MaxL3 { get; set; }
    }
}
