using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    public class VisionDevice : EntityBase
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Sn { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        public int FireUnitId { get; set; }
    }
}
