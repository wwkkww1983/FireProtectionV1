using Abp.Application.Services.Dto;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetDutyStatusTotalOutput
    {
        /// <summary>
        /// 正常数量
        /// </summary>
        public int NormalCount { get; set; }
        /// <summary>
        /// 绿色故障数量
        /// </summary>
        public int GreenFaultCount { get; set; }
        /// <summary>
        /// 橙色故障数量
        /// </summary>
        public int RedFaultCount { get; set; }
    }
}
