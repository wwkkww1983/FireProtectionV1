using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.MiniFireStationCore.Dto
{
    public class GetMiniFireEquipmentDefineOutput
    {
        /// <summary>
        /// 类别
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 设施名称
        /// </summary>
        public List<MiniFireEquipmentNameOutput> Equipment {get;set;}
    }
    public class MiniFireEquipmentNameOutput
    {
        /// <summary>
        /// 设施ID
        /// </summary>
        public int EquipmentId { get; set; }
        /// <summary>
        /// 设施名称
        /// </summary>
        public string Name { get; set; }
    }
}
