using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto.FireDevice
{
    public class FireElectricDeviceItemDto
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public int DeviceId { get; set; }
        /// <summary>
        /// 设备SN
        /// </summary>
        public string DeviceSn { get; set; }
        /// <summary>
        /// 所在建筑Id
        /// </summary>
        public int FireUnitArchitectureId { get; set; }
        /// <summary>
        /// 所在建筑
        /// </summary>
        public string FireUnitArchitectureName { get; set; }
        /// <summary>
        /// 所在楼层Id
        /// </summary>
        public int FireUnitArchitectureFloorId { get; set; }
        /// <summary>
        /// 所在楼层
        /// </summary>
        public string FireUnitArchitectureFloorName { get; set; }
        /// <summary>
        /// 具体位置
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 监测项目数组
        /// </summary>
        public List<string> MonitorItem { get; set; }
        /// <summary>
        /// "单项"/"三项"
        /// </summary>
        public string PhaseType { get; set; }
        /// <summary>
        /// 当前L数值
        /// </summary>
        public string L { get; set; }
        /// <summary>
        /// 当前N数值
        /// </summary>
        public string N { get; set; }
        /// <summary>
        /// 当前电流数值
        /// </summary>
        public string A { get; set; }
        /// <summary>
        /// 当前L1数值
        /// </summary>
        public string L1 { get; set; }
        /// <summary>
        /// 当前L2数值
        /// </summary>
        public string L2 { get; set; }
        /// <summary>
        /// 当前L3数值
        /// </summary>
        public string L3 { get; set; }
    }
}
