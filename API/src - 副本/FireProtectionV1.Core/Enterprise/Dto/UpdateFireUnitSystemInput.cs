using FireProtectionV1.Enterprise.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class UpdateFireUnitSystemInput
    {
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }

        /// <summary>
        /// 消防系统Id
        /// </summary>
        public List<int> SystemId { get; set; }
    }
}
