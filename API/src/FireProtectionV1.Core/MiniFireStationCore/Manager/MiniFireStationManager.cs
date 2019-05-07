using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Helper;
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
        IRepository<MiniFireStation> _miniFireStationRepository;

        public MiniFireStationManager(IRepository<MiniFireStation> miniFireStationRepository)
        {
            _miniFireStationRepository = miniFireStationRepository;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> Add(AddMiniFireStationInput input)
        {
            Valid.Exception(_miniFireStationRepository.Count(m => input.Name.Equals(m.Name)) > 0, "保存失败：站点名称已存在");

            var entity = input.MapTo<MiniFireStation>();
            return await _miniFireStationRepository.InsertAndGetIdAsync(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            await _miniFireStationRepository.DeleteAsync(id);
        }

        /// <summary>
        /// 获取单个微型消防站信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MiniFireStation> GetById(int id)
        {
            return await _miniFireStationRepository.GetAsync(id);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<PagedResultDto<MiniFireStation>> GetList(GetMiniFireStationListInput input)
        {
            var miniFireStations = _miniFireStationRepository.GetAll();

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

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task Update(UpdateMiniFireStationInput input)
        {
            Valid.Exception(_miniFireStationRepository.Count(m => input.Name.Equals(m.Name) && !input.Id.Equals(m.Id)) > 0, "保存失败：站点名称已存在");

            var entity = input.MapTo<MiniFireStation>();
            await _miniFireStationRepository.UpdateAsync(entity);
        }
    }
}
