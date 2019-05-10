using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetPageByFireUnitIdInput: PagedResultRequestDto
    {
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int Id { get; set; }
    }
}
