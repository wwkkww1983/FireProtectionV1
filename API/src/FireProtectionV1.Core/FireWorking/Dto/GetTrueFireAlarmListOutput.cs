using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetTrueFireAlarmListOutput
    {
        /// <summary>
        /// 报警时间
        /// </summary>
        public DateTime FireAlarmTime { get; set; }
        /// <summary>
        /// 核警时间
        /// </summary>
        public DateTime FireCheckTime { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        public int FireUnitId { get; set; }
        /// <summary>
        /// 防火单位名称
        /// </summary>
        public string FireUnitName { get; set; }
        /// <summary>
        /// 防火单位地址
        /// </summary>
        public string FireUnitAddress { get; set; }
        /// <summary>
        /// 防火单位消防联系人
        /// </summary>
        public string ContractName { get; set; }
        /// <summary>
        /// 防火单位消防联系人手机
        /// </summary>
        public string ContractPhone { get; set; }
        /// <summary>
        /// 报警部件类型
        /// </summary>
        public string AlarmDetectorTypeName { get; set; }
        /// <summary>
        /// 报警部件位置
        /// </summary>
        public string AlarmDetectorAddress { get; set; }
    }
}
