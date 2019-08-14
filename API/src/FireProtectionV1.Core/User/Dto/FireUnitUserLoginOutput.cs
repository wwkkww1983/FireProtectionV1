using FireProtectionV1.User.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.User.Dto
{
    public class FireUnitUserLoginOutput : SuccessOutput
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 防火单位名称
        /// </summary>
        public string FireUnitName { get; set; }
        /// <summary>
        /// 防火单位ID
        /// </summary>
        public int FireUnitID { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public List<FireUnitRole> Rolelist { get; set; }
        /// <summary>
        /// 是否引导
        /// </summary>
        public bool GuideFlage { get; set; }
    }
}
