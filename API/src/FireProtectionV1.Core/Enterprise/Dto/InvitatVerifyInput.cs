using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class InvitatVerifyInput
    {
        /// <summary>
        /// 防火单位名称
        /// </summary>
        public string FireUnitName { get; set; }
        /// <summary>
        /// 邀请码
        /// </summary>
        public string InvitatCode { get; set; }
    }
}
