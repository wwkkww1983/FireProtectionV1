using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Infrastructure.Dto
{
    public class GetAreaOutput
    {
        /// <summary>
        /// 区域Id
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string AreaName { get; set; }
    }
}
