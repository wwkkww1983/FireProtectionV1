using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    public class FireWaterDeviceType : EntityBase
    {
        /// <summary>
        /// 型号名称
        /// </summary>
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
