using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.StreetGridCore.Model
{
    public class StreetGridEventRemark : EntityBase
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public int StreetGridEventId { get; set; }
        /// <summary>
        /// 事件描述
        /// </summary>
        public string Remark { get; set; }
    }
}
