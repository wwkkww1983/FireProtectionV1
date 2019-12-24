using Abp.Application.Services.Dto;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetDataDutyForWebInput 
    {
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }
        /// <summary>
        /// 年份（初始传0）
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// 月份（初始传0）
        /// </summary>
        public int Month { get; set; }
    }
}
