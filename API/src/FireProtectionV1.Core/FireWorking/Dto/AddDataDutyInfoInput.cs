using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class AddDataDutyInfoInput
    {
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
        public byte DutyStatus { get; set; }
        /// <summary>
        /// 值班记录图片路径
        /// </summary>
        public List<string> DutyPhtosPath { get; set; }
        /// <summary>
        /// 问题描述类型
        /// </summary>
        public ProblemType ProblemRemarkType { get; set; }
        /// <summary>
        /// 问题描述(如果类型为文本则存放的文本，类型为语音则存放的语音路径)
        /// </summary>
        public string ProblemRemark { get; set; }
        /// <summary>
        /// 现场问题图片路径
        /// </summary>
        public List<string> ProblemPhtosPath { get; set; }
    }
}
