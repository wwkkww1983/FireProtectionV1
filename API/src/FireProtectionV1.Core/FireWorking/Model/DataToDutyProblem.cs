using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    /// <summary>
    /// 值班记录问题
    /// </summary>
    public class DataToDutyProblem : EntityBase
    {
        /// <summary>
        /// 值班记录Id
        /// </summary>
        [Required]
        public int DutyId { get; set; }
        /// <summary>
        /// 问题语音
        /// </summary>
        [MaxLength(100)]
        public string ProblemVoice { get; set; }
        /// <summary>
        /// 问题图片
        /// </summary>
        [MaxLength(100)]
        public string ProblemPicture { get; set; }
        /// <summary>
        /// 问题描述
        /// </summary>
        [MaxLength(200)]
        public string ProblemRemark { get; set; }
        /// <summary>
        /// 问题描述类型(1文本类型，2语音类型)
        /// </summary>
        [Required]
        public int ProblemRemarkType { get; set; }
    }
}
