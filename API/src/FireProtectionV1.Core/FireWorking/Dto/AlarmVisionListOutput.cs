using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class AlarmVisionListOutput
    {
        /// <summary>
        /// 报警数据Id
        /// </summary>
        public int VisionAlarmId { get; set; }
        /// <summary>
        /// 报警时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 报警类型
        /// </summary>
        public VisionAlarmType VisionAlarmType { get; set; }
        /// <summary>
        /// 视觉设备（编号+通道号）
        /// </summary>
        public string VisionDevice { get; set; }
        /// <summary>
        /// 监控地点
        /// </summary>
        public string Location { get; set; }
    }
}
