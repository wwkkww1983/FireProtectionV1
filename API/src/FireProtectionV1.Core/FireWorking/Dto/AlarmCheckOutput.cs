using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class AlarmCheckOutput
    {
        /// <summary>
        /// 警情Id
        /// </summary>
        public int CheckId { get; set; }
        /// <summary>
        /// 发生火警时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 警情内容
        /// </summary>
        public string Alarm { get; set; }
        /// <summary>
        /// 火警地点
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 核警状态值
        /// </summary>
        public byte CheckStateValue { get; set; }
        /// <summary>
        /// 核警状态名称
        /// </summary>
        public string CheckStateName  { get; set; }
    }
}
