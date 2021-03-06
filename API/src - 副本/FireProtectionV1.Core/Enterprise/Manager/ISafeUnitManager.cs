﻿using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using FireProtectionV1.Enterprise.Dto;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.User.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.Enterprise.Manager
{
    public interface ISafeUnitManager : IDomainService
    {
        /// <summary>
        /// 选择查询维保单位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<GetSafeUnitOutput>> GetSelectSafeUnits(GetSafeUnitInput input);

        /// <summary>
        /// 消防维保Excel导出
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<SafeUnit>> GetSafeUnitsExcel(GetSafeUnitListInput input);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        Task<int> Add(AddSafeUnitInput input);
        Task<SuccessOutput> UserRegist(SafeUnitUserRegistInput input);
        Task<SafeUserLoginOutput> UserLogin(LoginInput input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Update(UpdateSafeUnitInput input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> Delete(DeletFireUnitInput input);

        /// <summary>
        /// 获取单个实体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SafeUnit> GetById(int id);
        Task<SuccessOutput> ChangePassword(ChangeUserPassword input);
        Task<SuccessOutput> InvitatVerify(InvitatVerifyDto input);

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<SafeUnit>> GetList(GetSafeUnitListInput input);
        Task<PagedResultDto<FireUnitSafe>> GetSafeUnitUserEvent(int userId, PagedResultRequestDto page);
        Task<PagedResultDto<UnitNameAndIdDto>> GetAllFireUnitOfSafe(int safeUnitId, PagedResultRequestDto page);
        Task<SuccessOutput> AddSafeUserFireUnit(SafeUserFireUnitDto dto);
        Task<SuccessOutput> DelSafeUserFireUnit(SafeUserFireUnitDto dto);
    }
}
