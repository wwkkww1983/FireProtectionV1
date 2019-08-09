using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class PagedResultDeviceDto<T>: PagedResultDto<T>
    {
        /// <summary>
        /// 离线数量
        /// </summary>
        public int OfflineCount { get; set; }
    }
}
