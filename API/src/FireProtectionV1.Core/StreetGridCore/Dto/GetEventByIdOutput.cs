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
        public string GridName { get; set; }
        /// <summary>
        /// 所属街道
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// 所属社区
        /// </summary>
        public string Community { get; set; }
        /// <summary>
        /// 网格员
        /// </summary>
        public string GridUserName { get; set; }
        /// <summary>
        /// 网格员电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 事件描述
        /// </summary>
        public string Remark { get; set; }
    }
}
