using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using FireProtectionV1.VersionCore.Dto;
using FireProtectionV1.VersionCore.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.VersionCore.Manager
{
    public class VersionManager: IDomainService,IVersionManager
    {
        IRepository<Suggest> _suggestRepository;

        public VersionManager(IRepository<Suggest> suggestRepository)
        {
            _suggestRepository = suggestRepository;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> Add(AddSuggestInput input)
        {
            var entity = input.MapTo<Suggest>();
            return await _suggestRepository.InsertAndGetIdAsync(entity);
        }
    }
}
