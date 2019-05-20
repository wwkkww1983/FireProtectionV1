using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class GetFireUnitListInput : PagedResultRequestDto
    {
        ///// <summary>
        ///// 消防部门用户Id
        ///// </summary>
        [Required]
        public int UserId { get; set; }
        /// <summary>
        /// 防火单位名称
        /// </summary>
        public string Name { get; set; }

    }
}
