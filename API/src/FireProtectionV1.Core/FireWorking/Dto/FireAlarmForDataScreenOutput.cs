using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class FireAlarmForDataScreenOutput
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
        /// 核警状态(0:未核警，1:误报，2:测试，3:真实火警，4:已过期)
        /// </summary>
        public FireAlarmCheckState CheckState { get; set; }
    }
}
