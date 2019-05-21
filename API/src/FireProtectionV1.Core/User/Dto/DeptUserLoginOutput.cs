﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.User.Dto
{
    public class DeptUserLoginOutput:SuccessOutput
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
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }
    }
}
