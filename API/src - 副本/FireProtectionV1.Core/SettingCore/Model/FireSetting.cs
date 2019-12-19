using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.SettingCore.Model
{
    public class FireSetting : EntityBase
    {
        /// <summary>
        /// 设置项
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 下限值
        /// </summary>
        public double MinValue { get; set; }
        /// <summary>
        /// 上限值
        /// </summary>
        public double MaxValue { get; set; }
    }
}
