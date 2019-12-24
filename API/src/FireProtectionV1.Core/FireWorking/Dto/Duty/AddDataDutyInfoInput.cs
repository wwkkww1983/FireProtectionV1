using FireProtectionV1.Common.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class AddDataDutyInfoInput
    {
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }
        /// <summary>
        /// 值班用户Id
        /// </summary>
        [Required]
        public int UserId { get; set; }
        /// <summary>
        /// 记录状态（1、正常；2：绿色故障；3：橙色故障）
        /// </summary>
        [Required]
        public DutyOrPatrolStatus Status { get; set; }
        /// <summary>
        /// 值班记录图片
        /// </summary>
        public IFormFile DutyPicture1 { get; set; }
        /// <summary>
        /// 值班记录图片
        /// </summary>
        public IFormFile DutyPicture2 { get; set; }
        /// <summary>
        /// 值班记录图片
        /// </summary>
        public IFormFile DutyPicture3 { get; set; }
        /// <summary>
        /// 问题文字描述
        /// </summary>
        public string ProblemRemark { get; set; }
        /// <summary>
        /// 问题描述语音
        /// </summary>
        public IFormFile ProblemVoice { get; set; }
        /// <summary>
        /// 语音长度
        /// </summary>
        public int VoiceLength { get; set; }
        /// <summary>
        /// 现场问题图片
        /// </summary>
        public IFormFile ProblemPicture1 { get; set; }
        /// <summary>
        /// 现场问题图片
        /// </summary>
        public IFormFile ProblemPicture2 { get; set; }
        /// <summary>
        /// 现场问题图片
        /// </summary>
        public IFormFile ProblemPicture3 { get; set; }
    }
}
