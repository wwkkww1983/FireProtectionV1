using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.BigScreen.Dto
{
    public class GetElectricDeviceStatusNumDto
    {
        /// <summary>
        /// 离线数量
        /// </summary>
        public int OfflineNum { get; set; }
        /// <summary>
        /// 良好数量
        /// </summary>
        public int GoodNum { get; set; }
        /// <summary>
        /// 隐患数量
        /// </summary>
        public int DangerNum { get; set; }
        /// <summary>
        /// 超限数量
        /// </summary>
        public int TransfiniteNum { get; set; }
    }
}
