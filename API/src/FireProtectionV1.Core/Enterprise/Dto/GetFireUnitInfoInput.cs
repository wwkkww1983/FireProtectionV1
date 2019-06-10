using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class GetFireUnitInfoInput
    {
        ///// <summary>
        ///// 消防部门用户Id
        ///// </summary>
        [Required]
        public int UserId { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int Id { get; set; }
    }
}
