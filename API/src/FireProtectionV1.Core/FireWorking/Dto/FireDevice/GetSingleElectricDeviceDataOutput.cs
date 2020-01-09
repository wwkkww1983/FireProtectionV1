using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto.FireDevice
{
    public class GetSingleElectricDeviceDataOutput
    {
        /// <summary>
        /// 设备状态：离线/良好/隐患/超限
        /// </summary>
        public FireElectricDeviceState State { get; set; }
        /// <summary>
        /// 剩余电流监测
        /// </summary>
        public bool ExistAmpere { get; set; }
        /// <summary>
        /// 电缆温度监测
        /// </summary>
        public bool ExistTemperature { get; set; }
        /// <summary>
        /// "单项"/"三项"
        /// </summary>
        public PhaseType PhaseType { get; set; }
        /// <summary>
        /// 当前L数值
        /// </summary>
        public string L { get; set; }
        /// <summary>
        /// 当前N数值
        /// </summary>
        public string N { get; set; }
        /// <summary>
        /// 当前电流数值
        /// </summary>
        public string A { get; set; }
        /// <summary>
        /// 当前L1数值
        /// </summary>
        public string L1 { get; set; }
        /// <summary>
        /// 当前L2数值
        /// </summary>
        public string L2 { get; set; }
        /// <summary>
        /// 当前L3数值
        /// </summary>
        public string L3 { get; set; }
    }
}
