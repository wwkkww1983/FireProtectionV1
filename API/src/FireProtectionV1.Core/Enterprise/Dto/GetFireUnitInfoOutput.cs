using FireProtectionV1.Common.Enum;
using FireProtectionV1.Enterprise.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class GetFireUnitInfoOutput : FireUnit
    {
        /// <summary>
        /// 防火单位区域
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 防火单位类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 维保单位
        /// </summary>
        public string SafeUnit { get; set; }
        /// <summary>
        /// 是否关注
        /// </summary>
        public bool IsAttention { get; set; }
        /// <summary>
        /// 巡查方式
        /// </summary>
        //public Patrol Patrol { get; set; }
    }
}
