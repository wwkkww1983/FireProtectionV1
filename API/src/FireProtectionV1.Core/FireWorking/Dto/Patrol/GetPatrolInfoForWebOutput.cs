using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetPatrolInfoForWebOutput
    {
        /// <summary>
        /// 值班ID
        /// </summary>
        public int PatrolId { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public string CreationTime { get; set; }
        /// <summary>
        /// 值班人员
        /// </summary>
        public string PatrolUser { get; set; }
        /// <summary>
        /// 巡查方式(1.一般巡查；2.扫码巡查)
        /// </summary>
        public string PatrolType { get; set; }
        /// <summary>
        /// 有效轨迹点
        /// </summary>
        public int TrackCount { get; set; }
        /// <summary>
        /// 发现问题数量
        /// </summary>
        public int ProblemCount { get; set; }
        /// <summary>
        /// 现场解决数量
        /// </summary>
        public int ResolvedConut { get; set; }
        /// <summary>
        /// 巡查轨迹
        /// </summary>
        public List<GetPatrolTrackOutput> TrackList { get; set; }
}
}
