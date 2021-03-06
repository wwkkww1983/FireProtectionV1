﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.User.Dto
{
    public class ChangeUserPassword
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required]
        public string Account { get; set; }
        /// <summary>
        /// 原密码
        /// </summary>
        [Required]
        public string OldPassword { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        [Required]
        public string NewPassword { get; set; }
    }
}
