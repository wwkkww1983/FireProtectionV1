using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class AlarmCheckInput
    {
        /// <summary>
        /// 警情Id
        /// </summary>
        public int CheckId { get; set; }
        /// <summary>
        /// 核实警状态(1:误报,2:测试,3:真实火警)
        /// </summary>
        public byte CheckState { get; set; }
        /// <summary>
        /// 检查情况
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 检查图片
        /// </summary>
        public IFormFile Picture1 { get; set; }
        /// <summary>
        /// 检查图片
        /// </summary>
        public IFormFile Picture2 { get; set; }
        /// <summary>
        /// 检查图片
        /// </summary>
        public IFormFile Picture3 { get; set; }
        /// <summary>
        /// 检查语音
        /// </summary>
        public IFormFile Vioce { get; set; }
        ///// <summary>
        ///// 测试图片数组
        ///// </summary>
        //public IFormFileCollection Pictures { get; set; }
    }
}
