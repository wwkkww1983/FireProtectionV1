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
        /// <summary>
        /// 新增值班记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddNewDuty(AddNewDutyInput input);
        IQueryable<DataToDuty> GetDutyDataAll();
        IQueryable<DataToDuty> GetDutyDataMonth(int year, int month);
        IQueryable<DataToDuty> GetDutyDataDuration(DateTime start, DateTime end);
        IQueryable<FireUnitManualOuput> GetDutyFireUnitsAll(string filterName = null);
        IQueryable<FireUnitManualOuput> GetNoDuty1DayFireUnits();
    }
}
