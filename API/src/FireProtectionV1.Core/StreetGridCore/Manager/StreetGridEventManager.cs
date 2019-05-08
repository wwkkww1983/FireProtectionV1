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
    public class StreetGridEventManager : DomainService, IStreetGridEventManager
    {
        IRepository<StreetGrid> _streetGridRepository;
        IRepository<StreetGridEvent> _streetGridEventRepository;
        IRepository<StreetGridEventRemark> _streetGridEventRemarkRepository;

        public StreetGridEventManager(
            IRepository<StreetGrid> streetGridRepository, 
            IRepository<StreetGridEvent> streetGridEventRepository, 
            IRepository<StreetGridEventRemark> streetGridEventRemarkRepository)
        {
            _streetGridRepository = streetGridRepository;
            _streetGridEventRepository = streetGridEventRepository;
            _streetGridEventRemarkRepository = streetGridEventRemarkRepository;
        }

        /// <summary>
        /// 获取单个实体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<GetEventByIdOutput> GetEventById(int id)
        {
            var streetGridEvents = _streetGridEventRepository.GetAll();
            var streetGrids = _streetGridRepository.GetAll();
            var streetGridEventRemarks = _streetGridEventRemarkRepository.GetAll();

            var query = from a in streetGridEvents
                        join b in streetGrids on a.StreetGridId equals b.Id
                        join c in streetGridEventRemarks on a.Id equals c.StreetGridEventId into r1
                        from dr1 in r1.DefaultIfEmpty()
                        where id.Equals(a.Id)
                        select new GetEventByIdOutput
                        {
                            Id = a.Id,
                            Title = a.Title,
                            EventType = a.EventType,
                            StreetGridName = b.Name,
                            Street = b.Street,
                            Community = a.Community,
                            ContractName = b.ContractName,
                            ContractPhone = b.ContractPhone,
                            Status = a.Status,
                            CreationTime = a.CreationTime,
                            Remark = dr1.Remark
                        };

            var result = query.SingleOrDefault();
            return Task.FromResult<GetEventByIdOutput>(result);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<PagedResultDto<GetStreeGridEventListOutput>> GetList(GetStreetGridEventListInput input)
        {
            var streetGridEvents = _streetGridEventRepository.GetAll();
            var expr = ExprExtension.True<StreetGridEvent>()
                .IfAnd(input.Status!= EventStatus.未指定, item => input.Status.Equals(item.Status));
            streetGridEvents = streetGridEvents.Where(expr);

            var streetGrids = _streetGridRepository.GetAll();

            var query = from a in streetGridEvents
                        join b in streetGrids
                        on a.StreetGridId equals b.Id into g
                        from b2 in g.DefaultIfEmpty()
                        orderby a.CreationTime descending
                        select new GetStreeGridEventListOutput
                        {
                            Id = a.Id,
                            Title = a.Title,
                            EventType = a.EventType,
                            StreeGridName = b2.Name,
                            Street = b2.Street,
                            Community = a.Community,
                            CreationTime = a.CreationTime,
                            Status = a.Status
                        };

            var list = query
                .Skip(input.SkipCount).Take(input.MaxResultCount)
                .ToList();
            var tCount = query.Count();

            return Task.FromResult(new PagedResultDto<GetStreeGridEventListOutput>(tCount, list));
        }
    }
}
