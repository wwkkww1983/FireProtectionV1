using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class PendingFault
    {
        /// <summary>
        /// 故障事件描述
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 故障时间
        /// </summary>
        public string Time { get; set; }
    }
}
