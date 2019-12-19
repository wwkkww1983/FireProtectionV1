using FireProtectionV1.Common.Enum;
using FireProtectionV1.HydrantCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.HydrantCore.Dto
{
    public class AddAlarm
    {
        /// <summary>
        /// 消火栓ID
        /// </summary>
        public int HydrantId { get; set; }
        /// <summary>
        /// 报警事件标题
        /// </summary>
        public string Title { get; set; }
    }
}

