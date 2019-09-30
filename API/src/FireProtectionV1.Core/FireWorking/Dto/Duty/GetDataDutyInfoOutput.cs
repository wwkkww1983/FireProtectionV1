using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetDataDutyInfoOutput
    {
        /// <summary>
        /// 值班ID
        /// </summary>
        public int DutyId { get; set; }
        /// <summary>
        /// 值班人员
        /// </summary>
        public string DutyUser { get; set; }
        /// <summary>
        /// 记录状态
        /// </summary>
        public ProblemStatusType DutyStatus { get; set; }
        /// <summary>
        /// 值班记录描述
        /// </summary>
        public string DutyRemark { get; set; }
        /// <summary>
        /// 值班记录图片路径
        /// </summary>
        public List<string> DutyPhtosPath { get; set; }
        public List<string> PhotosBase64Duty { get; set; }
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
        /// <summary>
        /// 现场问题图片缩略图
        /// </summary>
        public List<string> PhotosBase64 { get; set; }
    }
}
