using FireProtectionV1.FireWorking.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class WaterAlarmListOutput
    {
        /// <summary>
        /// 接收警情时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 设备地址
        /// </summary>
        public string DeviceAddress { get; set; }
        /// <summary>
        /// 监控类型
        /// </summary>
        public MonitorType MonitorType { get; set; }
        /// <summary>
        /// 设备安装位置
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 数值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 是否已读（手机端未读的警情需加粗显示）
        /// </summary>
        public bool IsRead { get; set; }
    }
}
