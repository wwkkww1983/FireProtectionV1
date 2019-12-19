using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.MiniFireStationCore.Model
{
    public class MiniFireEquipmentDefine : EntityBase
    {
        /// <summary>
        /// 微型消防器材类别
        /// </summary>
        [MaxLength(50)]
        public string Type { get; set; }
        /// <summary>
        /// 微型消防器材名称
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
