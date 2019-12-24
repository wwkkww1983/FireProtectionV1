using FireProtectionV1.FireWorking.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetFireElectricDeviceOutput : FireElectricDevice
    {
        /// <summary>
        /// 监测类型数组（剩余电流、电缆温度）
        /// </summary>
        public List<string> MonitorItemList { get; set; }
        /// <summary>
        /// 超限动作数组（终端报警、云端报警、发送开关量信号）
        /// </summary>
        public List<string> EnableAlarmList { get; set; }
    }
}
