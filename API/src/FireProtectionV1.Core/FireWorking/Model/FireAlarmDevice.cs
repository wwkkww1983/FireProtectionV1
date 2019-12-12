using FireProtectionV1.Common.DBContext;
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
        /// 网关ID
        /// </summary>
        public int GatewayId { get; set; }
        /// <summary>
        /// 设备型号
        /// </summary>
        public string DeviceType { get; set; }
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
        /// 品牌
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// 通信协议
        /// </summary>
        public string Protocol { get; set; }
        /// <summary>
        /// 联网部件数量
        /// </summary>
        public int NetDetectorNum { get; set; }
        /// <summary>
        /// 启用发现火警
        /// </summary>
        public bool EnableAlarm { get; set; }
        /// <summary>
        /// 启用火警发送开关量信号
        /// </summary>
        public bool EnableAlarmSwitch { get; set; }
        /// <summary>
        /// 启用发现故障
        /// </summary>
        public bool EnableFault { get; set; }
        /// <summary>
        /// 启用故障发送开关量信号
        /// </summary>
        public bool EnableFaultSwitch { get; set; }
    }
}
