using FireProtectionV1.User.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.User.Dto
{
    public class GetHyrantAreaOutput
    {
        /// <summary>
        /// 区域ID
        /// </summary>
        public int AreaID { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string AreaName { get; set; }
    }
}
