using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.HydrantCore.Model
{
    /// <summary>
    /// 市政消火栓水压
    /// </summary>
    public class HydrantPressure : EntityBase
    {
        /// <summary>
        /// 消火栓Id
        /// </summary>
        public int HydrantId { get; set; }
        /// <summary>
        /// 水压数值
        /// </summary>
        public double Pressure { get; set; }
    }
}
