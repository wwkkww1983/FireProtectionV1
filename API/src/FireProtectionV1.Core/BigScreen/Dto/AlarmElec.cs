using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.BigScreen.Dto
{
    public class AlarmElec
    {
        public DateTime CreationTime { get; set; }
        public string FireUnitName { get; set; }
        public string ContractName { get; set; }
        public string ContractPhone { get; set; }
        public string Address { get; set; }
        /// <summary>
        /// 剩余电流探测器 | 电缆温度探测器
        /// </summary>
        public string AlarmType { get; set; }
        /// <summary>
        /// 报警数值（带单位，例如115℃）
        /// </summary>
        public string AlarmValue { get; set; }
    }
}
