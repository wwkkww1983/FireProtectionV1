using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.HydrantCore.Dto
{
    public class QueryHyrantNameInput
    {
        /// <summary>
        /// 消火栓名称(模糊查询)
        /// </summary>
        public string MatchName { get; set; }
    }
}
