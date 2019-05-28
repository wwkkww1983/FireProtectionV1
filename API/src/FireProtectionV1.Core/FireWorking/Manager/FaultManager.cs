using Abp.Domain.Repositories;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireProtectionV1.FireWorking.Manager
{
    public class FaultManager: IFaultManager
    {
        IRepository<FireUnit> _fireUnitRep;
        IRepository<Fault> _faultRep;

        public FaultManager(
            IRepository<FireUnit> fireUnitRep,
            IRepository<Fault> faultRep
            )
        {
            _fireUnitRep = fireUnitRep;
            _faultRep = faultRep;
        }
        public IQueryable<Fault> GetFaultDataAll()
        {
            return _faultRep.GetAll();
        }
        public IQueryable<Fault> GetFaultDataMonth(int year, int month)
        {
            return _faultRep.GetAll().Where(p => p.CreationTime.Year == year && p.CreationTime.Month == month);
        }
        public IQueryable<PendingFaultFireUnitOutput> GetPendingFaultFireUnits()
        {
            return from a in _faultRep.GetAll().GroupBy(p => p.FireUnitId).Select(p => new
                    {
                        FireUnitId = p.Key,
                        FaultCount = p.Count(),
                        PendingCount = p.Where(p1 => p1.ProcessState == 0).Count()
                    })
                   join b in _fireUnitRep.GetAll()
                   on a.FireUnitId equals b.Id
                   select new PendingFaultFireUnitOutput()
                   {
                       FireUnitId = a.FireUnitId,
                       FireUnitName = b.Name,
                       Count = a.FaultCount,
                       PendingCount = a.PendingCount
                   };
        }
    }
}
