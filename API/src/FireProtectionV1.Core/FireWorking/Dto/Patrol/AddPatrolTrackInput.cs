using FireProtectionV1.Common.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class AddPatrolTrackAllInput
    {
        /// <summary>
        /// 巡查ID
        /// </summary>
        [Required]
        public int PatrolId { get; set; }
        /// <summary>
        /// 消防设施编号
        /// </summary>
        public List<string> DeviceSn { get; set; }
        /// <summary>
        /// 巡查地点
        /// </summary>
        public List<string> PatrolAddress { get; set; }
        /// <summary>
        /// 巡查系统(ID用逗号隔开)
        /// </summary>
        public List<string> SystemIdList { get; set; }
        /// <summary>
        /// 巡查结果
        /// </summary>
        public List<DutyOrPatrolStatus> ProblemStatus { get; set; }
        /// <summary>
        /// 现场照片
        /// </summary>
        public List<IFormFile> LivePicture1 { get; set; }
        /// <summary>
        /// 现场照片
        /// </summary>
        public List<IFormFile> LivePicture2 { get; set; }
        /// <summary>
        /// 现场照片
        /// </summary>
        public List<IFormFile> LivePicture3 { get; set; }
        /// <summary>
        /// 问题描述类型
        /// </summary>
        public List<ProblemType> ProblemRemarkType { get; set; }
        /// <summary>
        /// 问题描述
        /// </summary>
        public List<string> ProblemRemark { get; set; }
        /// <summary>
        /// 问题描述语音
        /// </summary>
        public List<IFormFile> RemarkVioce { get; set; }
        /// <summary>
        /// 语音长度
        /// </summary>
        public List<UInt16> VoiceLength { get; set; }

    }
    public class AddPatrolTrackOne
    {
        /// <summary>
        /// 消防设施编号
        /// </summary>
        [MaxLength(20)]
        public string DeviceSn { get; set; }
        /// <summary>
        /// 巡查地点
        /// </summary>
        [Required]
        public string PatrolAddress { get; set; }
        /// <summary>
        /// 巡查系统(ID用逗号隔开)
        /// </summary>
        public string SystemIdList { get; set; }
        /// <summary>
        /// 巡查结果
        /// </summary>
        [Required]
        public DutyOrPatrolStatus ProblemStatus { get; set; }
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
        /// <summary>
        /// 语音长度
        /// </summary>
        public UInt16 VoiceLength { get; set; }
    }
    public class AddPatrolTrackInput
    {
        /// <summary>
        /// 巡查ID
        /// </summary>
        [Required]
        public int PatrolId { get; set; }
        /// <summary>
        /// 消防设施编号
        /// </summary>
        [MaxLength(20)]
        public string DeviceSn { get; set; }
        /// <summary>
        /// 巡查地点
        /// </summary>
        [Required]
        public string PatrolAddress { get; set; }       
        /// <summary>
        /// 巡查系统(ID用逗号隔开)
        /// </summary>
        public string SystemIdList { get; set; }
        /// <summary>
        /// 巡查结果
        /// </summary>
        [Required]
        public DutyOrPatrolStatus ProblemStatus { get; set; }
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
        /// <summary>
        /// 语音长度
        /// </summary>
        public UInt16 VoiceLength { get; set; }

    }
}
