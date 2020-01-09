using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    public class AlarmToWater : EntityBase
    {
        /// <summary>
        /// 消防管网监测设备Id
        /// </summary>
        public int FireWaterDeviceId { get; set; }
        /// <summary>
        /// 模拟量值
        /// </summary>
        public double Analog { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        public int FireUnitId { get; set; }
        /// <summary>
        /// 是否已读取（用于手机端显示未读的报警数据）
        /// </summary>
        public bool IsRead { get; set; } = false;
    }
}
