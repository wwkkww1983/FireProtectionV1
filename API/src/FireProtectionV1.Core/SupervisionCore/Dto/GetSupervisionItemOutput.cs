using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.SupervisionCore.Dto
{
    public class GetSupervisionItemOutput : EntityBase
    {
        /// <summary>
        /// 监督执法项目Id
        /// </summary>
        public int SupervisionItemId { get; set; }
        /// <summary>
        /// 监管执法项目名称
        /// </summary>
        public string SupervisionItemName { get; set; }
        /// <summary>
        /// 父级项目Id
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 父级项目名称
        /// </summary>
        public string ParentName { get; set; }
        /// <summary>
        /// 二级执法项目
        /// </summary>
        public List<GetSupervisionItemOutput> SonList { get; set; }
    }
}
