using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetDataDutyInfoInput
    {
        /// <summary>
        /// 值班记录ID
        /// </summary>
        [Required]
        public int DutyId { get; set; }
    }
}
