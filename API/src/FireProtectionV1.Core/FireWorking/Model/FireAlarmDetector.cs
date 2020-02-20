using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    public class FireAlarmDetector : EntityBase
    {
        /// <summary>
        /// 部件编号
        /// </summary>
        [MaxLength(50)]
        public string Identify { get; set; }
        /// <summary>
        /// 探测器类型Id
        /// </summary>
        [Required]
        public int DetectorTypeId { get; set; }
        /// <summary>
        /// 火警联网设施Id
        /// </summary>
        [Required]
        public int FireAlarmDeviceId { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }
        /// <summary>
        /// 安装位置（例如2003室）
        /// </summary>
        [MaxLength(100)]
        public string Location { get; set; }
        /// <summary>
        /// 探测器状态，正常/故障
        /// </summary>
        public FireAlarmDetectorState State { get; set; }
        /// <summary>
        /// 所在楼层Id
        /// </summary>
        public int FireUnitArchitectureFloorId { get; set; }
        /// <summary>
        /// 火警点位图X坐标
        /// </summary>
        public double CoordinateX { get; set; }
        /// <summary>
        /// 火警点位图Y坐标
        /// </summary>
        public double CoordinateY { get; set; }
        /// <summary>
        /// 故障数量
        /// </summary>
        public int FaultNum { get; set; }
        /// <summary>
        /// 最后一次故障ID
        /// </summary>
        public int LastFaultId { get; set; }
        /// <summary>
        /// 安装位置全路径，包括建筑、楼层及具体位置，例如（行政楼20楼2003室）
        /// </summary>
        public string FullLocation { get; set; }
    }
}
