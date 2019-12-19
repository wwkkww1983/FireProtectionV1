using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class GetEquipmentNoInfoInput
    {
        /// <summary>
        /// 设施编码
        /// </summary>
        [Required]
        public string EquiNo { get; set; }
    }
}
