using Abp.Application.Services.Dto;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetBreakDownInput : PagedResultRequestDto
    {
        /// <summary>
        /// 是否维保单位请求数据
        /// </summary>
        public bool IsRequstBySafe { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }
        /// <summary>
        /// 故障来源
        /// </summary>
        [Required]
        public FaultSource Source { get; set; }
        /// <summary>
        /// 处理状态(1待处理,3已解决,4自行处理,5维保叫修处理中,6维保叫修已处理)
        /// </summary>
        [Required]
        public HandleStatus HandleStatus { get; set; }
    }
}
