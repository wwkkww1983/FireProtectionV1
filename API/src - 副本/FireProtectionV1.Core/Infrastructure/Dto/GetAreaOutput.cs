using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Infrastructure.Dto
{
    public class GetAreaOutput
    {
        /// <summary>
        /// 防火单位类型Id
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 防火单位类型
        /// </summary>
        public string AreaName { get; set; }
    }
}
