using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto.FireDevice
{
    /// <summary>
    /// 高频报警部件数传对象
    /// </summary>
    public class GetFireAlarmHighDto
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
        /// 30天拼接每天报警数量
        /// </summary>
        public int AlarmNum { get; set; }
    }
}
