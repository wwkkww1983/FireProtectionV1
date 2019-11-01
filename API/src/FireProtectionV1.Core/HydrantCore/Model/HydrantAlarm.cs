using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.HydrantCore.Model
{
    public class HydrantAlarm : EntityBase
    {
        /// <summary>
        /// 消火栓Id
        /// </summary>
        public int HydrantId { get; set; }
        /// <summary>
        /// 报警事件标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 是否已读
        /// </summary>
        [DefaultValue(false)]
        public bool ReadFlag { get; set; }
        /// <summary>
        /// 处理情况
        /// </summary>
        [DefaultValue(1)]
        public byte HandleStatus { get; set; }
        /// <summary>
        /// 问题描述类型(1文本类型，2语音类型)
        /// </summary>
        public byte ProblemRemarkType { get; set; }
        /// <summary>
        /// 问题描述
        /// </summary>
        [MaxLength(200)]
        public string ProblemRemark { get; set; }
        /// <summary>
        /// 语音长度
        /// </summary>
        public int VoiceLength { get; set; }
        /// <summary>
        /// 处理人姓名
        /// </summary>
        public string HandleUser { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime SoultionTime { get; set; }
    }
}
