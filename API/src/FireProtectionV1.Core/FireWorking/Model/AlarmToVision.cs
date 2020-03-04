using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    public class AlarmToVision : EntityBase
    {
        /// <summary>
        /// 消防分析仪设备Id
        /// </summary>
        public int VisionDeviceId { get; set; }
        /// <summary>
        /// 摄像头Id
        /// </summary>
        public int VisionDetectorId { get; set; }
        /// <summary>
        /// 报警类型
        /// </summary>
        public VisionAlarmType VisionAlarmType { get; set; }
        /// <summary>
        /// 现场照片
        /// </summary>
        public string PhotoPath { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        public int FireUnitId { get; set; }
    }
}
