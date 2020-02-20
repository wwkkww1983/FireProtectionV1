using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class UpdateDetectorCoordinateInput
    {
        /// <summary>
        /// 部件Id
        /// </summary>
        public int DetectorId { get; set; }
        /// <summary>
        /// 火警点位图X坐标
        /// </summary>
        public double CoordinateX { get; set; }
        /// <summary>
        /// 火警点位图Y坐标
        /// </summary>
        public double CoordinateY { get; set; }
    }
}
