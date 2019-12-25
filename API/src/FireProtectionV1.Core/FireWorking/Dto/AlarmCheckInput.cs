using FireProtectionV1.Common.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class AlarmCheckInput
    {
        /// <summary>
        /// 核警用户Id
        /// </summary>
        [Required]
        public int CheckUserId { get; set; }
        /// <summary>
        /// 火警联网数据Id
        /// </summary>
        [Required]
        public int FireAlarmId { get; set; }
        /// <summary>
        /// 核警状态
        /// </summary>
        [Required]
        public FireAlarmCheckState CheckState { get; set; }
        /// <summary>
        /// 核警情况描述
        /// </summary>
        public string CheckContent { get; set; }
        /// <summary>
        /// 核警语音
        /// </summary>
        public IFormFile CheckVoice { get; set; }
        /// <summary>
        /// 核警语音长度
        /// </summary>
        public int CheckVoiceLength { get; set; }
        /// <summary>
        /// 通知工作人员、微型消防站
        /// </summary>
        public List<string> NotifyList { get; set; }
    }
}
