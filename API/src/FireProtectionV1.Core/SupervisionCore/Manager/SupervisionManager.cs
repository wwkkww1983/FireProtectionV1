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
        IRepository<SupervisionDetail> _supervisionDetailRepository;
        IRepository<SupervisionDetailRemark> _supervisionDetailRemarkRepository;
        IRepository<FireUnit> _fireUnitRepository;

        public SupervisionManager(
            IRepository<Supervision> supervisionRepository,
            IRepository<SupervisionItem> supervisionItemRepository,
            IRepository<SupervisionDetail> supervisionDetailRepository,
            IRepository<SupervisionDetailRemark> supervisionDetailRemarkRepository,
            IRepository<FireUnit> fireUnitRepository)
        {
            _supervisionRepository = supervisionRepository;
            _supervisionItemRepository = supervisionItemRepository;
            _supervisionDetailRepository = supervisionDetailRepository;
            _supervisionDetailRemarkRepository = supervisionDetailRemarkRepository;
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
                .IfAnd(input.CheckResult != CheckResult.未指定, item => input.CheckResult.Equals(item.CheckResult))
                .IfAnd(input.FireUnitId != 0, item => input.FireUnitId.Equals(item.FireUnitId))
                .IfAnd(input.FireDeptUserId != 0, item => input.FireDeptUserId.Equals(item.FireDeptUserId));
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
                .OrderByDescending(item => item.CreationTime)
                .ToList();
            var tCount = query.Count();

            return Task.FromResult(new PagedResultDto<GetSupervisionListOutput>(tCount, list));
        }

        /// <summary>
        /// 获取单条执法记录明细项目信息
        /// </summary>
        /// <param name="supervisionId"></param>
        /// <returns></returns>
        public Task<List<GetSingleSupervisionDetailOutput>> GetSingleSupervisionDetail(int supervisionId)
        {
            var supervisionDetails = _supervisionDetailRepository.GetAll();
            var supervisionItems = _supervisionItemRepository.GetAll();
            var supervisionDetailRemarks = _supervisionDetailRemarkRepository.GetAll();

            var query = from a in supervisionDetails
                        join b in supervisionItems on a.SupervisionItemId equals b.Id
                        join c in supervisionDetailRemarks on a.Id equals c.SupervisionDetailId into r1
                        from dr1 in r1.DefaultIfEmpty()
                        where supervisionId.Equals(a.SupervisionId)
                        select new GetSingleSupervisionDetailOutput
                        {
                            Id = a.Id,
                            SupervisionItemId = a.SupervisionItemId,
                            SupervisionItemName = b.Name,
                            ParentId = b.ParentId,
                            ParentName = supervisionItems.Single(s => s.Id.Equals(b.ParentId)).Name,
                            SupervisionId = a.SupervisionId,
                            IsOK = a.IsOK,
                            Remark = dr1.Remark
                        };

            return Task.FromResult(query.ToList());
        }

        /// <summary>
        /// 获取单条记录主信息
        /// </summary>
        /// <param name="supervisionId"></param>
        /// <returns></returns>
        public Task<GetSingleSupervisionMainOutput> GetSingleSupervisionMain(int supervisionId)
        {
            var supervisions = _supervisionRepository.GetAll();
            var fireUnits = _fireUnitRepository.GetAll();

            var query = from a in supervisions
                        join b in fireUnits
                        on a.FireUnitId equals b.Id
                        where supervisionId.Equals(a.Id)
                        select new GetSingleSupervisionMainOutput
                        {
                            Id = a.Id,
                            FireUnitName = b.Name,
                            CheckUser = a.CheckUser,
                            CreationTime = a.CreationTime,
                            CheckResult = a.CheckResult,
                            DocumentDeadline = a.DocumentDeadline,
                            DocumentInspection = a.DocumentInspection,
                            DocumentMajor = a.DocumentMajor,
                            DocumentPunish = a.DocumentPunish,
                            DocumentReview = a.DocumentReview,
                            DocumentSite = a.DocumentSite,
                            Remark = a.Remark
                        };

            return Task.FromResult(query.Single());
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
