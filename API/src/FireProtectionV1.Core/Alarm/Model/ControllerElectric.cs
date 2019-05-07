using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Alarm.Model
{
    /// <summary>
    /// 安全用电控制器
    /// </summary>
    public class ControllerElectric : EntityBase
    {
        /// <summary>
        /// 编号
        /// </summary>
        [MaxLength(20)]
        public string Sn { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }
        /// <summary>
        /// 网关状态
        /// </summary>
        [MaxLength(10)]
        public string NetworkState { get; set; }
    }
}
