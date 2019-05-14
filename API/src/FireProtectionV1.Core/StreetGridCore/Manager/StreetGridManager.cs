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
    public class StreetGridManager : DomainService, IStreetGridManager
    {
        IRepository<StreetGrid> _streetGridRepository;

        public StreetGridManager(IRepository<StreetGrid> streetGridRepository)
        {
            _streetGridRepository = streetGridRepository;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<PagedResultDto<StreetGrid>> GetList(GetStreetGridListInput input)
        {
            var streetGrids = _streetGridRepository.GetAll();

            var expr = ExprExtension.True<StreetGrid>()
             .IfAnd(!string.IsNullOrEmpty(input.Name), item => item.Name.Contains(input.Name));

            streetGrids = streetGrids.Where(expr);

            List<StreetGrid> list = streetGrids
                .OrderByDescending(item => item.CreationTime)
                .Skip(input.SkipCount).Take(input.MaxResultCount)
                .ToList();
            var tCount = streetGrids.Count();

            return Task.FromResult(new PagedResultDto<StreetGrid>(tCount, list));
        }
    }
}
