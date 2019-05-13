using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.DataReportCore.Dto
{
    public class DataMinotorOutput
    {
        /// <summary>
        /// 数据类型（防火单位、微型消防站、市政消火栓、综合数据报表）
        /// </summary>
        public string DataTypeName { get; set; }
        /// <summary>
        /// 接入数量
        /// </summary>
        public int JoinNumber { get; set; }
    }
}
