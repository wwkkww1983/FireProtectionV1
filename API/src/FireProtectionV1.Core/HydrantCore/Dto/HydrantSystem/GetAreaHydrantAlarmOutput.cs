using FireProtectionV1.Common.Enum;
using FireProtectionV1.HydrantCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.HydrantCore.Dto
{
    public class GetAreaHydrantAlarm
    {
        /// <summary>
        /// 警情ID
        /// </summary>
        public int AlarmId { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 报警时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 报警事件标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public HandleStatus HandleStatus { get; set; }
    }
    public class GetAreaHydrantAlarmOutput
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 页面数据
        /// </summary>
        public List<GetAreaHydrantAlarm> AlarmList { get; set; }
    }
}

