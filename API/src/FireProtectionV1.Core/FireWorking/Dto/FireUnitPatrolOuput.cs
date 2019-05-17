using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class FireUnitPatrolOuput
    {
        /// <summary>
        /// 超过7天没有巡查记录的单位数量
        /// </summary>
        public int NoWork7DayCount { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        public int FireUnitId { get; set; }
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
