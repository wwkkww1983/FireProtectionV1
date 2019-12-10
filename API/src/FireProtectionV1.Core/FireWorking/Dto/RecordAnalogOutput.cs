using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetRecordElectricOutput
    {
        /// <summary>
        /// 监测指标
        /// </summary>
        public string MonitorItemName { get; set; }
        /// <summary>
        /// 模拟量单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 上限值
        /// </summary>
        public string Max { get; set; } 
        /// <summary>
        /// 下限值
        /// </summary>
        public string Min { get; set; }
        /// <summary>
        /// 历史模拟量
        /// </summary>
        public List<AnalogTime> AnalogTimes { get; set; }
    }
    public class RecordAnalogOutput
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
        /// 模拟量单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 历史模拟量
        /// </summary>
        public List<AnalogTime> AnalogTimes { get; set; }
    }
    public class AnalogTime
    {
        /// <summary>
        /// 模拟量时间
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// 模拟量值
        /// </summary>
        public double Value { get; set; }
    }
}
