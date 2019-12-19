using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Enterprise.Model
{
    public class FireSystem :EntityBase
    {
        /// <summary>
        /// 消防系统名称
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string SystemName { get; set; }
    }
}
