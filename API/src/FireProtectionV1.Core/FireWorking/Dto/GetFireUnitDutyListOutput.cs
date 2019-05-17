using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class GetFireUnitDutyListOutput
    {
        /// <summary>
        /// 超过1天没有巡查记录的单位数量
        /// </summary>
        public int NoWork1DayCount { get; set; }
        /// <summary>
        /// 巡检页面数据
        /// </summary>
        public PagedResultDto<FireUnitManualOuput> PagedResultDto { get; set; }
    }
}
