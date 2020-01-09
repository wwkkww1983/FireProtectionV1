using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetNoReadAlarmNumOutput
    {
        /// <summary>
        /// 警情类型
        /// </summary>
        public AlarmType AlarmType { get; set; }
        /// <summary>
        /// 未读的警情数量
        /// </summary>
        public int NoReadAlarmNum { get; set; }
    }
}
