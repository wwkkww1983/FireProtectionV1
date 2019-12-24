using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetDutyInfoForWebOutput
    {
        /// <summary>
        /// 值班ID
        /// </summary>
        public int DutyId { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public string CreationTime { get; set; }
        /// <summary>
        /// 值班人员
        /// </summary>
        public string DutyUser { get; set; }
        /// <summary>
        /// 记录状态
        /// </summary>
        public DutyOrPatrolStatus DutyStatus { get; set; }
        /// <summary>
        /// 值班记录描述
        /// </summary>
        public string DutyRemark { get; set; }
        /// <summary>
        /// 值班记录图片路径
        /// </summary>
        public List<string> DutyPhtosPath { get; set; }
    }
}
