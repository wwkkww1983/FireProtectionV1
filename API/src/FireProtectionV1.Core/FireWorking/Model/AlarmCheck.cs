using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    public class AlarmCheck: EntityBase
    {
        /// <summary>
        /// 报警类型electric:1,fire:2
        /// </summary>
        [Required]
        public byte FireSysType { get; set; }
        /// <summary>
        /// 报警Id
        /// </summary>
        [Required]
        public int AlarmDataId { get; set; }
        /// <summary>
        /// 核实警状态
        /// </summary>
        [Required]
        public byte CheckState { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }
        /// <summary>
        /// 检查情况
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 检查图片
        /// </summary>
        public string Picturs { get; set; }
        /// <summary>
        /// 检查语音
        /// </summary>
        public string Vioce { get; set; }
    }
}
