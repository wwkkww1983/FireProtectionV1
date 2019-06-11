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
        /// 添加监管执法记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddSupervision(AddSupervisionInput input)
        {
            var supervisionId = await _supervisionRepository.InsertAndGetIdAsync(input.Supervision);    // 综合信息
            foreach (var detail in input.SupervisionDetailInputs)
            {
                var supervisionDetal = new SupervisionDetail()
                {
                    SupervisionItemId = detail.SupervisionItemId,
                    IsOK = detail.IsOK,
                    SupervisionId = supervisionId
                };
                var detailId = await _supervisionDetailRepository.InsertAndGetIdAsync(supervisionDetal);    // 明细项目信息
                if (!string.IsNullOrEmpty(detail.Remark))
                {
                    var supervisionDetailRemark = new SupervisionDetailRemark()
                    {
                        SupervisionDetailId = detailId,
                        Remark = detail.Remark
                    };
                    await _supervisionDetailRemarkRepository.InsertAsync(supervisionDetailRemark);  // 明细项目备注信息
                }
            }
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
            var expr2 = ExprExtension.True<FireUnit>()
               .IfAnd(!string.IsNullOrEmpty(input.FireUnitName), item => item.Name.Contains(input.FireUnitName));

            fireUnits = fireUnits.Where(expr2);

            var query = from a in supervisions
                        join b in fireUnits
                        on a.FireUnitId equals b.Id
                        orderby a.CreationTime descending
                        select new GetSupervisionListOutput
                        {
                            Id = a.Id,
                            FireUnitName = b.Name,
                            CheckUser = a.CheckUser,
                            CreationTime = a.CreationTime.ToString("yyyy-MM-dd"),
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
        /// 导出监管EXCEL
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<List<GetSupervisionExcelOutput>> GetSupervisionListExcel(GetSupervisionListInput input)
        {
            var supervisions = _supervisionRepository.GetAll();
            var expr = ExprExtension.True<Supervision>()
                .IfAnd(input.CheckResult != CheckResult.未指定, item => input.CheckResult.Equals(item.CheckResult))
                .IfAnd(input.FireUnitId != 0, item => input.FireUnitId.Equals(item.FireUnitId))
                .IfAnd(input.FireDeptUserId != 0, item => input.FireDeptUserId.Equals(item.FireDeptUserId));
            supervisions = supervisions.Where(expr);

            var fireUnits = _fireUnitRepository.GetAll();
            var expr2 = ExprExtension.True<FireUnit>()
               .IfAnd(!string.IsNullOrEmpty(input.FireUnitName), item => item.Name.Contains(input.FireUnitName));

            fireUnits = fireUnits.Where(expr2);

            var query = from a in supervisions
                        join b in fireUnits
                        on a.FireUnitId equals b.Id
                        orderby a.CreationTime descending
                        select new GetSupervisionExcelOutput
                        {                        
                            FireUnitName = b.Name,
                            CreationTime = a.CreationTime,
                            CheckUser = a.CheckUser,
                            CheckResult = a.CheckResult
                        };

            return Task.FromResult<List<GetSupervisionExcelOutput>>(query.ToList());
        }
        /// <summary>
        /// 获取单条执法记录明细项目信息
        /// </summary>
        /// <param name="supervisionId"></param>
        /// <returns></returns>
        public Task<List<GetSingleSupervisionDetailOutput>> GetSingleSupervisionDetail(int supervisionId)
        {
            var supervisionItems = _supervisionItemRepository.GetAll();
            var supervisionDetails = _supervisionDetailRepository.GetAll().Where(s => s.SupervisionId.Equals(supervisionId));
            var supervisionDetailRemarks = _supervisionDetailRemarkRepository.GetAll();

            var queryParentList = (from a in supervisionItems
                                   join c in supervisionDetails on a.Id equals c.SupervisionItemId into r1
                                   from dr1 in r1.DefaultIfEmpty()
                                   join c in supervisionDetailRemarks on dr1.Id equals c.SupervisionDetailId into r2
                                   from dr2 in r2.DefaultIfEmpty()
                                   where a.ParentId == 0
                                   orderby a.Id
                                   select new GetSingleSupervisionDetailOutput
                                   {
                                       SupervisionId = supervisionId,
                                       SupervisionItemId = a.Id,
                                       SupervisionItemName = a.Name,
                                       ParentId = 0,
                                       ParentName = "",
                                       IsOK = dr1 == null ? true : dr1.IsOK,
                                       Remark = dr2 == null ? "" : dr2.Remark,
                                       SonList = null
                                   }).ToList();          
            foreach (var parent in queryParentList)
            {
                parent.SonList = new List<GetSingleSupervisionDetailOutput>();
                var sonList = from a in supervisionItems
                              join c in supervisionDetails on a.Id equals c.SupervisionItemId into r1
                              from dr1 in r1.DefaultIfEmpty()
                              join c in supervisionDetailRemarks on dr1.Id equals c.SupervisionDetailId into r2
                              from dr2 in r2.DefaultIfEmpty()
                              where a.ParentId == parent.SupervisionItemId
                              orderby a.Id
                              select new GetSingleSupervisionDetailOutput
                              {
                                  SupervisionId = supervisionId,
                                  SupervisionItemId = a.Id,
                                  SupervisionItemName = a.Name,
                                  ParentId = parent.SupervisionItemId,
                                  ParentName = parent.SupervisionItemName,
                                  IsOK = dr1 == null ? true : dr1.IsOK,
                                  Remark = dr2 == null ? "" : dr2.Remark,
                                  SonList = null
                              };
                parent.SonList.AddRange(sonList);
            }

            //var query = from a in supervisionItems
            //            join b in supervisionItems on a.ParentId equals b.Id
            //            join c in supervisionDetails on a.Id equals c.SupervisionItemId into r1
            //            from dr1 in r1.DefaultIfEmpty()
            //            join c in supervisionDetailRemarks on dr1.Id equals c.SupervisionDetailId into r2
            //            from dr2 in r2.DefaultIfEmpty()
            //            orderby a.ParentId
            //            select new GetSingleSupervisionDetailOutput
            //            {
            //                SupervisionId = supervisionId,
            //                SupervisionItemId = a.Id,
            //                SupervisionItemName = a.Name,
            //                ParentId = a.ParentId,
            //                ParentName = b.Name,
            //                IsOK = dr1 == null ? true : dr1.IsOK,
            //                Remark = dr2 == null ? "" : dr2.Remark
            //            };

            return Task.FromResult(queryParentList.ToList());


        }

        public IEnumerable<SupervisionItem> GetSonID(int p_id,IQueryable <SupervisionItem> supervisionItems)
        {
            var query = from c in supervisionItems
                        where c.ParentId == p_id
                        select c;

            return query.ToList().Concat(query.ToList().SelectMany(t => GetSonID(t.Id, query)));
        }

        public void GetTest()
        {
            var supervisionItems = _supervisionItemRepository.GetAll();
            var supervisionDetails = _supervisionDetailRepository.GetAll();

            var query = from a in supervisionItems
                        join b in supervisionDetails on a.Id equals b.SupervisionItemId into r1
                        from dr1 in r1.DefaultIfEmpty()
                        select new
                        {
                            SupervisionId = 1,
                            SupervisionItemId = a.Id,
                            SupervisionItemName = a.Name,
                            a.ParentId,
                            dr1.IsOK
                        };
            var list = query.ToList();
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
                            FireUnitId=b.Id,
                            CheckUser = a.CheckUser,
                            CreationTime = a.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
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
        public async Task<List<GetSupervisionItemOutput>> GetSupervisionItem()
        {
            var supervisionItems = _supervisionItemRepository.GetAll();

            var queryParentList = (from a in supervisionItems
                                   where a.ParentId == 0
                                   orderby a.Id
                                   select new GetSupervisionItemOutput
                                   {
                                       SupervisionItemId = a.Id,
                                       SupervisionItemName = a.Name,
                                       ParentId = 0,
                                       ParentName = "",
                                       SonList = null
                                   }).ToList();
            foreach (var parent in queryParentList)
            {
                parent.SonList = new List<GetSupervisionItemOutput>();
                var sonList = from a in supervisionItems
                              where a.ParentId == parent.SupervisionItemId
                              orderby a.Id
                              select new GetSupervisionItemOutput
                              {                                  
                                  SupervisionItemId = a.Id,
                                  SupervisionItemName = a.Name,
                                  ParentId = parent.SupervisionItemId,
                                  ParentName = parent.SupervisionItemName,
                                  SonList = null
                              };
                parent.SonList.AddRange(sonList);
            }
            return await Task.FromResult(queryParentList.ToList());
        }
    }
}
