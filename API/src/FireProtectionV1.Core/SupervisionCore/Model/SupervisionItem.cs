using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.SupervisionCore.Model
{
    /// <summary>
    /// 监督执法项目
    /// </summary>
    public class SupervisionItem : EntityBase
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 父级项目Id
        /// </summary>
        public int ParentId { get; set; }
    }
}
