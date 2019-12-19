using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    /// <summary>
    /// 火警联网设施
    /// </summary>
    public class FireAlarmDevice: EntityBase
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
        /// 通讯方式
        /// </summary>
        public string NetComm { get; set; }
        /// <summary>
        /// 所在建筑Id
        /// </summary>
        public int FireUnitArchitectureId { get; set; }
        /// <summary>
        /// 通信协议
        /// </summary>
        public string Protocol { get; set; }
        /// <summary>
        /// 消防主机品牌
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// 联网部件数量
        /// </summary>
        public int NetDetectorNum { get; set; }
        /// <summary>
        /// 启用火警发送云端报警
        /// </summary>
        public bool EnableAlarmCloud { get; set; }
        /// <summary>
        /// 启用火警发送开关量信号
        /// </summary>
        public bool EnableAlarmSwitch { get; set; }
        /// <summary>
        /// 启用故障发送云端报警
        /// </summary>
        public bool EnableFaultCloud { get; set; }
        /// <summary>
        /// 启用故障发送开关量信号
        /// </summary>
        public bool EnableFaultSwitch { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public GatewayStatus State { get; set; }
    }
}
