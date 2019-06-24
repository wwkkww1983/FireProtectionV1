﻿using System;
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
        IRepository<FireUnitAttention> _fireUnitAttentionRep;
        IRepository<SafeUnit> _safeUnitRep;
        IRepository<Area> _areaRep;
        IRepository<FireUnitType> _fireUnitTypeRep;
        IRepository<FireUnit> _fireUnitRep;
        IRepository<FireUnitUser> _fireUnitUserRep;
        ICacheManager _cacheManager;
        public FireUnitManager(
            IRepository<FireUnitAttention> fireUnitAttentionRep,
            IRepository<SafeUnit> safeUnitR,
            IRepository<Area> areaR,
            IRepository<FireUnitType> fireUnitTypeR,
            IRepository<FireUnit> fireUnitInfoRepository, IRepository<FireUnitUser> fireUnitAccountRepository,
            IFireUnitUserManager fireUnitAccountManager,
            ICacheManager cacheManager
            )
        {
            _fireUnitAttentionRep = fireUnitAttentionRep;
            _safeUnitRep = safeUnitR;
            _areaRep = areaR;
            _fireUnitTypeRep = fireUnitTypeR;
            _fireUnitRep = fireUnitInfoRepository;
            _fireUnitUserRep = fireUnitAccountRepository;
            _cacheManager = cacheManager;
        }
        public Task<List<GetFireUnitTypeOutput>> GetFireUnitTypes()
        {
            var v =new List<GetFireUnitTypeOutput>(){ new GetFireUnitTypeOutput() { TypeId=0,TypeName="全部"}};
            var v2 = _fireUnitTypeRep.GetAll().Select(p => new GetFireUnitTypeOutput()
            {
                TypeId = p.Id,
                TypeName = p.Name
            }).ToList();
            ;
            return Task.FromResult<List<GetFireUnitTypeOutput>>(v.Union(v2).ToList());
        }
        /// <summary>
        /// 得到防火单位列表excel数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<List<GetFireUnitExcelOutput>> GetFireUnitListExcel(GetPagedFireUnitListInput input)
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
        public Task<PagedResultDto<GetFireUnitListOutput>> GetFireUnitList(GetPagedFireUnitListInput input)
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
                            InvitationCode = a.InvitationCode,
                            CreationTime = a.CreationTime.ToString("yyyy-MM-dd HH:mm:ss")
                        };
            var list = query
                .OrderByDescending(u=>u.CreationTime)
                .Skip(input.SkipCount).Take(input.MaxResultCount)
                .ToList();
            var tCount = fireUnits.Count();

            return Task.FromResult(new PagedResultDto<GetFireUnitListOutput>(tCount, list));
        }

        public Task<PagedResultDto<GetFireUnitListForMobileOutput>> GetFireUnitListForMobile(GetPagedFireUnitListInput input)
        {
            var fireUnits = _fireUnitRep.GetAll();
            var expr = ExprExtension.True<FireUnit>()
                .IfAnd(!string.IsNullOrEmpty(input.Name), item => item.Name.Contains(input.Name));
            fireUnits = fireUnits.Where(expr);

            var attenFireUnits = from a in fireUnits
                             join b in _fireUnitAttentionRep.GetAll().Where(p => p.FireDeptUserId == input.UserId)
                             on a.Id equals b.FireUnitId
                             orderby b.CreationTime descending
                             select a;
            var attentions = attenFireUnits.Select(p => new GetFireUnitListForMobileOutput()
            {
                IsAttention = true,
                Id = p.Id,
                Name = p.Name,
                Address = p.Address
            });
            var query = from a in fireUnits.Except(attenFireUnits)
                        join b in _fireUnitTypeRep.GetAll()
                        on a.TypeId equals b.Id into g
                        from b2 in g.DefaultIfEmpty()
                        orderby a.CreationTime descending
                        select new GetFireUnitListForMobileOutput()
                        {
                            IsAttention=false,
                            Id = a.Id,
                            Name = a.Name,
                            Address = a.Address
                        };
            var list = attentions.Union(query)
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
            var existflag = _fireUnitRep.GetAll().Where(u => u.Name == input.Name).Count();
            if(existflag!=0)
            {
                return new SuccessOutput() { Success = false, FailCause="防火单位："+input.Name+".已存在" };
            }
            await Task.Run(()=> _fireUnitRep.InsertAndGetId(new FireUnit()
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
                InvitationCode = MethodHelper.CreateInvitationCode().Trim(),
                Address=input.Address
            }));
            return new SuccessOutput() { Success = true };
        }
        /// <summary>
        /// 删除防火单位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> Delete(FireUnitIdInput input)
        {
            await _fireUnitRep.DeleteAsync(input.Id);
            return new SuccessOutput() { Success = true };
        }
        /// <summary>
        /// 修改防火单位信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> Update(UpdateFireUnitInput input)
        {
            var old= _fireUnitRep.GetAll().Where(u=>u.Id==input.Id).FirstOrDefault();
            old.Name = input.Name;
            old.TypeId = input.TypeId;
            old.AreaId = input.AreaId;
            old.Address = input.Address;
            old.ContractName = input.ContractName;
            old.ContractPhone = input.ContractPhone;
            old.SafeUnitId = input.SafeUnitId;
            old.Lng = input.Lng;
            old.Lat = input.Lat;

            await _fireUnitRep.UpdateAsync(old);
            return new SuccessOutput() { Success = true };
        }

        /// <summary>
        /// 防火单位信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnitInfoOutput> GetFireUnitInfo(GetFireUnitInfoInput input)
        {
            GetFireUnitInfoOutput output = new GetFireUnitInfoOutput();
            var att = _fireUnitAttentionRep.GetAll().Where(p => p.FireDeptUserId == input.UserId && p.FireUnitId == input.Id).FirstOrDefault();
            var f = await _fireUnitRep.SingleAsync(p => p.Id.Equals(input.Id));
            if (f != null)
            {
                output.IsAttention = att != null;
                output.Id = f.Id;
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
                    {
                        output.SafeUnit = safe.Name;
                        output.SafeUnitId = safe.Id;
                    }
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
        /// <summary>
        /// 消防部门用户关注防火单位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> AttentionFireUnit(DeptUserAttentionFireUnitInput input)
        {
            var attention = _fireUnitAttentionRep.GetAll().Where(p => p.FireDeptUserId == input.UserId && p.FireUnitId == input.FireUnitId)
                .FirstOrDefault();
            if (attention == null)
            {
                await _fireUnitAttentionRep.InsertAsync(new FireUnitAttention() { FireDeptUserId = input.UserId, FireUnitId = input.FireUnitId });
            }
            return new SuccessOutput() { Success = true };
        }
        /// <summary>
        /// 消防部门用户取消关注防火单位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> AttentionFireUnitCancel(DeptUserAttentionFireUnitInput input)
        {
            await _fireUnitAttentionRep.DeleteAsync(p => p.FireUnitId == input.FireUnitId && p.FireDeptUserId == input.UserId);
            return new SuccessOutput() { Success = true };
        }
        /// <summary>
        /// 地图加载所需使用到的防火单位列表数据
        /// </summary>
        /// <returns></returns>
        public Task<List<GetFireUnitMapListOutput>> GetMapList()
        {
            var fireUnits = _fireUnitRep.GetAll();

            var query = from a in fireUnits
                        select new GetFireUnitMapListOutput()
                        {
                            Id = a.Id,
                            Lat = a.Lat,
                            Lng = a.Lng
                        };

            var list = query.ToList();
            return Task.FromResult(list);
        }
    }
}
