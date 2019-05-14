using Abp.Application.Services.Dto;
using FireProtectionV1.SupervisionCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.SupervisionCore.Dto
{
    public class GetSupervisionListInput : PagedResultRequestDto
    {
        /// <summary>
        /// 执法检查结论
        /// </summary>
        public CheckResult CheckResult { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        public int FireUnitId { get; set; }
        /// <summary>
        /// 消防监管人员Id（记录提交人Id）
        /// </summary>
        public int FireDeptUserId { get; set; }
    }
}
