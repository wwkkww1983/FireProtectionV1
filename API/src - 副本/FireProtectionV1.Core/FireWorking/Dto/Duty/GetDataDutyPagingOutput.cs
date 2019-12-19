using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetDataDutyPagingOutput
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 页面数据
        /// </summary>
        public List<GetDataDutyOutput> DutyList { get; set; }
    }
}
