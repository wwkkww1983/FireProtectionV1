using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    /// <summary>
    /// 设备设施故障
    /// </summary>
    public class Fault : EntityBase
    {
        /// <summary>
        /// 火警联网设施Id
        /// </summary>
        public int FireAlarmDeviceId { get; set; }
        /// <summary>
        /// 探测器Id
        /// </summary>
        public int FireAlarmDetectorId { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        public int FireUnitId { get; set; }
        /// <summary>
        /// 解决时间
        /// </summary>
        public DateTime SolutionTime { get; set; }
    }
}
