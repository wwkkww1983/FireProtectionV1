using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    /// <summary>
    /// 火警联网设施通讯协议
    /// </summary>
    public class FireAlarmDeviceProtocol : EntityBase
    {
        /// <summary>
        /// 协议名称
        /// </summary>
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
