using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.MiniFireStationCore.Model
{
    public class MiniFireActionType: EntityBase
    {
        /// <summary>
        /// 活动类别
        /// </summary>
        public string Type { get; set; }
    }
}
