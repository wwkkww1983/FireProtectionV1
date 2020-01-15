using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    /// <summary>
    /// 电气火灾设施
    /// </summary>
    public class FireElectricDevice : EntityBase
    {
        /// <summary>
        /// 防火单位ID
        /// </summary>
        public int FireUnitId { get; set; }
        /// <summary>
        /// 设备型号
        /// </summary>
        [MaxLength(20)]
        public string DeviceModel { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        [MaxLength(20)]
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
        /// 通讯方式
        /// </summary>
        public string NetComm { get; set; }
        /// <summary>
        /// 数据传送频率
        /// </summary>
        public string DataRate { get; set; } = "2小时";
        /// <summary>
        /// 设备安装地点
        /// </summary>
        [MaxLength(100)]
        public string Location { get; set; }
        /// <summary>
        /// 设备状态：离线/良好/隐患/超限
        /// </summary>
        public FireElectricDeviceState State { get; set; }
        /// <summary>
        /// 剩余电流监测
        /// </summary>
        public bool ExistAmpere { get; set; }
        /// <summary>
        /// 电缆温度监测
        /// </summary>
        public bool ExistTemperature { get; set; }
        /// <summary>
        /// 启用终端报警
        /// </summary>
        public bool EnableEndAlarm { get; set; }
        /// <summary>
        /// 启用云端报警
        /// </summary>
        public bool EnableCloudAlarm { get; set; }
        /// <summary>
        /// 发送开关量信号
        /// </summary>
        public bool EnableAlarmSwitch { get; set; }
        /// <summary>
        /// 剩余电流下限
        /// </summary>
        public int MinAmpere { get; set; }
        /// <summary>
        /// 剩余电流上限
        /// </summary>
        public int MaxAmpere { get; set; }
        /// <summary>
        /// 电缆温度监测：单相/三相
        /// </summary>
        public PhaseType PhaseType { get; set; }
        /// <summary>
        /// L温度下限
        /// </summary>
        public int MinL { get; set; }
        /// <summary>
        /// L温度上限
        /// </summary>
        public int MaxL { get; set; }
        /// <summary>
        /// N温度下限
        /// </summary>
        public int MinN { get; set; }
        /// <summary>
        /// N温度上限
        /// </summary>
        public int MaxN { get; set; }
        /// <summary>
        /// L1温度下限
        /// </summary>
        public int MinL1 { get; set; }
        /// <summary>
        /// L1温度上限
        /// </summary>
        public int MaxL1 { get; set; }
        /// <summary>
        /// L2温度下限
        /// </summary>
        public int MinL2 { get; set; }
        /// <summary>
        /// L2温度上限
        /// </summary>
        public int MaxL2 { get; set; }
        /// <summary>
        /// L3温度下限
        /// </summary>
        public int MinL3 { get; set; }
        /// <summary>
        /// L3温度上限
        /// </summary>
        public int MaxL3 { get; set; }
        /// <summary>
        /// 启用发送短信
        /// </summary>
        public bool EnableSMS { get;  set; }
        /// <summary>
        /// 短信接收号码数组“,”分割,临时最多100个号码
        /// </summary>
        [MaxLength(1200)]
        public string SMSPhones { get; set; }
    }
}
