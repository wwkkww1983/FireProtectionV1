using FireProtectionV1.Common.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class AddPatrolOutput
    {
        /// <summary>
        /// 巡查ID
        /// </summary>
        [Required]
        public int PatrolId { get; set; }
    }
}
