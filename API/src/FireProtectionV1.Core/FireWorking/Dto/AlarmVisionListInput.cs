using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class AlarmVisionListInput
    {
        /// <summary>
        /// 防火单位Id
        /// </summary>
        public int FireUnitId { get; set; }
        /// <summary>
        /// 报警类型
        /// </summary>
        public VisionAlarmType VisionAlarmType { get; set; }
    }
    public class AlarmVisionList_DeptInput
    {
        /// <summary>
        /// 监管部门Id
        /// </summary>
        public int FireDeptId { get; set; }
        /// <summary>
        /// 防火单位名称
        /// </summary>
        public string FireUnitName { get; set; }
        /// <summary>
        /// 报警类型
        /// </summary>
        public VisionAlarmType VisionAlarmType { get; set; }
    }
}
