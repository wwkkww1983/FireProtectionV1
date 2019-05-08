using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Alarm.Dto
{
    public class GetFireUnitAlarmOutput
    {
        /// <summary>
        /// （安全用电）管控点位
        /// </summary>
        public int ElecPointsCount { get; set; }
        /// <summary>
        /// （安全用电）网关状态
        /// </summary>
        public string ElecState { get; set; }
        /// <summary>
        /// （安全用电）最近30天报警次数
        /// </summary>
        public int Elec30DayNum { get; set; }
        /// <summary>
        /// （安全用电）高频报警部件数量
        /// </summary>
        public int ElecHighCount { get; set; }
        /// <summary>
        /// （火警预警）管控点位
        /// </summary>
        public int FirePointsCount { get; set; }
        /// <summary>
        /// （火警预警）网关状态
        /// </summary>
        public string FireState { get; set; }
        /// <summary>
        /// （火警预警）最近30天报警次数
        /// </summary>
        public int Fire30DayNum { get; set; }
        /// <summary>
        /// （火警预警）高频报警部件数量
        /// </summary>
        public int FireHighCount { get; set; }
    }
}
