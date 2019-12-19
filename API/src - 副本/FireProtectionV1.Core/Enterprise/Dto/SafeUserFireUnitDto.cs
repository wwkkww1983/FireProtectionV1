using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class SafeUserFireUnitDto
    {
        /// <summary>
        /// 维保人员Id
        /// </summary>
        public int SafeUserId { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        public int FireUnitId { get; set; }
    }
}
