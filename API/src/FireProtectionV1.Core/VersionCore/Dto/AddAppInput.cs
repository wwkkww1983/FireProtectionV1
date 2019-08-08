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
    public class AddAppInput
    {
        /// <summary>
        /// App类型
        /// </summary>
        [Required]
        public AppType AppType { get; set; }

        /// <summary>
        /// 版本号(v1.0)
        /// </summary>
        [Required]
        public string VersionNo { get; set; }

        /// <summary>
        /// app
        /// </summary>
        [Required]
        public IFormFile App { get; set; }
    }

    [Export("APP类型")]
    public enum AppType
    {
        /// <summary>
        /// 监管部门
        /// </summary>
        [Description("0")]
        监管部门 = 0,
        /// <summary>
        /// 普通巡查
        /// </summary>
        [Description("1")]
        消火栓管理 = 1,
        /// <summary>
        /// 扫码巡查
        /// </summary>
        [Description("2")]
        防火单位管理 = 2,

    }
}
