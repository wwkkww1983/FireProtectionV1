using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class GetSafeUnitOutput
    {
        /// <summary>
        /// 维保单位Id
        /// </summary>
        public int SafeUnitId { get; set; }
        /// <summary>
        /// 维保单位名称
        /// </summary>
        public string SafeUnitName { get; set; }
    }
}
