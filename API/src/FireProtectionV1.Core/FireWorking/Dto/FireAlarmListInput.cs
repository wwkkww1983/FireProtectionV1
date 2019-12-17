using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class FireAlarmListInput
    {
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }
        /// <summary>
        /// 核警状态查询条件，以英文逗号分隔，例如：未核警,误报,测试,真实火警,已过期
        /// </summary>
        public string CheckStates { get; set; }
    }
}
