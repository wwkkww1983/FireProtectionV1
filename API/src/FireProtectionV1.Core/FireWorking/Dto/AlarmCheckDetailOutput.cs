using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class AlarmCheckDetailOutput
    {
        /// <summary>
        /// 警情Id
        /// </summary>
        public int CheckId { get; set; }
        /// <summary>
        /// 发生火警时间
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// 警情内容
        /// </summary>
        public string Alarm { get; set; }
        /// <summary>
        /// 火警地点
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 核警状态值
        /// </summary>
        public byte CheckStateValue { get; set; }
        /// <summary>
        /// 核警状态名称
        /// </summary>
        public string CheckStateName { get; set; }
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
        /// 工作人员姓名
        /// </summary>
        public string UserName { get; set; }
        /// 工作人员电话
        /// </summary>
        public string UserPhone { get; set; }
        /// <summary>
        /// 核警处理时间
        /// </summary>
        public string CheckTime { get; set; }
    }
}
