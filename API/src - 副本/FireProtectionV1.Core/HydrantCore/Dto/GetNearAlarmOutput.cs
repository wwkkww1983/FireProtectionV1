using FireProtectionV1.HydrantCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.HydrantCore.Dto
{
    public class GetNearAlarmOutput: HydrantAlarm
    {
        /// <summary>
        /// 距离（米）
        /// </summary>
        public string CreationTime { get; set; }
    }
}
