using Abp.Application.Services.Dto;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetDataForCalendarInput
    {
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }
        /// <summary>
        /// 日历的年月（不传值则默认取当前月份）
        /// </summary>
        public DateTime? CalendarDate { get; set; }
    }
}
