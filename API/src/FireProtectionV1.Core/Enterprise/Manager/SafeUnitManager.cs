using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Helper;
using FireProtectionV1.Enterprise.Dto;
using FireProtectionV1.Enterprise.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.Enterprise.Manager
{
    public class SafeUnitManager : DomainService, ISafeUnitManager
    {
        IRepository<SafeUnit> _safeUnitRepository;

        public SafeUnitManager(IRepository<SafeUnit> safeUnitRepository)
        {
            _safeUnitRepository = safeUnitRepository;
        }
        /// <summary>
        /// 查询维保单位(模糊查询)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<List<GetSafeUnitOutput>> GetSafeUnits(GetSafeUnitInput input)
        {
            var query = _safeUnitRepository.GetAll().Where(p => string.IsNullOrEmpty(input.Name) ? true : p.Name.Contains(input.Name))
                .Select(p => new GetSafeUnitOutput()
                {
                    SafeUnitId = p.Id,
                    SafeUnitName = p.Name
                });
            return Task.FromResult<List<GetSafeUnitOutput>>(query.ToList());
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> Add(AddSafeUnitInput input)
        {
            Valid.Exception(_safeUnitRepository.Count(m => input.Name.Equals(m.Name)) > 0, "保存失败：单位名称已存在");

            var entity = input.MapTo<SafeUnit>();
            return await _safeUnitRepository.InsertAndGetIdAsync(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            await _safeUnitRepository.DeleteAsync(id);
        }

        /// <summary>
        /// 获取单个信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SafeUnit> GetById(int id)
        {
            return await _safeUnitRepository.GetAsync(id);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<PagedResultDto<SafeUnit>> GetList(GetSafeUnitListInput input)
        {
            var safeUnits = _safeUnitRepository.GetAll();

            var expr = ExprExtension.True<SafeUnit>()
             .IfAnd(!string.IsNullOrEmpty(input.Name), item => item.Name.Contains(input.Name));

            safeUnits = safeUnits.Where(expr);

            List<SafeUnit> list = safeUnits
                .OrderByDescending(item => item.CreationTime)
                .Skip(input.SkipCount).Take(input.MaxResultCount)
                .ToList();
            var tCount = safeUnits.Count();

            return Task.FromResult(new PagedResultDto<SafeUnit>(tCount, list));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task Update(UpdateSafeUnitInput input)
        {
            Valid.Exception(_safeUnitRepository.Count(m => input.Name.Equals(m.Name) && !input.Id.Equals(m.Id)) > 0, "保存失败：单位名称已存在");

            var entity = input.MapTo<SafeUnit>();
            await _safeUnitRepository.UpdateAsync(entity);
        }
    }
}
