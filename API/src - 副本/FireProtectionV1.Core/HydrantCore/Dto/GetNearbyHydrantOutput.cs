using FireProtectionV1.HydrantCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.HydrantCore.Dto
{
    public class GetNearbyHydrantOutput: Hydrant
    {
        /// <summary>
        /// 距离（米）
        /// </summary>
        public double Distance { get; set; }

    }
}
