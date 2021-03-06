﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class EndDeviceStateOutput
    {
        /// <summary>
        /// 探测器ID
        /// </summary>
        public int DetectorId { get; set; }
        /// <summary>
        /// 探测器是否模拟量类型
        /// </summary>
        public bool IsAnalog { get; set; }
        /// <summary>
        /// 终端名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 终端地点
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 终端状态名称 "在线"/"离线"
        /// </summary>
        public string StateName { get; set; }
        /// <summary>
        /// 当前数值
        /// </summary>
        public string Analog { get; set; }
        /// <summary>
        /// 合格范围
        /// </summary>
        public string Standard { get; set; }
        /// <summary>
        /// 是否超限
        /// </summary>
        public bool IsOverRange { get; set; }
    }
}
