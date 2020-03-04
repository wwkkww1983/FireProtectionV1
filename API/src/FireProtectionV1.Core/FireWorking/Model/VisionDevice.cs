using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    public class VisionDevice : EntityBase
    {
        /// <summary>
        /// 消防分析仪编号
        /// </summary>
        public string Sn { get; set; }
        /// <summary>
        /// 最大监控路数
        /// </summary>
        public int MonitorNum { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        public int FireUnitId { get; set; }
    }
}
