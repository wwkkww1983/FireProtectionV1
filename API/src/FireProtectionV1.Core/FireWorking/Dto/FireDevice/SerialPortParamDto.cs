using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto.FireDevice
{
    public class SerialPortParamDto
    {
        /// <summary>
        /// 接口
        /// </summary>
        public string ComType { get; set; }
        /// <summary>
        /// 波特率
        /// </summary>
        public string Rate { get; set; }
        /// <summary>
        /// 校验位
        /// </summary>
        public byte ParityBit { get; set; }
        /// <summary>
        /// 数据位
        /// </summary>
        public string DataBit { get; set; }
        /// <summary>
        /// 停止位
        /// </summary>
        public string StopBit { get; set; }
    }
}
