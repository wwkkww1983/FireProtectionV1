using FireProtectionV1.User.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.User.Dto
{
    public class FireUnitUserInput
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required]
        public string Account { get; set; }
        /// <summary>
        /// 密码（默认6个0）
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        [Required]
        public FireUnitRole Role { get; set; }
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
        public DateTime QualificationValidity { get; set; }
    }
}
