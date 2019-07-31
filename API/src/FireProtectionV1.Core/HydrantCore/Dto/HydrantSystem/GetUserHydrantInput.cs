using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.HydrantCore.Dto
{
    public class GetUserHydrantInput : PagedResultRequestDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required]
        public int UserID { get; set; }
        /// <summary>
        /// 消火栓名称(模糊查询)
        /// </summary>
        public string MatchName { get; set; }
    }
}
