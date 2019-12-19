using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.SupervisionCore.Model
{
    /// <summary>
    /// 监督执法记录明细表
    /// </summary>
    public class SupervisionDetail : EntityBase
    {
        /// <summary>
        /// 监督执法记录Id
        /// </summary>
        public int SupervisionId { get; set; }
        /// <summary>
        /// 监督执法项目Id
        /// </summary>
        public int SupervisionItemId { get; set; }
        /// <summary>
        /// 是否合格
        /// </summary>
        public bool IsOK { get; set; }
    }
}
