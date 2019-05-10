using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    /// <summary>
    /// 探测器类型
    /// </summary>
    public class DetectorType : EntityBase
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        [MaxLength(20)]
        public string Name { get; set; }
    }
    public enum FireSysType
    {
        /// <summary>
        /// 安全用电
        /// </summary>
        Electric = 1,
        /// <summary>
        /// 火警预警
        /// </summary>
        Fire
    }
}
