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
        public int FireAlarmDetectorId { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }
        /// <summary>
        /// 核警状态
        /// </summary>
        public FireAlarmCheckState CheckState { get; set; }
        /// <summary>
        /// 核警情况描述
        /// </summary>
        [MaxLength(300)]
        public string CheckContent { get; set; }
        /// <summary>
        /// 核警语音url
        /// </summary>
        [MaxLength(100)]
        public string CheckVoiceUrl { get; set; }
        /// <summary>
        /// 核警语音长度
        /// </summary>
        public int CheckVoiceLength { get; set; }
        /// <summary>
        /// 核警人员Id
        /// </summary>
        public int CheckUserId { get; set; }
        /// <summary>
        /// 核警时间
        /// </summary>
        public DateTime? CheckTime { get; set; }
        /// <summary>
        /// 通知工作人员
        /// </summary>
        public bool NotifyWorker { get; set; }
        /// <summary>
        /// 通知微型消防站
        /// </summary>
        public bool NotifyMiniStation { get; set; }
        /// <summary>
        /// 是否已读取（用于手机端显示未读的报警数据）
        /// </summary>
        public bool IsRead { get; set; } = false;
    }
}
