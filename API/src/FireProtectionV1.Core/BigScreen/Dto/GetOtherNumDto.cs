using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.BigScreen.Dto
{
    public class GetOtherNumOutput
    {
        /// <summary>
        /// 本月火警数量
        /// </summary>
        public int FireAlarmNum { get; set; }
        /// <summary>
        /// 联网单位数量
        /// </summary>
        public int FireunitNum { get; set; }
    }
}
