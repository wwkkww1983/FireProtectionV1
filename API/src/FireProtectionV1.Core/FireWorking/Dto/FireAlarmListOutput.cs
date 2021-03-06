﻿using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class FireAlarmListOutput
    {
        /// <summary>
        /// 火警数据Id
        /// </summary>
        public int FireAlarmId { get; set; }
        /// <summary>
        /// 火警接收时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 火警联网设施编号
        /// </summary>
        public string GatewaySn { get; set; }
        /// <summary>
        /// 部件地址
        /// </summary>
        public string DetectorSn { get; set; }
        /// <summary>
        /// 部件类型
        /// </summary>
        public string DetectorTypeName { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 存在点位图坐标
        /// </summary>
        public bool ExistBitMap { get; set; }
        /// <summary>
        /// 核警状态
        /// </summary>
        public FireAlarmCheckState CheckState { get; set; }
        /// <summary>
        /// 是否已读（手机端未读的警情需加粗显示）
        /// </summary>
        public bool IsRead { get; set; }
    }
}
