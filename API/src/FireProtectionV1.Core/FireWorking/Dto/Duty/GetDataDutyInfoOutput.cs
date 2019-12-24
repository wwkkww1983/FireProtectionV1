using FireProtectionV1.Common.Enum;
using FireProtectionV1.FireWorking.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetDataDutyInfoOutput : DataToDuty
    {
        /// <summary>
        /// 值班人员姓名
        /// </summary>
        public string DutyUserName { get; set; }
        /// <summary>
        /// 值班人员手机
        /// </summary>
        public string DutyUserPhone { get; set; }
        /// <summary>
        /// 问题描述文字
        /// </summary>
        public string ProblemRemark { get; set; }
        /// <summary>
        /// 问题描述语音url
        /// </summary>
        public string ProblemVoiceUrl { get; set; }
        /// <summary>
        /// 语音长度
        /// </summary>
        public int VoiceLength { get; set; }
        /// <summary>
        /// 值班记录图片路径
        /// </summary>
        public List<string> DutyPhtosPath { get; set; }
        /// <summary>
        /// 值班记录图片缩略图
        /// </summary>
        public List<string> DutyPhotosBase64 { get; set; }
        /// <summary>
        /// 现场问题图片路径
        /// </summary>
        public List<string> ProblemPhtosPath { get; set; }
        /// <summary>
        /// 现场问题图片缩略图
        /// </summary>
        public List<string> ProblemPhotosBase64 { get; set; }
    }
}
