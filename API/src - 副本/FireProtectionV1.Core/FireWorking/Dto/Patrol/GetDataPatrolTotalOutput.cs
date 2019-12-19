using Abp.Application.Services.Dto;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetDataPatrolTotalOutput
    {
        /// <summary>
        /// 值班总计
        /// </summary>
        public int PatrolCount { get; set; }
        /// <summary>
        /// 问题总计
        /// </summary>
        public int ProplemCount { get; set; }
        /// <summary>
        /// 现场解决总计
        /// </summary>
        public int LiveSolutionCount { get; set; }
    }
}
