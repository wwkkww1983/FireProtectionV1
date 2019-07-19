using FireProtectionV1.Common.Enum;
using FireProtectionV1.HydrantCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.HydrantCore.Dto
{
    public class GetHydrantAlarmOutput
    {
        /// <summary>
        /// 警情ID
        /// </summary>
        public int AlarmId { get; set; }
        /// <summary>
        /// 报警时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 消火栓地点
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 报警事件标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 是否已读
        /// </summary>
        public bool ReadFlag { get; set; }
    }
    public class GetHydrantAlarmPagingOutput
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 页面数据
        /// </summary>
        public List<GetHydrantAlarmOutput> AlarmList { get; set; }
    }
}

