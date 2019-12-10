using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    /// <summary>
    /// 其他消防设施
    /// </summary>
    public class FireOrtherDevice : EntityBase
    {
        /// <summary>
        /// 防火单位ID
        /// </summary>
        public int FireUnitId { get; set; }
        /// <summary>
        /// 所属系统
        /// </summary>
        public int FireSystemId { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 设备型号
        /// </summary>
        public string DeviceType { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string DeviceSn { get; set; }
        /// <summary>
        /// 所在建筑
        /// </summary>
        public int FireUnitArchitectureId { get; set; }
        /// <summary>
        /// 所在楼层
        /// </summary>
        public int FireUnitArchitectureFloorId { get; set; }
        /// <summary>
        /// 设备地点
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Location { get; set; }
        /// <summary>
        /// 启用时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime ExpireTime { get; set; }
    }
}
