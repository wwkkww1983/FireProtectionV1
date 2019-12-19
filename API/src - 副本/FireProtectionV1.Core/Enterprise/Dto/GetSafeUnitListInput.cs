using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class GetSafeUnitListInput : PagedResultRequestDto
    {
        public string Name { get; set; }
    }
}
