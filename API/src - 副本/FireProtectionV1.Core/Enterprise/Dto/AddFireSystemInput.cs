using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class AddFireSystemInput
    {
        /// <summary>
        /// 消防系统名称
        /// </summary>
        [Required]
        public string SystemName { get; set; }
    }
}
