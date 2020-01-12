using FireProtectionV1.Enterprise.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class UpdateFireUnitArchitectureFloorInput
    {
        /// <summary>
        /// 楼层Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 楼层平面图
        /// </summary>
        public string Floor_Picture { get; set; }
    }
}
