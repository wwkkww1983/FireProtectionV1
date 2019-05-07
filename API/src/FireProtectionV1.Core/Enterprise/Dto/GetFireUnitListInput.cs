using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class GetFireUnitListInput : PagedResultRequestDto
    {
        /// <summary>
        /// 防火单位名称
        /// </summary>
        public string Name { get; set; }
        ///// <summary>
        ///// 跳转数量
        ///// </summary>
        //[System.ComponentModel.DataAnnotations.Range(0, int.MaxValue)]
        //public override int SkipCount { get; set; }

    }
}
