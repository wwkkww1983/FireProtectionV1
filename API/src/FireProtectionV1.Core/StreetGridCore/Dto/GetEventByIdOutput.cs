using FireProtectionV1.StreetGridCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.StreetGridCore.Dto
{
    public class GetEventByIdOutput : StreetGridEvent
    {
        /// <summary>
        /// 网格名称
        /// </summary>
        public string StreetGridName { get; set; }
        /// <summary>
        /// 所属街道
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContractName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContractPhone { get; set; }
        /// <summary>
        /// 事件描述
        /// </summary>
        public string Remark { get; set; }
    }
}
