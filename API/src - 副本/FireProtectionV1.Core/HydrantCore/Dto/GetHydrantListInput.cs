using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.HydrantCore.Dto
{
    public class GetHydrantListInput : PagedResultRequestDto
    {
        /// <summary>
        /// 设施编号
        /// </summary>
        public string Sn { get; set; }
    }
}
