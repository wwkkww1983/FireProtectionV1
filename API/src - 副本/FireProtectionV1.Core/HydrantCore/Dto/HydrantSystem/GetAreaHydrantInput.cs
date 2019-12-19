using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.HydrantCore.Dto
{
    public class GetAreaHydrantInput : PagedResultRequestDto
    {
        /// <summary>
        /// 区域ID
        /// </summary>
        [Required]
        public int AreaID { get; set; }
    }
}
