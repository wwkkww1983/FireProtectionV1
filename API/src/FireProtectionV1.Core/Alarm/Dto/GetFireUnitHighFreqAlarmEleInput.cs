﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Alarm.Dto
{
    public class GetFireUnitHighFreqAlarmEleInput
    {
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int Id { get; set; }
    }
}
