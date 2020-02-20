using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetBitMapSetOutput
    {
        /// <summary>
        /// 建筑名称
        /// </summary>
        public string ArchitectureName { get; set; }
        /// <summary>
        /// 楼层名称
        /// </summary>
        public string FloorName { get; set; }
        /// <summary>
        /// 楼层平面图
        /// </summary>
        public string Floor_Picture { get; set; }
        /// <summary>
        /// 部件列表
        /// </summary>
        public List<BitMapSetDetector> BitMapSetDetectorList { get; set; }
    }
    public class BitMapSetDetector
    {
        /// <summary>
        /// 部件Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 部件地址
        /// </summary>
        public string Identify { get; set; }
        /// <summary>
        /// 部件类型
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 安装位置
        /// </summary>
        public string Location { get; set; }
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
