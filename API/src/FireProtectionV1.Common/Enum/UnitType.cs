using System;
using System.Collections.Generic;
using System.Text;

namespace DeviceServer.Tcp.Protocol
{
    /// <summary>
    /// 部件类型
    /// </summary>
    public enum UnitType : byte
    {
        /// <summary>
        /// 火灾报警控制器
        /// </summary>
        AlarmController = 1,
        /// <summary>
        /// (自定义)用户信息传输装置
        /// </summary>
        UITD = 2,
        /// <summary>
        /// 可燃气体探铡器
        /// </summary>
        GasDetector = 10,
        /// <summary>
        /// 点型可燃气体探测器
        /// </summary>
        GasDetectorPoint = 11,
        /// <summary>
        /// 独立式可燃气体探测器
        /// </summary>
        GasDetectorDiscrete = 12,
        /// <summary>
        /// 线型可燃气体探测器
        /// </summary>
        GasDetectorLine = 13,
        /// <summary>
        /// 电气火灾监控报警器
        /// </summary>
        ElectricAlertor = 16,
        /// <summary>
        /// 剩余电流式电气火灾监控探测器
        /// </summary>
        ElectricResidual = 17,
        /// <summary>
        /// 测温式电气火灾监控探测器
        /// </summary>
        ElectricTemperature = 18,
        /// <summary>
        /// 手动火灾报警按钮
        /// </summary>
        ManualAlarmButton = 23
    }
}
