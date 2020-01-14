using System;
using System.Collections.Generic;
using System.Text;

namespace TsjDeviceServer.Data
{
    public class DataHello:TsjData
    {
        /// <summary>
        /// 协议版本
        /// </summary>
        public string protocol { get; set; }
        /// <summary>
        /// 软件版本
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 配置业务信息CRC32校验
        /// </summary>
        public string CRC32_config { get; set; }
        /// <summary>
        /// 当前wifi连接信息
        /// </summary>
        public string wifi { get; set; }
    }
}
