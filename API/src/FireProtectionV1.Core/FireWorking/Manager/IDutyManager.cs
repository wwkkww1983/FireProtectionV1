using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.FireWorking.Manager
{
    public interface IDutyManager : IDomainService
    {
        IQueryable<DataToDuty> GetDutyDataAll();
        IQueryable<DataToDuty> GetDutyDataMonth(int year, int month);
        IQueryable<DataToDuty> GetDutyDataDuration(DateTime start, DateTime end);
        IQueryable<FireUnitManualOuput> GetDutyFireUnitsAll(string filterName = null);
        IQueryable<FireUnitManualOuput> GetNoDuty1DayFireUnits();
        /// <summary>
        /// 获取值班记录列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<GetDataDutyOutput>> GetDutylist(GetDataDutyInput input, PagedResultRequestDto dto);
        /// <summary>
        /// 获取值班记录详情
        /// </summary>
        /// <param name="dutyId"></param>
        /// <returns></returns>
        Task<GetDataDutyInfoOutput> GetDutyInfo(int dutyId);
        /// <summary>
        /// 新增值班记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddDutyInfo(AddDataDutyInfoInput input);
        /// <summary>
        /// 获取值班记录日历列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<GetDataDutyForCalendarOutput>> GetDutylistForCalendar(GetDataDutyForCalendarInput input);
        /// <summary>
        /// 获取值班记录状态统计
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetDutyStatusTotalOutput> GetDutyStateTotal(int fireUnitId);
    }
}
