using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class GetFireUnitListFilterTypeInput : GetFireUnitListInput
    {
        /// <summary>
        /// 防火单位类型Id
        /// </summary>
        public int FireUnitTypeId { get; set; }
        /// <summary>
        /// 网关状态Id
        /// </summary>
        public int GetwayStatusId { get; set; }

    }
}
