using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.MiniFireStationCore.Dto;
using FireProtectionV1.MiniFireStationCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.MiniFireStationCore.Manager
{
    public class MiniFireStationManager : DomainService, IMiniFireStationManager
    {
        IRepository<MiniFireStation> _MiniFireStationRepository;

        public MiniFireStationManager(IRepository<MiniFireStation> miniFireStationRepository)
        {
            _MiniFireStationRepository = miniFireStationRepository;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> Add(AddMiniFireStationInput input)
        {
            var entity = input.MapTo<MiniFireStation>();
            return await _MiniFireStationRepository.InsertAndGetIdAsync(entity);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<PagedResultDto<MiniFireStation>> GetList(GetMiniFireStationListInput input)
        {
            var miniFireStations = _MiniFireStationRepository.GetAll();

            var expr = ExprExtension.True<MiniFireStation>()
             .IfAnd(!string.IsNullOrEmpty(input.Name), item => input.Name.Contains(item.Name));

            miniFireStations = miniFireStations.Where(expr);

            List<MiniFireStation> list = miniFireStations
                .OrderByDescending(item => item.CreationTime)
                .Skip(input.SkipCount).Take(input.MaxResultCount)
                .ToList();
            var tCount = miniFireStations.Count();

            return Task.FromResult(new PagedResultDto<MiniFireStation>(tCount, list));
        }
    }
}
