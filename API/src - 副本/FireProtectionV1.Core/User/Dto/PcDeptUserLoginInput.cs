﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.User.Dto
{
    public class PcDeptUserLoginInput:LoginInput
    {
        /// <summary>
        /// 验证码
        /// </summary>
        [Required]
        public string VerifyCode { get; set; }

    }
}
