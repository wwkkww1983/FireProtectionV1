using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetBreakDownInput
    {
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }
        /// <summary>
        /// 故障来源
        /// </summary>
        [Required]
        public SourceType Source { get; set; }
        /// <summary>
        /// 处理状态
        /// </summary>
        [Required]
        public HandleStatus HandleStatus { get; set; }
    }
}
