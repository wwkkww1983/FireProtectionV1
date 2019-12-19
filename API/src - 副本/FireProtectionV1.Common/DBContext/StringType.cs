using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Common.DBContext
{
    /// <summary>
    /// 字符型字段长度限制
    /// 如果超出Long的范围，则直接用string定义字段类型，不使用此枚举限制
    /// </summary>
    public class StringType
    {
        public const int Short = 10;
        public const int Normal = 50;
        public const int Long = 200;
    }
}
