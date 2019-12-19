using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Enterprise.Model
{
    public class FireUnitArchitectureFloor : EntityBase
    {
        /// <summary>
        /// 楼层名称，例如1楼、-1楼
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 楼层平面图
        /// </summary>
        public string Floor_Picture { get; set; }
        /// <summary>
        /// 所属建筑ID
        /// </summary>
        public int ArchitectureId { get; set; }
    }
}
