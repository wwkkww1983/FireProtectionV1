using FireProtectionV1.User.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.User.Dto
{
    public class GetHyrantAreaForPCOutput
    {
        /// <summary>
        /// 父级区域ID
        /// </summary>
        public int AreaID { get; set; }
        /// <summary>
        /// 父级区域名称
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// 子级区域列表
        /// </summary>
        public List<GetHyrantAreaOutput> sonlist { get; set; }
    }
}
