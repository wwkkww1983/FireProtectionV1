using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class GetFireUnitListOutput
    {
        public int ID { get; set; }
        /// <summary>
        /// 防火单位名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 管理员账号（手机号）
        /// </summary>
        public string AdminUserAccount { get; set; }
        /// <summary>
        /// 管理员姓名
        /// </summary>
        public string AdminUseraName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
