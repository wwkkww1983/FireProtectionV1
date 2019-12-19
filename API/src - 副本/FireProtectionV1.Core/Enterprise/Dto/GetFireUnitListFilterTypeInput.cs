using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class GetFireUnitListFilterTypeInput 
    {
        ///// <summary>
        ///// 消防部门用户Id
        ///// </summary>
        [Required]
        public int UserId { get; set; }
        /// <summary>
        /// 防火单位名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 防火单位类型Id
        /// </summary>
        public int FireUnitTypeId { get; set; }
        /// <summary>
        /// 网关状态值
        /// </summary>
        public string GetwayStatusValue { get; set; }

    }
}
