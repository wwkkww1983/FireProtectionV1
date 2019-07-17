using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    public class BreakDown : EntityBase
    {
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }
        /// <summary>
        /// 记录人员ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 故障来源（1.值班 2.巡查 3.物联终端）
        /// </summary>
        [Required]
        public byte Source { get; set; }
        /// <summary>
        /// 处理状态（1.未处理 2.处理中 3.已解决）
        /// </summary>
        [Required]
        public byte HandleStatus { get; set; }
        /// <summary>
        /// 故障来源数据ID
        /// </summary>
        [Required]
        public int DataId { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime SolutionTime { get; set; }
        /// <summary>
        /// 问题处理途径（1.自行处理 2.维保叫修）
        /// </summary>
        public byte SolutionWay { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(200)]
        public string Remark { get; set; }
    }
}
