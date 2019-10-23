using Abp.Application.Services.Dto;
using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class GetEquipmentNoListInput : PagedResultRequestDto
    {
        /// <summary>
        /// 设施编码
        /// </summary>
        public string EquiNo { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }
    }
}
