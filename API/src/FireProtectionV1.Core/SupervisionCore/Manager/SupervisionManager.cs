using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.SupervisionCore.Dto;
using FireProtectionV1.SupervisionCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.SupervisionCore.Manager
{
    public class SupervisionManager : DomainService, ISupervisionManager
    {
        IRepository<Supervision> _supervisionRepository;
        IRepository<SupervisionItem> _supervisionItemRepository;
        IRepository<FireUnit> _fireUnitRepository;

        public SupervisionManager(IRepository<Supervision> supervisionRepository, IRepository<SupervisionItem> supervisionItemRepository, IRepository<FireUnit> fireUnitRepository)
        {
            _supervisionRepository = supervisionRepository;
            _supervisionItemRepository = supervisionItemRepository;
            _fireUnitRepository = fireUnitRepository;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<PagedResultDto<GetSupervisionListOutput>> GetList(GetSupervisionListInput input)
        {
            var supervisions = _supervisionRepository.GetAll();
            var expr = ExprExtension.True<Supervision>()
                .IfAnd(input.CheckResult != CheckResult.未指定, item => input.CheckResult.Equals(item.CheckResult));
            supervisions = supervisions.Where(expr);

            var fireUnits = _fireUnitRepository.GetAll();

            var query = from a in supervisions
                        join b in fireUnits
                        on a.FireUnitId equals b.Id
                        orderby a.CreationTime descending
                        select new GetSupervisionListOutput
                        {
                            Id = a.Id,
                            FireUnitName = b.Name,
                            CheckUser = a.CheckUser,
                            CreationTime = a.CreationTime,
                            CheckResult = a.CheckResult
                        };

            var list = query
                .Skip(input.SkipCount).Take(input.MaxResultCount)
                .ToList();
            var tCount = query.Count();

            return Task.FromResult(new PagedResultDto<GetSupervisionListOutput>(tCount, list));
        }

        /// <summary>
        /// 获取所有监管执法项目
        /// </summary>
        /// <returns></returns>
        public async Task<List<SupervisionItem>> GetSupervisionItem()
        {
            return await _supervisionItemRepository.GetAllListAsync();
        }
    }
}
