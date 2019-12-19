using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetBreakDownInfoInput
    {
        /// <summary>
        /// 故障ID
        /// </summary>
        [Required]
        public int BreakDownId { get; set; }
    }
}
