using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.User.Dto
{
    public class SafeUserLoginOutput:SuccessOutput
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 维保单位名称
        /// </summary>
        public string SafeUnitName { get; set; }
        /// <summary>
        /// 维保单位ID
        /// </summary>
        public int SafeUnitID { get; set; }
    }
}
