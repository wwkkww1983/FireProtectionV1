using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class FireAlarmDetailOutput
    {
        /// <summary>
        /// 火警数据Id
        /// </summary>
        public int FireAlarmId { get; set; }
        /// <summary>
        /// 接收火警时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 部件地址
        /// </summary>
        public string DetectorSn { get; set; }
        /// <summary>
        /// 部件类型名称
        /// </summary>
        public string DetectorTypeName { get; set; }
        /// <summary>
        /// 部件安装位置
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 火警联网设施编号
        /// </summary>
        public string GatewaySn { get; set; }
        /// <summary>
        /// 消防联系人
        /// </summary>
        public string FireContractUser { get; set; }
        /// <summary>
        /// 核警状态(0:未核警，1:误报，2:测试，3:真实火警)
        /// </summary>
        public int CheckState { get; set; }
        /// <summary>
        /// 核警时间
        /// </summary>
        public DateTime? CheckTime { get; set; }
        /// <summary>
        /// 核警情况说明
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 核警语音说明
        /// </summary>
        public string VioceUrl { get; set; }
        /// <summary>
        /// 核警语音长度
        /// </summary>
        public int VoiceLength { get; set; }
        /// <summary>
        /// 核警人员
        /// </summary>
        public string FireCheckUser { get; set; }
        /// <summary>
        /// 是否通知工作人员
        /// </summary>
        public int NotifyWorker { get; set; }
        /// <summary>
        /// 是否通知微型消防站
        /// </summary>
        public int NotifyMiniaturefire { get; set; }
        /// <summary>
        /// 是否通知119
        /// </summary>
        public int Notify119 { get; set; }
    }
}
