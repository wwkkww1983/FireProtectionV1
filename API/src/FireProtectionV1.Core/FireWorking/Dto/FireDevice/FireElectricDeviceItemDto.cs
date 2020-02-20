using Abp.Application.Services.Dto;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto.FireDevice
{
    public class FireElectricDeviceItemDto
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public int DeviceId { get; set; }
        /// <summary>
        /// 设备SN
        /// </summary>
        public string DeviceSn { get; set; }
        /// <summary>
        /// 所在建筑Id
        /// </summary>
        public int FireUnitArchitectureId { get; set; }
        /// <summary>
        /// 所在建筑
        /// </summary>
        public string FireUnitArchitectureName { get; set; }
        /// <summary>
        /// 所在楼层Id
        /// </summary>
        public int FireUnitArchitectureFloorId { get; set; }
        /// <summary>
        /// 所在楼层
        /// </summary>
        public string FireUnitArchitectureFloorName { get; set; }
        /// <summary>
        /// 具体位置
        /// </summary>
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
        /// "单项"/"三项"
        /// </summary>
        public PhaseType PhaseType { get; set; }
        /// <summary>
        /// 当前L数值
        /// </summary>
        public string L { get; set; }
        /// <summary>
        /// 当前N数值
        /// </summary>
        public string N { get; set; }
        /// <summary>
        /// 当前电流数值
        /// </summary>
        public string A { get; set; }
        /// <summary>
        /// 当前L1数值
        /// </summary>
        public string L1 { get; set; }
        /// <summary>
        /// 当前L2数值
        /// </summary>
        public string L2 { get; set; }
        /// <summary>
        /// 当前L3数值
        /// </summary>
        public string L3 { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }

    public class FireElectricDevice_DeptDto : FireElectricDeviceItemDto
    {
        /// <summary>
        /// 防火单位名称
        /// </summary>
        public string FireUnitName { get; set; }
    }

    public class FireElectricDevice_EngineerDto
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public int DeviceId { get; set; }
        /// <summary>
        /// 设备SN
        /// </summary>
        public string DeviceSn { get; set; }
        /// <summary>
        /// 防火单位名称
        /// </summary>
        public string FireUnitName { get; set; }
        /// <summary>
        /// 防火单位区域
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// 防火单位联系人姓名
        /// </summary>
        public string ContractName { get; set; }
        /// <summary>
        /// 防火单位联系人手机
        /// </summary>
        public string ContractPhone { get; set; }
        /// <summary>
        /// 施工人员姓名
        /// </summary>
        public string EngineerName { get; set; }
        /// <summary>
        /// 施工人员手机
        /// </summary>
        public string EngineerPhone { get; set; }
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
        /// "单项"/"三项"
        /// </summary>
        public PhaseType PhaseType { get; set; }
        /// <summary>
        /// 当前L数值
        /// </summary>
        public string L { get; set; }
        /// <summary>
        /// 当前N数值
        /// </summary>
        public string N { get; set; }
        /// <summary>
        /// 当前电流数值
        /// </summary>
        public string A { get; set; }
        /// <summary>
        /// 当前L1数值
        /// </summary>
        public string L1 { get; set; }
        /// <summary>
        /// 当前L2数值
        /// </summary>
        public string L2 { get; set; }
        /// <summary>
        /// 当前L3数值
        /// </summary>
        public string L3 { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
