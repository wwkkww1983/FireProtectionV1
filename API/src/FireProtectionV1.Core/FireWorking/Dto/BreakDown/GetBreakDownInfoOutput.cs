using FireProtectionV1.Common.Enum;
using FireProtectionV1.FireWorking.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetBreakDownInfoOutput : BreakDown
    {
        /// <summary>
        /// 发现人员姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 发现人员联系方式
        /// </summary>
        public string UserPhone { get; set; }
        /// <summary>
        /// 巡查图片路径
        /// </summary>
        public List<string> PatrolPhotosPath { get; set; }
        /// <summary>
        /// 图片缩略图
        /// </summary>
        public List<string> PatrolPhotosBase64 { get; set; }
    }
}