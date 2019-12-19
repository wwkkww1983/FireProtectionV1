using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class GetFireUnitTypeOutput
    {
        /// <summary>
        /// 防火单位类型Id
        /// </summary>
        public int TypeId { get; set; }
        /// <summary>
        /// 防火单位类型
        /// </summary>
        public string TypeName { get; set; }
    }
}
