using System;
using System.Collections.Generic;
using System.Text;

namespace DeviceServer.Tcp
{
    /// <summary>
    /// 模拟量类型
    /// </summary>
    public enum AnalogType:byte
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown=0,
        /// <summary>
        /// 温度
        /// </summary>
        Temperature =3,
        /// <summary>
        /// 安吉斯自定义电流
        /// </summary>
        ResidualAjs=0x80
    }
}
