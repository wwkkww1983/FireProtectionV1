using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Enterprise.Model
{
    /// <summary>
    /// 防火单位预案
    /// </summary>
    public class FireUnitPlan : EntityBase
    {
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }
        /// <summary>
        /// 消防预案
        /// </summary>
        public string FirePlan { get; set; }
    }

}
