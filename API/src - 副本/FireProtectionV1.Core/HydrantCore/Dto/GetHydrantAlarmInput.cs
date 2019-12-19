using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.HydrantCore.Dto
{
    public class GetHydrantAlarmInput : PagedResultRequestDto
    {
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }
    }
}
