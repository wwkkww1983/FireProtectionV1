using FireProtectionV1.Common.Enum;
using FireProtectionV1.HydrantCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.HydrantCore.Dto
{
    public class GetHydrantSetOutput
    {
        /// <summary>
        /// 水压最小值
        /// </summary>
        public double PressureMin { get; set; }
        /// <summary>
        /// 水压最大值
        /// </summary>
        public double PressureMax { get; set; }
        /// <summary>
        /// 电池电量报警阀值
        /// </summary>
        public double DumpEnergy { get; set; }
    }
}

