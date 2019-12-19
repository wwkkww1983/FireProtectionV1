using FireProtectionV1.MiniFireStationCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.MiniFireStationCore.Dto
{
    public class GetNearbyStationOutput : MiniFireStation
    {
        /// <summary>
        /// 距离（米）
        /// </summary>
        public double Distance { get; set; }
    }
}
