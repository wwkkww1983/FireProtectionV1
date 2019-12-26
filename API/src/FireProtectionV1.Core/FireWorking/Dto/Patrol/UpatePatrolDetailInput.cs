using FireProtectionV1.Common.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto.Patrol
{
    public class UpatePatrolDetailInput
    {
        /// <summary>
        /// 巡查轨迹点Id
        /// </summary>
        public int PatrolDetailId { get; set; }
        /// <summary>
        /// 消防设施编号（扫码巡查时用到）
        /// </summary>
        public string DeviceSn { get; set; }
        /// <summary>
        /// 巡查地点
        /// </summary>
        public string PatrolAddress { get; set; }
        /// <summary>
        /// 巡查建筑Id
        /// </summary>
        public int ArchitectureId { get; set; }
        /// <summary>
        /// 巡查楼层Id
        /// </summary>
        public int FloorId { get; set; }
        /// <summary>
        /// 记录状态
        /// </summary>
        public DutyOrPatrolStatus PatrolStatus { get; set; }
        /// <summary>
        /// 现场照片
        /// </summary>
        public IFormFile LivePicture1 { get; set; }
        /// <summary>
        /// 现场照片
        /// </summary>
        public IFormFile LivePicture2 { get; set; }
        /// <summary>
        /// 现场照片
        /// </summary>
        public IFormFile LivePicture3 { get; set; }
        /// <summary>
        /// 问题文字描述
        /// </summary>
        public string ProblemRemark { get; set; }
        /// <summary>
        /// 问题描述语音
        /// </summary>
        public IFormFile ProblemVoice { get; set; }
        /// <summary>
        /// 语音长度
        /// </summary>
        public int VoiceLength { get; set; }
    }
}
