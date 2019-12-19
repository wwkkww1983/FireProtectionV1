using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto.FireDevice
{
    public class GetFireAlarm30DayDto
    {
        /// <summary>
        /// 部件地址
        /// </summary>
        public string Identify { get; set; }
        /// <summary>
        /// 部件类型
        /// </summary>
        public string DetectorTypeName { get; set; }
        /// <summary>
        /// 楼层
        /// </summary>
        public string FireUnitArchitectureFloorName { get; set; }
        /// <summary>
        /// 安装位置
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 报警时间
        /// </summary>
        public string AlarmTime { get; set; }
    }
}
