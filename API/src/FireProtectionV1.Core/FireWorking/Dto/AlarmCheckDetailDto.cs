using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class AlarmCheckDetailDto
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 警情Id
        /// </summary>
        public int CheckId { get; set; }
        /// <summary>
        /// 核警状态(1:误报,2:测试,3:真实火警)
        /// </summary>
        public byte CheckState { get; set; }
        /// <summary>
        /// 检查情况
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 检查图片1
        /// </summary>
        public string PictureUrl_1 { get; set; }
        /// <summary>
        /// 检查图片2
        /// </summary>
        public string PictureUrl_2 { get; set; }
        /// <summary>
        /// 检查图片3
        /// </summary>
        public string PictureUrl_3 { get; set; }
        /// <summary>
        /// 检查语音
        /// </summary>
        public string VioceUrl { get; set; }
        /// <summary>
        /// 语音长度
        /// </summary>
        public int VoiceLength { get; set; }
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
