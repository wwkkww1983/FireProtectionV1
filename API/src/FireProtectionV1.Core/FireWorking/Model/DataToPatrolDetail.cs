using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    /// <summary>
    /// 巡查记录详情表
    /// </summary>
    public class DataToPatrolDetail : EntityBase
    {
        /// <summary>
        /// 巡查记录Id
        /// </summary>
        [Required]
        public int PatrolId { get; set; }
        /// <summary>
        /// 消防设施Id（扫码巡查时用到）
        /// </summary>
        public int DeviceId { get; set; }
        /// <summary>
        /// 记录状态
        /// </summary>
        public DutyOrPatrolStatus PatrolStatus { get; set; }
        /// <summary>
        /// 巡查地点
        /// </summary>
        public string PatrolAddress { get; set; }
        /// <summary>
        /// 巡查建筑Id
        /// </summary>
        public int ArchitectureId { get; set; }
        /// <summary>
        /// 巡查楼层Id
        /// </summary>
        public int FloorId { get; set; }
    }
}
