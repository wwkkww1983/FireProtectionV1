using FireProtectionV1.Common.DBContext;
using FireProtectionV1.VersionCore.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.VersionCore.Dto
{
    public class GetAppOutput
    {
        /// <summary>
        /// App类型
        /// </summary>
        [Required]
        public AppType AppType { get; set; }

        /// <summary>
        /// 版本号(v1.0)
        /// </summary>        
        public string VersionNo { get; set; }

        /// <summary>
        /// app
        /// </summary>
        public string Apppath { get; set; }
    }

}
