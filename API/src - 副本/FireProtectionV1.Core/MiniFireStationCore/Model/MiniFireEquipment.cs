using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.MiniFireStationCore.Model
{
    public class MiniFireEquipment : EntityBase
    {
        /// <summary>
        /// 微型消防站Id
        /// </summary>
        public int MiniFireStationId { get; set; }
        /// <summary>
        /// 微型消防站设施定义Id
        /// </summary>
        public int DefineId { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
    }
}
