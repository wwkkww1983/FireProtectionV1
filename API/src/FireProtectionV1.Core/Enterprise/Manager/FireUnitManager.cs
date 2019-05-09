using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Runtime.Caching;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Model;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Configuration;
using FireProtectionV1.Enterprise.Dto;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.Infrastructure.Model;
using FireProtectionV1.User.Manager;
using FireProtectionV1.User.Model;

namespace FireProtectionV1.Enterprise.Manager
{
    public class FireUnitManager : DomainService, IFireUnitManager
    {
        IRepository<SafeUnit> _safeUnitRep;
        IRepository<Area> _areaRep;
        IRepository<FireUnitType> _fireUnitTypeRep;
        IRepository<FireUnit> _fireUnitRep;
        IRepository<FireUnitUser> _fireUnitUserRep;
        IFireUnitUserManager _fireUnitAccountManager;
        ICacheManager _cacheManager;
        public FireUnitManager(
            IRepository<SafeUnit> safeUnitR,
            IRepository<Area> areaR,
            IRepository<FireUnitType> fireUnitTypeR,
            IRepository<FireUnit> fireUnitInfoRepository, IRepository<FireUnitUser> fireUnitAccountRepository,
            IFireUnitUserManager fireUnitAccountManager,
            ICacheManager cacheManager
            )
        {
            _safeUnitRep = safeUnitR;
            _areaRep = areaR;
            _fireUnitTypeRep = fireUnitTypeR;
            _fireUnitRep = fireUnitInfoRepository;
            _fireUnitUserRep = fireUnitAccountRepository;
            _fireUnitAccountManager = fireUnitAccountManager;
            _cacheManager = cacheManager;
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
                            Id = a.Id,
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
                            Id = a.Id,
                            Name = a.Name,
                            Address = a.Address
                        };
            var list = query
                .Skip(input.SkipCount).Take(input.MaxResultCount)
                .ToList();
            var tCount = fireUnits.Count();

            return Task.FromResult(new PagedResultDto<GetFireUnitListForMobileOutput>(tCount, list));
        }

        /// <summary>
        /// 添加防火单位（同时会添加防火单位管理员账号）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> Add(AddFireUnitInput input)
        {
            await _fireUnitAccountManager.Add(input.accountInput);

            var fireUnitInfo = new FireUnit()
            {
                Name = input.Name
            };

            return await _fireUnitRep.InsertAndGetIdAsync(fireUnitInfo);
        }

        /// <summary>
        /// 修改防火单位信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task Update(UpdateFireUnitInput input)
        {
            
        }

        /// <summary>
        /// 防火单位信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnitInfoOutput> GetFireUnitInfo(GetFireUnitInfoInput input)
        {
            GetFireUnitInfoOutput output = new GetFireUnitInfoOutput();
            var f = await _fireUnitRep.SingleAsync(p => p.Id.Equals(input.Id));
            if (f != null)
            {
                output.Name = f.Name;
                output.Address = f.Address;
                var a =await _areaRep.SingleAsync(p => p.Id.Equals(f.AreaId));
                if(a!=null)
                {
                    var codes = a.AreaPath.Split('-');
                    output.Area = "";
                    foreach(var code in codes)
                    {
                        var area = await _areaRep.SingleAsync(p => p.AreaCode.Equals(code));
                        output.Area += area.Name;
                    }

                }
                var type =await _fireUnitTypeRep.SingleAsync(p => p.Id == f.TypeId);
                if (type != null)
                    output.Type = type.Name;
                if (f.SafeUnitId != 0)
                {
                    var safe = await _safeUnitRep.SingleAsync(p => p.Id == f.SafeUnitId);
                    if (safe != null)
                        output.SafeUnit = safe.Name;
                }
            }
            return output;
        }

        /// <summary>
        /// 根据ID获取防火单位信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<FireUnit> Get(int id)
        {
            return await _cacheManager
                        .GetCache("FireUnit")
                        .GetAsync(id.ToString(), () => _fireUnitRep.GetAsync(id)) as FireUnit;
        }

        /// <summary>
        /// 根据ID删除防火单位
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            await _fireUnitRep.DeleteAsync(id);
        }

    }
}
