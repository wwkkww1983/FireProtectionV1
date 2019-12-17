﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TsjDeviceServer
{
    class TsjConvert
    {
        /// <summary>
        /// 返回10位时间戳 Timestamp
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int ToUnixTimestamp( DateTime target)
        {
            if (target.Kind == DateTimeKind.Unspecified)
                target = target.ToLocalTime();
            return (int)((target.ToUniversalTime().Ticks - 621355968000000000) / 10000000);
        }

        /// <summary>
        /// 将10位时间戳Timestamp转换成日期
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static DateTime ToLocalDateTime( int target)
        {
            var date = new DateTime(621355968000000000 + (long)target * (long)10000000, DateTimeKind.Utc);
            return date.ToLocalTime();
        }
  }
}
