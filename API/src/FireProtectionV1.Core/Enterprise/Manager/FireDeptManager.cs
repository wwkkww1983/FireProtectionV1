using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Enterprise.Dto;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.Infrastructure.Model;

namespace FireProtectionV1.Enterprise.Manager
{
    public class FireDeptManager:IFireDeptManager
    {
        IRepository<FireUnit> _fireUnitRep;
        IRepository<FireUnitType> _fireUnitTypeRep;
        public FireDeptManager(IRepository<FireUnit> fireUnitRep, IRepository<FireUnitType> fireUnitTypeRep)
        {
            _fireUnitRep = fireUnitRep;
            _fireUnitTypeRep = fireUnitTypeRep;
        }

        public Task<PagedResultDto<GetFireUnitListOutput>> GetFireUnitList(GetFireUnitListInput input)
        {
            var fireUnits = _fireUnitRep.GetAll();
            var expr = ExprExtension.True<FireUnit>()
                .IfAnd(!string.IsNullOrEmpty(input.Name), item => input.Name.Contains(item.Name));
            fireUnits = fireUnits.Where(expr);

            var query = from a in fireUnits
                       join b in _fireUnitTypeRep.GetAll()
                       on a.TypeId equals b.Id into g
                       from b2 in g.DefaultIfEmpty()
                       orderby a.CreationTime descending
                       select new GetFireUnitListOutput
                       {
                           Id=a.Id,
                           Name = a.Name,
                           Type = b2.Name,
                           ContractName = a.ContractName,
                           ContractPhone = a.ContractPhone,
                           InvitationCode = a.InvitationCode
                       };
            var list = query
                .Skip(input.SkipCount).Take(input.MaxResultCount)
                .ToList();
            var tCount = fireUnits.Count();

            return Task.FromResult(new PagedResultDto<GetFireUnitListOutput>(tCount, list));
        }

        public Task<PagedResultDto<GetFireUnitListForMobileOutput>> GetFireUnitListForMobile(GetFireUnitListInput input)
        {
            var fireUnits = _fireUnitRep.GetAll();
            var expr = ExprExtension.True<FireUnit>()
                .IfAnd(!string.IsNullOrEmpty(input.Name), item => input.Name.Contains(item.Name));
            fireUnits = fireUnits.Where(expr);

            var query = from a in fireUnits
                        join b in _fireUnitTypeRep.GetAll()
                        on a.TypeId equals b.Id into g
                        from b2 in g.DefaultIfEmpty()
                        orderby a.CreationTime descending
                        select new GetFireUnitListForMobileOutput
                        {
                            Id=a.Id,
                            Name = a.Name,
                            Address=a.Address
                        };
            var list = query
                .Skip(input.SkipCount).Take(input.MaxResultCount)
                .ToList();
            var tCount = fireUnits.Count();

            return Task.FromResult(new PagedResultDto<GetFireUnitListForMobileOutput>(tCount, list));
        }
    }
}
