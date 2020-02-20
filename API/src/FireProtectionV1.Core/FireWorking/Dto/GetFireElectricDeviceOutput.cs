using FireProtectionV1.Common.Enum;
using FireProtectionV1.FireWorking.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetFireElectricDeviceOutput : FireElectricDevice
    {
        /// <summary>
        /// 监测类型数组（剩余电流、电缆温度）
        /// </summary>
        public List<string> MonitorItemList { get; set; }
        /// <summary>
        /// 超限动作数组（终端报警、云端报警、发送开关量信号等）
        /// </summary>
        public List<string> EnableAlarmList { get; set; }
    }
    public class GetFireElectricDeviceParaOutput : FireElectricDeviceParaDto
    {
        /// <summary>
        /// 设施Id
        /// </summary>
        public int DeviceId { get; set; }
        /// <summary>
        /// 设施编号
        /// </summary>
        public string DeviceSn { get; set; }
        /// <summary>
        /// 防火单位名称
        /// </summary>
        public string FireUnitName { get; set; }
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string ContractName { get; set; }
        /// <summary>
        /// 联系人手机
        /// </summary>
        public string ContractPhone { get; set; }
        /// <summary>
        /// 超限动作数组（终端报警、云端报警、发送开关量信号等）
        /// </summary>
        public List<string> EnableAlarmList { get; set; }
    }
    public class UpdateFireElectricDeviceParaInput : FireElectricDeviceParaDto
    {
        /// <summary>
        /// 设施Id
        /// </summary>
        public int DeviceId { get; set; }
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string ContractName { get; set; }
        /// <summary>
        /// 联系人手机
        /// </summary>
        public string ContractPhone { get; set; }
        /// <summary>
        /// 超限动作数组（终端报警、云端报警、发送开关量信号等）
        /// </summary>
        public List<string> EnableAlarmList { get; set; }
    }
    public class FireElectricDeviceParaDto
    {
        /// <summary>
        /// 启用终端报警
        /// </summary>
        public bool EnableEndAlarm { get; set; }
        /// <summary>
        /// 启用云端报警
        /// </summary>
        public bool EnableCloudAlarm { get; set; }
        /// <summary>
        /// 发送开关量信号(自动断电)
        /// </summary>
        public bool EnableAlarmSwitch { get; set; }
        /// <summary>
        /// 剩余电流上限
        /// </summary>
        public int MaxAmpere { get; set; }
        /// <summary>
        /// 电缆温度监测：单相/三相
        /// </summary>
        public PhaseType PhaseType { get; set; }
        /// <summary>
        /// L温度上限
        /// </summary>
        public int MaxL { get; set; }
        /// <summary>
        /// N温度上限
        /// </summary>
        public int MaxN { get; set; }
        /// <summary>
        /// L1温度上限
        /// </summary>
        public int MaxL1 { get; set; }
        /// <summary>
        /// L2温度上限
        /// </summary>
        public int MaxL2 { get; set; }
        /// <summary>
        /// L3温度上限
        /// </summary>
        public int MaxL3 { get; set; }
        /// <summary>
        /// 启用发送短信
        /// </summary>
        public bool EnableSMS { get; set; }
        /// <summary>
        /// 短信接收号码，多个号码以英文“,”分割
        /// </summary>
        public string SMSPhones { get; set; }
    }
}
