using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetRecordElectricInput
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [Required]
        public int DeviceId { get; set; }
        /// <summary>
        /// 监测标识：A、L、N、L1、L2、L3，其中A代表剩余电流
        /// </summary>
        [Required]
        public string Sign { get; set; }
        /// <summary>
        /// 开始时间 yyyy-MM-dd HH:mm:ss
        /// </summary>
        public DateTime Start { get; set; }
        /// <summary>
        /// 结束时间 yyyy-MM-dd HH:mm:ss
        /// </summary>
        public DateTime End { get; set; }

        public GetRecordElectricInput()
        {
            End = DateTime.Now;
            Start = End.Date.AddDays(-1);
        }
    }
    public class GetRecordDetectorInput
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
        public int FireUnitId { get; set; }
        public GetRecordDetectorInput()
        {
            End = DateTime.Now;
            Start = End.Date.AddDays(-1);
            //Start = End.AddHours(-1);
        }
    }
}
