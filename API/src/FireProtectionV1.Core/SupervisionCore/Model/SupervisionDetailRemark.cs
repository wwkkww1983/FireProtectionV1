using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.SupervisionCore.Model
{
    public class SupervisionDetailRemark : EntityBase
    {
        /// <summary>
        /// 监督执法明细Id
        /// </summary>
        public int SupervisionDetailId { get; set; }
        /// <summary>
        /// 合格或不合格的备注信息
        /// </summary>
        public string Remark { get; set; }
    }
}
