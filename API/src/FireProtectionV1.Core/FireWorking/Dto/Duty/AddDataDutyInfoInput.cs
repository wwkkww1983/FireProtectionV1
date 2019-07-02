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
        /// 防火单位用户Id
        /// </summary>
        [Required]
        public int FireUnitUserId { get; set; }
        /// <summary>
        /// 值班记录描述
        /// </summary>
        public string DutyRemark { get; set; }
        /// <summary>
        /// 记录状态（1、正常；2：绿色故障；3：橙色故障）
        /// </summary>
        [Required]
        public ProblemStatusType DutyStatus { get; set; }
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
        /// 问题描述类型
        /// </summary>
        public ProblemType ProblemRemarkType { get; set; }
        /// <summary>
        /// 问题描述
        /// </summary>
        public string ProblemRemark { get; set; }
        /// <summary>
        /// 问题描述语音
        /// </summary>
        public IFormFile RemarkVioce { get; set; }
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
