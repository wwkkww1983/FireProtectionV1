﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.StreetGridCore.Dto
{
    public class GetStreetGridListInput : PagedResultRequestDto
    {
        public string Name { get; set; }
    }
}