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
        /// <summary>
        /// 在指定区域中查询，如果areaId=0则在全部区域中查询
        /// </summary>
        public int AreaId { get; set; } = 0;
    }
}
