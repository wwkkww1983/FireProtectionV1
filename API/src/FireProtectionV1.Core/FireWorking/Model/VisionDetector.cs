using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    public class VisionDetector : EntityBase
    {
        /// <summary>
        /// 消防分析仪设备Id
        /// </summary>
        public int VisionDeviceId { get; set; }
        /// <summary>
        /// 摄像头对应的通道编号
        /// </summary>
        public int Sn { get; set; }
        /// <summary>
        /// 摄像头监控的现实地址
        /// </summary>
        public string Location { get; set; }
    }
}
