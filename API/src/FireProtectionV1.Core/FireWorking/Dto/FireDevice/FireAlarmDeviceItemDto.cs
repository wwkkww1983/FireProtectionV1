using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto.FireDevice
{
    public class FireAlarmDeviceItemDto
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
        /// 状态
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 联网部件数量
        /// </summary>
        public int NetDetectorNum { get; set; }
        /// <summary>
        /// 故障部件数量
        /// </summary>
        public int FaultDetectorNum { get; set; }
        /// <summary>
        /// 部件故障率 %
        /// </summary>
        public string DetectorFaultRate { get; set; }
        /// <summary>
        /// 30天火警数量
        /// </summary>
        public int AlarmNum30Day { get; set; }
        /// <summary>
        /// 高频火警部件数量
        /// </summary>
        public int HighAlarmDetectorNum { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
