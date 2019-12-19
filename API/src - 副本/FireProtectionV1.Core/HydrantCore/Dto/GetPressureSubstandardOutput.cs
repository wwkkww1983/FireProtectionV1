using Abp.Application.Services.Dto;
using FireProtectionV1.HydrantCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.HydrantCore.Dto
{
    public class GetPressureSubstandardOutput
    {
        /// <summary>
        /// 低于设定水压总数
        /// </summary>
        public int SubstanCount { get; set; }
        /// <summary>
        /// 消火栓页面数据
        /// </summary>
        public PagedResultDto<GetHydrantListOutput> PagedResultDto { get; set; }
    }
}
