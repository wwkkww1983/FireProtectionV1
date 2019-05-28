using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using FireProtectionV1.Enterprise.Dto;
using FireProtectionV1.Enterprise.Model;
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

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<SafeUnit>> GetList(GetSafeUnitListInput input);
    }
}
