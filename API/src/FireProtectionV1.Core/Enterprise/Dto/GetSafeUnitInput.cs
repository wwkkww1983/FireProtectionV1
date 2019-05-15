using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class GetSafeUnitInput
    {
        /// <summary>
        /// 维保单位名称(模糊查询)
        /// </summary>
        public string Name { get; set; }
    }
}
