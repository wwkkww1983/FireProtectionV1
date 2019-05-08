using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.HydrantCore.Dto
{
    public class AddHydrantInput
    {
        /// <summary>
        /// 设施编号
        /// </summary>
        [Required]
        public string Sn { get; set; }
        /// <summary>
        /// 所属区域（街道）
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
    }
}
