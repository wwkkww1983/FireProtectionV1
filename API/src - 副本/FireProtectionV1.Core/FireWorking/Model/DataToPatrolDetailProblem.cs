﻿using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    /// <summary>
    /// 巡查记录详情问题表
    /// </summary>
    public class DataToPatrolDetailProblem : EntityBase
    {
        /// <summary>
        /// 巡查记录详情Id
        /// </summary>
        [Required]
        public int PatrolDetailId { get; set; }
        ///// <summary>
        ///// 问题语音
        ///// </summary>
        //[MaxLength(100)]
        //public string ProblemVoice { get; set; }
        ///// <summary>
        ///// 问题图片
        ///// </summary>
        //[MaxLength(100)]
        //public string ProblemPicture { get; set; }
        /// <summary>
        /// 问题描述
        /// </summary>
        [MaxLength(500)]
        public string ProblemRemark { get; set; }
        /// <summary>
        /// 语音长度
        /// </summary>
        public int VoiceLength { get; set; }
        /// <summary>
        /// 问题描述类型(1文本类型，2语音类型)
        /// </summary>
        [Required]
        public byte ProblemRemarkType { get; set; }
    }
}
