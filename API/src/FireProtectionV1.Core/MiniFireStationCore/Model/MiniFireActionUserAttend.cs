using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.MiniFireStationCore.Model
{
    public class MiniFireActionUserAttend: EntityBase
    {
        /// <summary>
        /// 微型消防站ID
        /// </summary>
        public int MiniFireActionId { get; set; }
        /// <summary>
        /// 人员ID
        /// </summary>
        public int JobUserId { get; set; }
    }
}
