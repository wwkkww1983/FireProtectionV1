using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetFireUnitSystemOutput
    {
        /// <summary>
        /// 消防系统ID
        /// </summary>
        [Required]
        public int FireSystemId { get; set; }
        /// <summary>
        /// 消防系统名称
        /// </summary>
        public string SystemName { get; set; }
    }
}
