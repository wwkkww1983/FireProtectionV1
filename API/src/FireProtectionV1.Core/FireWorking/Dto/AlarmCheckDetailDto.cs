using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class AlarmCheckDetailDto
    {
        /// <summary>
        /// 火警联网数据Id
        /// </summary>
        public int FireAlarmId { get; set; }
        /// <summary>
        /// 核警用户Id
        /// </summary>
        public int CheckUserId { get; set; }
        /// <summary>
        /// 核警状态(1:误报,2:测试,3:真实火警)
        /// </summary>
        public FireAlarmCheckState CheckState { get; set; }
        /// <summary>
        /// 检查情况
        /// </summary>
        public string CheckContent { get; set; }
        /// <summary>
        /// 核警语音Url
        /// </summary>
        public string CheckVoiceUrl { get; set; }
        /// <summary>
        /// 核警语音长度
        /// </summary>
        public int CheckVoiceLength { get; set; }
        /// <summary>
        /// 通知工作人员
        /// </summary>
        public bool NotifyWorker { get; set; }
        /// <summary>
        /// 通知微型消防站
        /// </summary>
        public bool NotifyMiniStation { get; set; }
    }
}
