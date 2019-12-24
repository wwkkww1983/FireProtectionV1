using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    /// <summary>
    /// 值班记录
    /// </summary>
    public class DataToDuty : EntityBase
    {
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }
        /// <summary>
        /// 值班用户Id
        /// </summary>
        [Required]
        public int UserId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public DutyOrPatrolStatus Status { get; set; }
    }
}
