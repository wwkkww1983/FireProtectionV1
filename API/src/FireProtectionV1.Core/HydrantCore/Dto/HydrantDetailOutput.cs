using FireProtectionV1.HydrantCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.HydrantCore.Dto
{
    public class HydrantDetailOutput : Hydrant
    {
        /// <summary>
        /// 区域名称
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// 水压数值
        /// </summary>
        public double Pressure { get; set; }
    }
}
