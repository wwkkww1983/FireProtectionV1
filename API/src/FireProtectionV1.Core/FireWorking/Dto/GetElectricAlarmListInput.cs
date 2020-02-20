using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetElectricAlarmListInput
    {
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }
        /// <summary>
        /// 接口访问的来源，如果是手机端调用接口，我会将所有警情记录更新为已读的状态
        /// </summary>
        [Required]
        public VisitSource VisitSource { get; set; }
        /// <summary>
        /// 报警状态：隐患/超限
        /// </summary>
        public FireElectricDeviceState State { get; set; }
    }
    public class GetElectricAlarmListForEngineerInput
    {
        /// <summary>
        /// 工程人员Id
        /// </summary>
        [Required]
        public int EngineerId { get; set; }
        /// <summary>
        /// 接口访问的来源，如果是手机端调用接口，我会将所有警情记录更新为已读的状态
        /// </summary>
        [Required]
        public VisitSource VisitSource { get; set; }
        /// <summary>
        /// 报警状态：隐患/超限
        /// </summary>
        public FireElectricDeviceState State { get; set; }
    }
}
