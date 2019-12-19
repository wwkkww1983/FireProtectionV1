using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class AlarmRecord
    {
        /// <summary>
        /// 报警内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 报警时间
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// 报警地点
        /// </summary>
        public string Location { get; set; }
    }
}
