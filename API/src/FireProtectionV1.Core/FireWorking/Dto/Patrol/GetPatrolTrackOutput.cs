using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetPatrolTrackOutput
    {
        /// <summary>
        /// 巡查ID
        /// </summary>
        public int PatrolId { get; set; }
        /// <summary>
        /// 轨迹ID
        /// </summary>
        public int TrackId { get; set; }
        /// <summary>
        /// 巡查模式
        /// </summary>
        public PatrolType PatrolType { get; set; }
        /// <summary>
        /// 巡查时间
        /// </summary>
        public string CreationTime { get; set; }
        /// <summary>
        /// 记录状态
        /// </summary>
        public DutyOrPatrolStatus PatrolStatus { get; set; }
        /// <summary>
        /// 巡查系统名称
        /// </summary>
        public string FireSystemName { get; set; }
        /// <summary>
        /// 巡查系统名称
        /// </summary>
        public List<string> FireSystemNames { get; set; }
        /// <summary>
        /// 巡查系统数量
        /// </summary>
        public int FireSystemCount { get; set; }
        /// <summary>
        /// 巡查地址
        /// </summary>
        public string PatrolAddress { get; set; }
        /// <summary>
        /// 备注类型(1.文本  2.语音)
        /// </summary>
        public int ProblemRemakeType { get; set; }
        /// <summary>
        /// 备注文字（如果备注类型为语音则存的语音路径）
        /// </summary>
        public string RemakeText { get; set; }
        /// <summary>
        /// 语音长度
        /// </summary>
        public int VoiceLength { get; set; }
        /// <summary>
        /// 巡查图片路径
        /// </summary>
        public List<string> PatrolPhotosPath { get; set; }
        /// <summary>
        /// 巡查图片缩略图
        /// </summary>
        public List<string> PhotosBase64 { get; set; }
    }
}
