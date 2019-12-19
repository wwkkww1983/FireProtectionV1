using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    public class AlarmToFire : EntityBase
    {
        /// <summary>
        /// 火警联网设施Id
        /// </summary>
        [Required]
        public int FireAlarmDeviceId { get; set; }
        /// <summary>
        /// 探测器Id
        /// </summary>
        [Required]
        public int DetectorId { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }
        /// <summary>
        /// 核警状态
        /// </summary>
        public CheckStateType CheckState { get; set; }
        /// <summary>
        /// 核警情况描述
        /// </summary>
        [MaxLength(300)]
        public string Content { get; set; }
        /// <summary>
        /// 核警语音url
        /// </summary>
        [MaxLength(100)]
        public string VioceUrl { get; set; }
        /// <summary>
        /// 核警语音长度
        /// </summary>
        public int VoiceLength { get; set; }
        /// <summary>
        /// 核警人员Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 通知工作人员
        /// </summary>
        public bool IsNotifyWorker { get; set; }
        /// <summary>
        /// 通知微型消防站
        /// </summary>
        public bool IsNotifyMiniStation { get; set; }
        /// <summary>
        /// 通知119
        /// </summary>
        //public bool Notify119 { get; set; }
    }
}
