using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetRecordAnalogInput
    {
        /// <summary>
        /// 探测器Id
        /// </summary>
        [Required]
        public int DetectorId { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime Start { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime End { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }
        public GetRecordAnalogInput()
        {
            End = DateTime.Now;
            Start = End.AddHours(-1);
        }
    }
}
