using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.MiniFireStationCore.Dto
{
    public class GetMiniFireStationListInput: PagedResultRequestDto
    {
        /// <summary>
        /// 微型消防站名称
        /// </summary>
        public string Name { get; set; }
    }
}
