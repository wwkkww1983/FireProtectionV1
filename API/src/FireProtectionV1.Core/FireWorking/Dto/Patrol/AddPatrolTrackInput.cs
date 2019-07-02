using FireProtectionV1.Common.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class AddPatrolTrackInput
    {
        /// <summary>
        /// 巡查地点
        /// </summary>
        [Required]
        public string PatrolAddress { get; set; }       
        /// <summary>
        /// 巡查系统
        /// </summary>
        [Required]
        public List<int> SystemId { get; set; }
        /// <summary>
        /// 巡查结果
        /// </summary>
        [Required]
        public ProblemStatusType ProblemStatus { get; set; }
        /// <summary>
        /// 现场照片
        /// </summary>
        public IFormFile LivePicture1 { get; set; }
        /// <summary>
        /// 现场照片
        /// </summary>
        public IFormFile LivePicture2 { get; set; }
        /// <summary>
        /// 现场照片
        /// </summary>
        public IFormFile LivePicture3 { get; set; }
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


    }
}
