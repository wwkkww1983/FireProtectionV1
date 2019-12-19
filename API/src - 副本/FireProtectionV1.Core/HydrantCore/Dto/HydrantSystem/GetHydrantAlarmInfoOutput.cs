using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.HydrantCore.Dto
{
    public class GetHydrantAlarmInfoOutput
    {
        /// <summary>
        /// 警情ID
        /// </summary>
        public int AlarmID { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string Sn { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Adress { get; set; }
        /// <summary>
        /// 警情
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 处理状态
        /// </summary>
        public HandleStatus HandleStatus { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public string CreationTime { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public string SolutionTime { get; set; }
        /// <summary>
        /// 处理人员
        /// </summary>
        public string HandleUser { get; set; }
        /// <summary>
        /// 处理人员电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 问题描述类型
        /// </summary>
        public ProblemType ProblemRemarkType { get; set; }
        /// <summary>
        /// 问题描述(如果类型为文本则存放的文本，类型为语音则存放的语音路径)
        /// </summary>
        public string ProblemRemark { get; set; }
        /// <summary>
        /// 语音长度
        /// </summary>
        public int VoiceLength { get; set; }
        /// <summary>
        /// 现场照片路径
        /// </summary>
        public List<string> PhtosPath { get; set; }
        public List<string> PhotosBase64 { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public decimal Lng { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public decimal Lat { get; set; }
    }
}
