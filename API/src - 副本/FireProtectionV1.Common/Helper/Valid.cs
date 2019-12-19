using Abp.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Common.Helper
{
    /// <summary>
    /// 异常过滤辅助类
    /// </summary>
    public static class Valid
    {
        /// <summary>
        /// 验证数据，当v值为true时，抛出异常
        /// </summary>
        /// <param name="v">判断条件 true 抛出异常</param>
        /// <param name="message">异常提示信息</param>
        public static void Exception(bool v, string message)
        {
            if (v)
            {
                throw new UserFriendlyException(500, message);
            }
        }

    }
}
