using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class RecordUnAnalogOutput
    {
        /// <summary>
        /// 部件名称
        /// </summary>
        public string Name{ get;set;}
        /// <summary>
        /// 安装地点
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 最后状态改变时间
        /// </summary>
        public string LastTimeStateChange { get; set; }
        /// <summary>
        /// 历史模拟量
        /// </summary>
        public List<UnAnalogTime> UnAnalogTimes { get; set; }
    }
    public class UnAnalogTime
    {
        /// <summary>
        /// 时间
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// 离线次数
        /// </summary>
        public int OfflineCount { get; set; }
        /// <summary>
        /// 报警次数
        /// </summary>
        public int AlarmCount { get; set; }
        /// <summary>
        /// 故障次数
        /// </summary>
        public int FaultCount { get; set; }
    }
}
