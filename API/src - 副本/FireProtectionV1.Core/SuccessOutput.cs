using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1
{
    public class SuccessOutput
    {
        /// <summary>
        /// 提交是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 失败原因
        /// </summary>
        public string FailCause { get; set; }
    }
}
