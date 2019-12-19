using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    /// <summary>
    /// 电气火灾数据记录
    /// </summary>
    public class FireElectricRecord : EntityBase
    {
        /// <summary>
        /// 电气火灾设施Id
        /// </summary>
        public int FireElectricDeviceId { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        public int FireUnitId { get; set; }
        /// <summary>
        /// 记录的类型标记：A/L/N/L1/L2/L3，其中A代表剩余电流，其它代表电缆温度
        /// </summary>
        public string Sign { get; set; }
        /// <summary>
        /// 模拟量值
        /// </summary>
        public double Analog { get; set; }
    }
}
