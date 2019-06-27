using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Enterprise.Model
{
    public class FireUntiSystem : EntityBase
    {
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }

        /// <summary>
        /// 消防系统Id
        /// </summary>
        [Required]
        public int FireSystemId { get; set; }
    }
}
