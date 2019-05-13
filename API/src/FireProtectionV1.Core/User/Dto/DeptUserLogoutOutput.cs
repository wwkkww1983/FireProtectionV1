﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.User.Dto
{
    public class DeptUserLogoutOutput
    {
        /// <summary>
        /// 注销成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 失败原因
        /// </summary>
        public string FailCause { get; set; }
    }
}