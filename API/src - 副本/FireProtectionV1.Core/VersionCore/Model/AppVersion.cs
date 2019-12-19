using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.VersionCore.Model
{
    public class AppVersion : EntityBase
    {
        /// <summary>
        /// App类型
        /// </summary>
        [Required]
        public byte AppType { get; set; }

        /// <summary>
        /// App路径
        /// </summary>
        public string AppPath { get; set; }

        /// <summary>
        /// 版本号(v1.0)
        /// </summary>
        public string VersionNo { get; set; }
    }
}
