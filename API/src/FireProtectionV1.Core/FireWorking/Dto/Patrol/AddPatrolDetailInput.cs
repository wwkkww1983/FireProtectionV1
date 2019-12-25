using FireProtectionV1.Common.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto.Patrol
{
    public class AddPatrolDetailInput
    {
        /// <summary>
        /// 巡查记录Id
        /// </summary>
        public int PatrolId { get; set; }
        /// <summary>
        /// 巡查用户Id（如果PatrolId=0，则该值必须填写，否则不用填）
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 巡查模式（如果PatrolId=0，则该值必须填写，否则不用填）
        /// </summary>
        public PatrolType PatrolType { get; set; }
        /// <summary>
        /// 防火单位Id（如果PatrolId=0，则该值必须填写，否则不用填）
        /// </summary>
        public int FireUnitId { get; set; }
        /// <summary>
        /// 巡查人员归属的单位Id。如果是本防火单位人员，则该值与FireUnitId一致，如果是维保人员，则该值存放维保单位的Id（如果PatrolId=0，则该值必须填写，否则不用填）
        /// </summary>
        public int UserBelongUnitId { get; set; }
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
