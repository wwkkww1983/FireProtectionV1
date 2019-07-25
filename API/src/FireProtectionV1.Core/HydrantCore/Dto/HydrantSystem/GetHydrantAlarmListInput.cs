using Abp.Application.Services.Dto;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.HydrantCore.Dto
{
    public class GetHydrantAlarmListInput : PagedResultRequestDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required]
        public int UserID { get; set; }
        /// <summary>
        /// 处理状态
        /// </summary>
        [Required]
        public HandleStatus HandleStatus { get; set; }
        /// <summary>
        /// 是否只显示未读信息
        /// </summary>
        public bool OnlyUnRead { get; set; }
    }
    
}
