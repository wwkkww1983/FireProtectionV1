using Abp.Application.Services.Dto;
using FireProtectionV1.Common.Enum;
using FireProtectionV1.Enterprise.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetPatrolTypeOutput
    {
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public Patrol PatrolType { get; set; }
    }
}
