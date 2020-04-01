using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto.FireDevice
{
    public class GetFireAlarmDeviceDto : UpdateFireAlarmDeviceDto
    {
        /// <summary>
        /// 设备状态
        /// </summary>
        public GatewayStatus State { get; set; }
        /// <summary>
        /// 通信方式数组
        /// </summary>
        public List<string> NetComms { get; set; }
        /// <summary>
        /// 数据采集频率
        /// </summary>
        public string DataRate { get; set; }
    }
    public class UpdateFireAlarmDeviceDto: FireAlarmDeviceDto
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public int DeviceId { get; set; }
    }
    public class FireAlarmDeviceDto
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
        /// 品牌
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// 通讯方式
        /// </summary>
        public string NetComm { get; set; }
        /// <summary>
        /// 通信协议
        /// </summary>
        public string Protocol { get; set; }
        /// <summary>
        /// 联网部件数量
        /// </summary>
        public int NetDetectorNum { get; set; }
        /// <summary>
        /// 发现火警时的动作
        /// </summary>
        public List<string> EnableAlarm { get; set; }
        /// <summary>
        /// 发现故障时的动作
        /// </summary>
        public List<string> EnableFault { get; set; }
        /// <summary>
        /// 短信接收号码
        /// </summary>
        public string SMSPhones { get; set; }
    }
}
