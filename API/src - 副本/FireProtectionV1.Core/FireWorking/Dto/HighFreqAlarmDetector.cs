using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class HighFreqAlarmDetector
    {
        /// <summary>
        /// 报警部件名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 最近报警时间
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// 最近30天报警次数
        /// </summary>
        public int Count { get; set; }
    }
}
