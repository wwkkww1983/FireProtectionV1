﻿using FireProtectionV1.Common.DBContext;
using FireProtectionV1.User.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.User.Dto
{
    public class AddUserInput
    {
        /// <summary>
        /// 账号（手机号）
        /// </summary>
        [Required]
        [Phone]
        public string Account { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 单位ID
        /// </summary>
        [Required]
        public int FireUnitInfoID { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        public string Photo { get; set; }
        /// <summary>
        /// 职业资格证书名称
        /// </summary>
        public string Qualification { get; set; }
        /// <summary>
        /// 职业资格证书编号
        /// </summary>
        public string QualificationNumber { get; set; }
        /// <summary>
        /// 职业资格证书有效期
        /// </summary>
        public string QualificationValidity { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public List<string> Rolelist { get; set; }
    }
}
