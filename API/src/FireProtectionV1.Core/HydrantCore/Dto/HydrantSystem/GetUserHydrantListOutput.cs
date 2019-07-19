using FireProtectionV1.Common.Enum;
using FireProtectionV1.HydrantCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.HydrantCore.Dto
{
    public class GetUserHydrantListOutput 
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 设施编号
        /// </summary>
        public string Sn { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 网关状态
        /// </summary>
        public GatewayStatus Status { get; set; }
    }
}

