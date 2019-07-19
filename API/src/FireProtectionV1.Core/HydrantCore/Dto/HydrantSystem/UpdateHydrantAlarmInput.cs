using FireProtectionV1.Common.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.HydrantCore.Dto
{
    public class UpdateHydrantAlarmInput
    {
        /// <summary>
        /// 警情ID
        /// </summary>
        public int AlarmId { get; set; }
        /// <summary>
        /// 处理人员名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 是否已解决
        /// </summary>
        public HandleStatus HandleStatus { get; set; }
        /// <summary>
        /// 情况说明类型
        /// </summary>
        public ProblemType ProblemRemarkType { get; set; }
        /// <summary>
        /// 情况说明
        /// </summary>
        public string ProblemRemark { get; set; }
        /// <summary>
        /// 现场照片
        /// </summary>
        public IFormFile Picture1 { get; set; }
        /// <summary>
        /// 现场照片
        /// </summary>
        public IFormFile Picture2 { get; set; }
        /// <summary>
        /// 现场照片
        /// </summary>
        public IFormFile Picture3 { get; set; }


    }
}