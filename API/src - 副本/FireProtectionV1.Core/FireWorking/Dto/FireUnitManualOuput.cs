using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class FireUnitManualOuput
    {
        /// <summary>
        /// 防火单位Id
        /// </summary>
        public int FireUnitId { get; set; }
        /// <summary>
        /// 防火单位名称
        /// </summary>
        public string FireUnitName { get; set; }
        /// <summary>
        /// 最近一次记录提交时间
        /// </summary>
        public string LastTime { get; set; }
        /// <summary>
        /// 最近30天提交记录数
        /// </summary>
        public int Recent30DayCount { get; set; }
    }
}
