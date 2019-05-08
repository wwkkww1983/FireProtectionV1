using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.HydrantCore.Model
{
    public class HydrantAlarm : EntityBase
    {
        /// <summary>
        /// 消火栓Id
        /// </summary>
        public int HydrantId { get; set; }
        /// <summary>
        /// 报警事件标题
        /// </summary>
        public string Title { get; set; }
    }
}
