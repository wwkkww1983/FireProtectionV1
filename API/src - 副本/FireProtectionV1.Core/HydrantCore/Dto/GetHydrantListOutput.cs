using FireProtectionV1.HydrantCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.HydrantCore.Dto
{
    public class GetHydrantListOutput : Hydrant
    {
        /// <summary>
        /// 区域名称
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// 水压数值
        /// </summary>
        public double Pressure { get; set; }
        /// <summary>
        /// 最近一次报警事件
        /// </summary>
        public string LastAlarmTitle { get; set; }
        /// <summary>
        /// 最近一次报警事件
        /// </summary>
        public string LastAlarmTime { get; set; }
        /// <summary>
        /// 最近30天报警次数
        /// </summary>
        public int NearbyAlarmNumber { get; set; }
    }
}
