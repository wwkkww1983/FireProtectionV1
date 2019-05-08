using FireProtectionV1.StreetGridCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.StreetGridCore.Dto
{
    public class GetStreeGridEventListOutput : StreetGridEvent
    {
        /// <summary>
        /// 网格名称
        /// </summary>
        public string StreeGridName { get; set; }
        /// <summary>
        /// 所属街道
        /// </summary>
        public string Street { get; set; }
    }
}
