using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.User.Dto
{
    public class UserRegistInput
    {
        /// <summary>
        /// 防火单位名称
        /// </summary>
        [Required]
        public string FireUnitName { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        [Required]
        public string UserName { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        [Required]
        [Phone]
        public string Phone { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string Password { get; set; }
        /// <summary>
        /// 邀请码
        /// </summary>
        [Required]
        public string InvitatCode { get; set; }
    }
}
