using Abp.Domain.Services;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireProtectionV1.FireWorking.Manager
{
    public interface IPatrolManager : IDomainService
    {
        IQueryable<DataToPatrol> GetPatrolDataAll();
        IQueryable<DataToPatrol> GetPatrolDataMonth(int year, int month);
        IQueryable<DataToPatrol> GetPatrolDataDuration(DateTime start, DateTime end);
        IQueryable<FireUnitManualOuput> GetPatrolFireUnitsAll(string filterName = null);
        IQueryable<FireUnitManualOuput> GetNoPatrol7DayFireUnits();
    }
}
