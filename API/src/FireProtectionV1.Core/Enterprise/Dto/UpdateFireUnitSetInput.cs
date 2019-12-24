using FireProtectionV1.Common.Enum;
using FireProtectionV1.Enterprise.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class UpdateFireUnitSetInput
    {
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }     
        /// <summary>
        /// 维保单位Id
        /// </summary>
        public int SafeUnitId { get; set; }
        /// <summary>
        /// 巡查方式
        /// </summary>
        public PatrolType Patrol { get; set; }
    }
}
