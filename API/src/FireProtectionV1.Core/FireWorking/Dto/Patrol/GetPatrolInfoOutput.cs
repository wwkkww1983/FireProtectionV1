using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto.Patrol
{
    public class GetPatrolInfoOutput
    {
        /// <summary>
        /// 记录Id
        /// </summary>
        public int PatrolId { get; set; }
        /// <summary>
        /// 巡查方式
        /// </summary>
        public PatrolType PatrolType { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 巡查人员姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 巡查人员手机
        /// </summary>
        public string UserPhone { get; set; }
        /// <summary>
        /// 记录状态
        /// </summary>
        public DutyOrPatrolStatus State { get; set; }
        /// <summary>
        /// 巡查轨迹
        /// </summary>
        public List<PatrolDetail> PatrolDetailList { get; set; }
    }
    public class PatrolDetail
    {
        /// <summary>
        /// 轨迹点Id
        /// </summary>
        public int patrolDetailId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 巡查地点
        /// </summary>
        public string PatrolAddress { get; set; }
        /// <summary>
        /// 设备编号（扫码巡查才有值）
        /// </summary>
        public string DeviceSn { get; set; }
        /// <summary>
        /// 设备名称（扫码巡查才有值）
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 设备型号（扫码巡查才有值）
        /// </summary>
        public string DeviceModel { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public DutyOrPatrolStatus Status { get; set; }
        /// <summary>
        /// 问题描述文字
        /// </summary>
        public string ProblemRemark { get; set; }
        /// <summary>
        /// 问题描述语音url
        /// </summary>
        public string ProblemVoiceUrl { get; set; }
        /// <summary>
        /// 语音长度
        /// </summary>
        public int VoiceLength { get; set; }
        /// <summary>
        /// 巡查记录图片路径
        /// </summary>
        public List<string> PatrolPhtosPath { get; set; }
        /// <summary>
        /// 巡查记录图片缩略图
        /// </summary>
        public List<string> PatrolPhotosBase64 { get; set; }
    }
}
