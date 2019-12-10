using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto.FireDevice
{
    public class GetFireElectricDeviceStateOutput
    {
        /// <summary>
        /// 在线数量
        /// </summary>
        public int OnlineNum { get; set; }
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
        public int BadNum { get; set; }
        /// <summary>
        /// 超限数量
        /// </summary>
        public int WarnNum { get; set; }
    }
}
