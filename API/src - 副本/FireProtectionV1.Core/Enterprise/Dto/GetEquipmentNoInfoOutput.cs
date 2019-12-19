using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class GetEquipmentNoInfoOutput
    {
        /// <summary>
        /// 具体地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 消防系统
        /// </summary>
        public string FireSystemName { get; set; }
    }
}
