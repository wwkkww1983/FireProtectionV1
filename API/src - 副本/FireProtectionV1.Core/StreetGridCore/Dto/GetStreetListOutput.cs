using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.StreetGridCore.Dto
{
    public class GetStreetListOutput
    {
        /// <summary>
        /// 网格员姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }
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
        /// 创建时间
        /// </summary>
        public string CreationTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool isDeleted { get; set; }
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }
    }
}
