using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetFireUnitAlarmOutput
    {
        /// <summary>
        /// 防火单位名称
        /// </summary>
        public string FireUnitName { get; set; }
        /// <summary>
        /// 剩余电流探测器数量
        /// </summary>
        public int ElecECount { get; set; }
        /// <summary>
        /// 电缆温度探测器数量
        /// </summary>
        public int ElecTCount { get; set; }
        /// <summary>
        /// （安全用电）最近30天报警次数
        /// </summary>
        public int Elec30DayCount { get; set; }
        /// <summary>
        /// （安全用电）高频报警部件数量
        /// </summary>
        public int ElecHighCount { get; set; }
        /// <summary>
        /// （火警预警）物联网数据终端数量
        /// </summary>
        public int FirePointsCount { get; set; }
        /// <summary>
        /// （火警预警）最近30天报警次数
        /// </summary>
        public int Fire30DayCount { get; set; }
        /// <summary>
        /// （火警预警）高频报警部件数量
        /// </summary>
        public int FireHighCount { get; set; }
        /// <summary>
        /// 发生故障数量
        /// </summary>
        public int FaultCount { get; set; }
        /// <summary>
        /// 已处理故障数量
        /// </summary>
        public int FaultProcessedCount { get; set; }
        /// <summary>
        /// 待处理故障数量
        /// </summary>
        public int FaultPendingCount { get; set; }
        /// <summary>
        /// 巡查记录最近30天数量
        /// </summary>
        public int Patrol30DayCount { get; set; }
        /// <summary>
        /// 值班记录最近30天数量
        /// </summary>
        public int Duty30DayCount { get; set; }
    }
}
