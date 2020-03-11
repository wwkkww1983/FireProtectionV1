using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.BigScreen.Dto
{
    public class GetFireDetectorStatusNumDto
    {
        /// <summary>
        /// 联网部件总数量
        /// </summary>
        public int TotalNum { get; set; }
        /// <summary>
        /// 故障部件数量
        /// </summary>
        public int FaultNum { get; set; }
        /// <summary>
        /// 部件正常率
        /// </summary>
        public decimal NormalRate { get; set; }
    }
}
