using FireProtectionV1.User.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.User.Dto
{
    public class GetUnitPeopleOutput
    {
        /// <summary>
        /// 账号ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 账号（手机号）
        /// </summary>
        public string Account { get; set; }
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
        /// <summary>
        /// 角色
        /// </summary>
        public List<FireUnitRole> Rolelist { get; set; }

    }
}
