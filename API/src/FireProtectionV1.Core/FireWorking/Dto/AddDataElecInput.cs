using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class AddDataElecInput
    {
        /// <summary>
        /// 电气火灾设施编号
        /// </summary>
        [Required]
        public string FireElectricDeviceSn { get; set; }
        /// <summary>
        /// 记录的类型标记：A/L/N/L1/L2/L3，其中A代表剩余电流，其它代表电缆温度
        /// </summary>
        [Required]
        public string Sign { get; set; }
        /// <summary>
        /// 模拟量值
        /// </summary>
        [Required]
        public double Analog { get; set; }
    }
}
