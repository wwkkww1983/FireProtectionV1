using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class ElectricAlarmListOutput
    {
        /// <summary>
        /// 接收警情时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 电气火灾设施Id
        /// </summary>
        public int FireElectricDeviceId { get; set; }
        /// <summary>
        /// 电气火灾设施编号
        /// </summary>
        public string FireElectricDeviceSn { get; set; }
        /// <summary>
        /// 防火单位名称
        /// </summary>
        public string FireUnitName { get; set; }
        /// <summary>
        /// 所在建筑
        /// </summary>
        public string FireUnitArchitectureName { get; set; }
        /// <summary>
        /// 所在楼层
        /// </summary>
        public string FireUnitArchitectureFloorName { get; set; }
        /// <summary>
        /// 具体位置
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 报警状态：隐患/超限
        /// </summary>
        public FireElectricDeviceState State { get; set; }
        /// <summary>
        /// 记录的类型标记：A/L/N/L1/L2/L3，其中A代表剩余电流，其它代表电缆温度
        /// </summary>
        public string Sign { get; set; }
        /// <summary>
        /// 模拟量值
        /// </summary>
        public string Analog { get; set; }
        /// <summary>
        /// 是否已读（手机端未读的警情需加粗显示）
        /// </summary>
        public bool IsRead { get; set; }
    }
}
