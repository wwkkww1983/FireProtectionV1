using FireProtectionV1.SupervisionCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireProtectionV1.SupervisionCore.Dto
{
    public class GetSingleSupervisionDetailOutput : SupervisionDetail
    {
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
        /// 项目的注释
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 二级执法项目
        /// </summary>
        public List<GetSingleSupervisionDetailOutput> SonList { get; set; }
    }
}
