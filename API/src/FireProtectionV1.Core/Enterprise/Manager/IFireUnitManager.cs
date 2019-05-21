using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.Enterprise.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.Enterprise.Manager
{
    public interface IFireUnitManager : IDomainService
    {
        Task<List<GetFireUnitTypeOutput>> GetFireUnitTypes();
        /// <summary>
        /// 添加防火单位（同时会添加防火单位管理员账号）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> Add(AddFireUnitInput input);
        /// <summary>
        /// 修改防火单位信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> Update(UpdateFireUnitInput input);
        /// <summary>
        /// 删除防火单位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> Delete(FireUnitIdInput input);
        /// <summary>
        /// 防火单位信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetFireUnitInfoOutput> GetFireUnitInfo(GetFireUnitInfoInput input);
        Task<List<GetFireUnitExcelOutput>> GetFireUnitListExcel(GetPagedFireUnitListInput input);
        Task<PagedResultDto<GetFireUnitListOutput>> GetFireUnitList(GetPagedFireUnitListInput input);
        Task<PagedResultDto<GetFireUnitListForMobileOutput>> GetFireUnitListForMobile(GetPagedFireUnitListInput input);
        /// <summary>
        /// 消防部门用户关注防火单位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> AttentionFireUnit(DeptUserAttentionFireUnitInput input);
        /// <summary>
        /// 消防部门用户取消关注防火单位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> AttentionFireUnitCancel(DeptUserAttentionFireUnitInput input);

    }
}
