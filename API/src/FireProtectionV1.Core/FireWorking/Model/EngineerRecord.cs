using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    /// <summary>
    /// 施工记录表
    /// </summary>
    public class EngineerRecord : EntityBase
    {
        /// <summary>
        /// 工程人员Id
        /// </summary>
        public int EngineerUserId { get; set; }
        /// <summary>
        /// 电气火灾设施Id
        /// </summary>
        public int FireElectricDeviceId { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        public int FireUnitId { get; set; }
    }
}
