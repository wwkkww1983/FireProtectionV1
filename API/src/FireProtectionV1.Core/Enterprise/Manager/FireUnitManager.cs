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
using FireProtectionV1.Common.Helper;

namespace FireProtectionV1.Enterprise.Manager
{
    public class FireUnitManager : DomainService, IFireUnitManager
    {
        IRepository<SafeUnit> _safeUnitRep;
        IRepository<Area> _areaRep;
        IRepository<FireUnitType> _fireUnitTypeRep;
        IRepository<FireUnit> _fireUnitRep;
        IRepository<FireUnitUser> _fireUnitUserRep;
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
            _cacheManager = cacheManager;
        }
        public Task<List<GetFireUnitTypeOutput>> GetFireUnitTypes()
        {
            return Task.FromResult<List<GetFireUnitTypeOutput>>(
                _fireUnitTypeRep.GetAll().Select(p => new GetFireUnitTypeOutput()
                {
                    TypeId = p.Id,
                    TypeName = p.Name
                }).ToList());
        }
        /// <summary>
        /// 得到防火单位列表excel数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<List<GetFireUnitExcelOutput>> GetFireUnitListExcel(GetFireUnitListInput input)
        {
            var fireUnits = _fireUnitRep.GetAll();
            var expr = ExprExtension.True<FireUnit>()
                .IfAnd(!string.IsNullOrEmpty(input.Name), item => item.Name.Contains(input.Name));
            fireUnits = fireUnits.Where(expr);

            var query = from a in fireUnits
                        join b in _fireUnitTypeRep.GetAll()
                        on a.TypeId equals b.Id into g
                        from b2 in g.DefaultIfEmpty()
                        orderby a.CreationTime descending
                        join c in _areaRep.GetAll()
                        on a.AreaId equals c.Id
                        join d in _safeUnitRep.GetAll()
                        on a.SafeUnitId equals d.Id
                        select new GetFireUnitExcelOutput
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Type = b2.Name,
                            Area = c.Name,
                            ContractName = a.ContractName,
                            ContractPhone = a.ContractPhone,
                            SafeUnit = d.Name,
                            InvitationCode = a.InvitationCode
                        };
            return Task.FromResult<List<GetFireUnitExcelOutput>>(query.ToList());
        }
        public Task<PagedResultDto<GetFireUnitListOutput>> GetFireUnitList(GetFireUnitListInput input)
        {
            var fireUnits = _fireUnitRep.GetAll();
            var expr = ExprExtension.True<FireUnit>()
                .IfAnd(!string.IsNullOrEmpty(input.Name), item => item.Name.Contains(input.Name));
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
                .IfAnd(!string.IsNullOrEmpty(input.Name), item => item.Name.Contains(input.Name));
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
        public async Task<SuccessOutput> Add(AddFireUnitInput input)
        {
            await _fireUnitRep.InsertAsync(new FireUnit()
            {
                CreationTime = DateTime.Now,
                Name = input.Name,
                Lng = input.Lng,
                Lat = input.Lat,
                AreaId = input.AreaId,
                SafeUnitId = input.SafeUnitId,
                TypeId = input.TypeId,
                ContractName = input.ContractName,
                ContractPhone = input.ContractPhone,
                InvitationCode = MethodHelper.CreateInvitationCode()
            });
            return new SuccessOutput() { Success = true };
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
                output.ContractName = f.ContractName;
                output.ContractPhone = f.ContractPhone;
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
