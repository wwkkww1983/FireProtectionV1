using Abp.Application.Services.Dto;
using FireProtectionV1.StreetGridCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.StreetGridCore.Dto
{
    public class GetStreetGridEventListInput : PagedResultRequestDto
    {
        /// <summary>
        /// 事件状态
        /// </summary>
        public EventStatus Status { get; set; }
    }
}
