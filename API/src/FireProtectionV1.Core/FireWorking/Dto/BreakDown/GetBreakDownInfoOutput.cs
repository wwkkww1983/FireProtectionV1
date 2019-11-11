using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetBreakDownInfoOutput
    {
        /// <summary>
        /// 处理状态（1待处理,2处理中,3已解决,4自行处理,5维保叫修处理中,6维保叫修已处理）
        /// </summary>
        public byte HandleStatus { get; set; }
        /// <summary>
        /// 故障来源（1.值班 2.巡查 3.物联终端）
        /// </summary>
        public byte Source { get; set; }
        /// <summary>
        /// 发现人员
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 发现人员联系方式
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 发现时间
        /// </summary>
        public string CreationTime { get; set; }
        /// <summary>
        /// 问题描述类型(1.文本  2.语音)
        /// </summary>
        public byte ProblemRemakeType { get; set; }
        /// <summary>
        /// 问题描述（如果备注类型为语音则存的语音路径）
        /// </summary>
        public string RemakeText { get; set; }
        /// <summary>
        /// 语音长度
        /// </summary>
        public int VoiceLength { get; set; }
        /// <summary>
        /// 巡查图片
        /// </summary>
        public List<string> PatrolPhotosPath { get; set; }
        public List<string> PhotosBase64 { get; set; }
        /// <summary>
        /// 问题处理途径（1.自行处理 2.维保叫修）
        /// </summary>
        public byte SolutionWay { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 解决时间
        /// </summary>
        public string SolutionTime { get; set; }
    }
}