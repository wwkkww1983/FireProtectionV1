using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetDetectorBitMapOutput
    {
        /// <summary>
        /// 接收火警时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 部件地址
        /// </summary>
        public string DetectorSn { get; set; }
        /// <summary>
        /// 部件类型名称
        /// </summary>
        public string DetectorTypeName { get; set; }
        /// <summary>
        /// 部件安装位置
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 楼层平面图
        /// </summary>
        public string FloorPicture { get; set; }
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
