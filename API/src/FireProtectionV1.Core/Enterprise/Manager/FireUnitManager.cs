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
using FireProtectionV1.Account.Dto;
using FireProtectionV1.Account.Manager;
using FireProtectionV1.Account.Model;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Enterprise.Dto;
using FireProtectionV1.Enterprise.Model;

namespace FireProtectionV1.Enterprise.Manager
{
    public class FireUnitManager : DomainService, IFireUnitManager
    {
        IRepository<FireUnit> _fireUnitInfoRepository;
        IRepository<FireUnitAccount> _fireUnitAccountRepository;
        IFireUnitAccountManager _fireUnitAccountManager;
        ICacheManager _cacheManager;
        public FireUnitManager(
            IRepository<FireUnit> fireUnitInfoRepository, IRepository<FireUnitAccount> fireUnitAccountRepository,
            IFireUnitAccountManager fireUnitAccountManager,
            ICacheManager cacheManager
            )
        {
            _fireUnitInfoRepository = fireUnitInfoRepository;
            _fireUnitAccountRepository = fireUnitAccountRepository;
            _fireUnitAccountManager = fireUnitAccountManager;
            _cacheManager = cacheManager;
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

            return await _fireUnitInfoRepository.InsertAndGetIdAsync(fireUnitInfo);
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
        /// 防火单位分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<PagedResultDto<GetFireUnitListOutput>> GetList(GetFireUnitListInput input)
        {
            var fireUnitInfos = _fireUnitInfoRepository.GetAll();
            var fireUnitAccounts = _fireUnitAccountRepository.GetAll();

            var expr = ExprExtension.True<FireUnit>()
             .IfAnd(!string.IsNullOrEmpty(input.Name), item => input.Name.Contains(item.Name));
            fireUnitInfos = fireUnitInfos.Where(expr);

            var query = from i in fireUnitInfos
                        join a in fireUnitAccounts on i.Id equals a.FireUnitInfoID into a_join
                        from aj in a_join.DefaultIfEmpty()
                        select new GetFireUnitListOutput
                        {
                            ID = i.Id,
                            Name = i.Name,
                            AdminUserAccount = aj.Account,
                            AdminUseraName = aj.Name,
                            CreationTime = i.CreationTime
                        };

            List<GetFireUnitListOutput> list = query
                .OrderByDescending(item => item.CreationTime)
                .Skip(input.SkipCount).Take(input.MaxResultCount)
                .ToList();
            var tCount = query.Count();

            return Task.FromResult(new PagedResultDto<GetFireUnitListOutput>(tCount, list));
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
                        .GetAsync(id.ToString(), () => _fireUnitInfoRepository.GetAsync(id)) as FireUnit;
        }

        /// <summary>
        /// 根据ID删除防火单位
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            await _fireUnitInfoRepository.DeleteAsync(id);
        }
    }
}
