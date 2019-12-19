using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class QueryFireUnitLikeNameInput
    {
        /// <summary>
        /// 防火单位名称(模糊查询)
        /// </summary>
        public string MatchName { get; set; }
    }
}
