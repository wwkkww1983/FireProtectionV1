using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    /// <summary>
    /// 火警联网设施型号
    /// </summary>
    public class FireAlarmDeviceModel: EntityBase
    {
        /// <summary>
        /// 型号名称
        /// </summary>
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
