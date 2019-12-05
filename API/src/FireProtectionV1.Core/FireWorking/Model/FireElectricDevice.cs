using FireProtectionV1.Common.DBContext;
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
        /// 网关ID
        /// </summary>
        public int GatewayId { get; set; }
        /// <summary>
        /// 设备型号
        /// </summary>
        [MaxLength(20)]
        public string DeviceType { get; set; }
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
        /// 设备地点
        /// </summary>
        [MaxLength(100)]
        public string Location { get; set; }
        /// <summary>
        /// 设备状态：良好/隐患
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 电缆温度监测
        /// </summary>
        public bool ExistTemperature { get; set; }
        /// <summary>
        /// 温度监测范围(josn SinglePhase/ThreePhase)
        /// </summary>
        public string TemperatureThreshold { get; set; }
        /// <summary>
        /// 剩余电流监测
        /// </summary>
        public bool ExistAmpere { get; set; }
        /// <summary>
        /// 电流监测阈值
        /// </summary>
        public string AmpereThreshold { get; set; }
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
        /// "单项"/"三项"
        /// </summary>
        public string PhaseType { get; set; }
        /// <summary>
        /// "单相"/"三相"配置对象json
        /// </summary>
        public string PhaseJson { get; set; }
    }
    public class SinglePhase
    {
        public int Lmin { get; set; }
        public int Lmax { get; set; }
        public int Nmin { get; set; }
        public int Nmax { get; set; }
        public int Amin { get; set; }
        public int Amax { get; set; }
    }
    public class ThreePhase
    {
        public int L1min { get; set; }
        public int L1max { get; set; }
        public int L2min { get; set; }
        public int L2max { get; set; }
        public int L3min { get; set; }
        public int L3max { get; set; }
        public int Nmin { get; set; }
        public int Nmax { get; set; }
        public int Amin { get; set; }
        public int Amax { get; set; }
    }
}
