using FireProtectionV1.Common.DBContext;
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
        /// 值班记录图片
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string DutyPicture { get; set; }
        /// <summary>
        /// 记录状态（1、正常；2：绿色故障；3：橙色故障）
        /// </summary>
        [Required]
        public byte DutyStatus { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }
        /// <summary>
        /// 防火单位用户Id
        /// </summary>
        [Required]
        public int FireUnitUserId { get; set; }
    }
}
