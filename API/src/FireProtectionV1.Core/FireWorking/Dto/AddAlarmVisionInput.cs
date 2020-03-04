using FireProtectionV1.Common.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class AddAlarmVisionInput
    {
        /// <summary>
        /// 消防分析仪设备Sn号
        /// </summary>
        public string VisionDeviceSn { get; set; }
        /// <summary>
        /// 通道Sn号
        /// </summary>
        public int VisionDetectorSn { get; set; }
        /// <summary>
        /// 报警类型
        /// </summary>
        public VisionAlarmType VisionAlarmType { get; set; }
        /// <summary>
        /// 现场图片
        /// </summary>
        public IFormFile AlarmPicture { get; set; }
    }
}
