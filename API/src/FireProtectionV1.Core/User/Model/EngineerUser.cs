using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.User.Model
{
    public class EngineerUser : EntityBase
    {
        /// <summary>
        /// 账号（手机号）
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 负责区域（用于新增施工记录时，显示默认的区域）
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 消防监管部门Id（用于新增施工记录时，设置防火单位的FireDeptId）
        /// </summary>
        public int FireDeptId { get; set; }
    }
}
