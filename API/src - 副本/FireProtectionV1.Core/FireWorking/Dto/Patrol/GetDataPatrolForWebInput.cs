using Abp.Application.Services.Dto;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetDataPatrolForWebInput
    {
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }
        /// <summary>
        /// 月份（格式:yyyy-MM）
        /// </summary>
        public DateTime Moth { get; set; }
    }
}
