using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetBreakDownTotalOutput
    {
        /// <summary>
        /// 值班故障来源数量
        /// </summary>
        public int DutyCount { get; set; }
        /// <summary>
        /// 巡查故障来源数量
        /// </summary>
        public int PatrolCount { get; set; }
        /// <summary>
        /// 物联终端故障来源数量
        /// </summary>
        public int TerminalCount { get; set; }


        /// <summary>
        /// 待处理数量
        /// </summary>
        public int UuResolveCount { get; set; }
        /// <summary>
        /// 处理中数量
        /// </summary>
        public int ResolvingCount { get; set; }
        /// <summary>
        /// 已解决数量
        /// </summary>
        public int ResolvedCount { get; set; }
     
    }
}