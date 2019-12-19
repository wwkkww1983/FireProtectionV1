using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.MiniFireStationCore.Dto
{
    public class MiniFireEquipmentDto
    {
        /// <summary>
        /// 微型消防设施ID
        /// </summary>
        public int MiniFireEquipmentId { get; set; }
        /// <summary>
        /// 微型消防器材列表
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 微型消防器材名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
    }
    public class AddMiniFireEquipmentDto
    {
        /// <summary>
        /// 微型消防站ID
        /// </summary>
        public int MiniFireStationId { get; set; }
        /// <summary>
        /// 微型消防器材列表
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 微型消防器材名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
    }
}
