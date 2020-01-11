using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    public class AlarmToElectric : EntityBase
    {
        /// <summary>
        /// 电气火灾设施Id
        /// </summary>
        [Required]
        public int FireElectricDeviceId { get; set; }
        /// <summary>
        /// 记录的类型标记：A/L/N/L1/L2/L3，其中A代表剩余电流，其它代表电缆温度
        /// </summary>
        [Required]
        public string Sign { get; set; }
        /// <summary>
        /// 报警状态：隐患/超限
        /// </summary>
        public FireElectricDeviceState State { get; set; }
        /// <summary>
        /// 模拟量值
        /// </summary>
        [Required]
        public double Analog { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }
        /// <summary>
        /// 是否已读取（用于手机端显示未读的报警数据）
        /// </summary>
        public bool IsRead { get; set; } = false;
    }
}
