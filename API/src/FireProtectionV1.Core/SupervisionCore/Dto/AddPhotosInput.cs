using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.SupervisionCore.Dto
{
    public class AddPhotosInput
    {
        /// <summary>
        /// 监管执法ID
        /// </summary>
        public int SupervisionID { get; set; }
        /// <summary>
        /// 图片base64
        /// </summary>
        public List<string> code64 { get; set; }
    }
}
