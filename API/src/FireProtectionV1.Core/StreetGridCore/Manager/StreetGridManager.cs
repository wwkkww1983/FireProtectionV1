using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.StreetGridCore.Dto;
using FireProtectionV1.StreetGridCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.StreetGridCore.Manager
{
    public class StreetGridUserManager : DomainService, IStreetGridUserManager
    {
        IRepository<StreetGridUser> _streetGridUserRepository;

        public StreetGridUserManager(IRepository<StreetGridUser> streetGridUserRepository)
        {
            _streetGridUserRepository = streetGridUserRepository;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<PagedResultDto<StreetGridUser>> GetList(GetStreetGridUserListInput input)
        {
            var streetGridUsers = _streetGridUserRepository.GetAll();

            var expr = ExprExtension.True<StreetGridUser>()
             .IfAnd(!string.IsNullOrEmpty(input.Name), item => item.Name.Contains(input.Name));

            streetGridUsers = streetGridUsers.Where(expr);

            List<StreetGridUser> list = streetGridUsers
                .OrderByDescending(item => item.CreationTime)
                .Skip(input.SkipCount).Take(input.MaxResultCount)
                .ToList();
            var tCount = streetGridUsers.Count();

            return Task.FromResult(new PagedResultDto<StreetGridUser>(tCount, list));
        }
    }
}
