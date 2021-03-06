﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class AlarmCheckInput
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required]
        public int UserId { get; set; }
        /// <summary>
        /// 警情Id
        /// </summary>
        [Required]
        public int CheckId { get; set; }
        /// <summary>
        /// 核实警状态(1:误报,2:测试,3:真实火警)
        /// </summary>
        [Required]
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
        public IFormFile Voice { get; set; }
        /// <summary>
        /// 语音长度
        /// </summary>
        public UInt16 VoiceLength { get; set; }
        /// <summary>
        /// 通知工作人员
        /// </summary>
        public bool NotifyWorker { get; set; }
        /// <summary>
        /// 通知微型消防站
        /// </summary>
        public bool NotifyMiniaturefire { get; set; }
        /// <summary>
        /// 通知119
        /// </summary>
        public bool Notify119 { get; set; }
    }
}
