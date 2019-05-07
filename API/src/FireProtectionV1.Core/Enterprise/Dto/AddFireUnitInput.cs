﻿using FireProtectionV1.User.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class AddFireUnitInput
    {
        /// <summary>
        /// 防火单位管理员账号
        /// </summary>
        public FireUnitUserInput accountInput { get; set; }

        /// <summary>
        /// 防火单位名称
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}
