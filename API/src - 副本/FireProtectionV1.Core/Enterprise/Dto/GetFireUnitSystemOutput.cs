using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class GetFireUnitSystemOutput
    {
        /// <summary>
        /// 消防系统ID
        /// </summary>
        public int FireSystemID { get; set; }

        /// <summary>
        /// 消防系统名称
        /// </summary>
        public string FireSystemName { get; set; }
    }
}
