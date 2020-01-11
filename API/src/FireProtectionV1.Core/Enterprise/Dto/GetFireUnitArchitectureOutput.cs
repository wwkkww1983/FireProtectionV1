using FireProtectionV1.Enterprise.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class GetFireUnitArchitectureOutput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 建筑下的楼层列表
        /// </summary>
        public List<GetFloorListOutput> Floors { get; set; }
    }
}
