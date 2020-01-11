using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class GetFloorListOutput
    {
        /// <summary>
        /// 楼层Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 楼层名称
        /// </summary>
        public string Name { get; set; }
    }
}
