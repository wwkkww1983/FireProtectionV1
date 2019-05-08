﻿using Abp.Application.Services.Dto;
using FireProtectionV1.Enterprise.Dto;
using FireProtectionV1.Enterprise.Manager;
using FireProtectionV1.Enterprise.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    /// <summary>
    /// 维保单位
    /// </summary>
    public class SafeUnitAppService : AppServiceBase
    {
        ISafeUnitManager _manager;

        public SafeUnitAppService(ISafeUnitManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> Add(AddSafeUnitInput input)
        {
            return await _manager.Add(input);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task Update(UpdateSafeUnitInput input)
        {
            await _manager.Update(input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            await _manager.Delete(id);
        }

        /// <summary>
        /// 获取单个实体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SafeUnit> GetById(int id)
        {
            return await _manager.GetById(id);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<SafeUnit>> GetList(GetSafeUnitListInput input)
        {
            return await _manager.GetList(input);
        }
    }
}