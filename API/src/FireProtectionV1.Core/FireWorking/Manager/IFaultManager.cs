using Abp.Domain.Services;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireProtectionV1.FireWorking.Manager
{
    public interface IFaultManager : IDomainService
    {
        IQueryable<Fault> GetFaultDataAll();
        IQueryable<Fault> GetFaultDataMonth(int year, int month);
        IQueryable<PendingFaultFireUnitOutput> GetPendingFaultFireUnits();
    }
}
