using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using FireProtectionV1.StreetGridCore.Dto;
using FireProtectionV1.StreetGridCore.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.StreetGridCore.Manager
{
    public interface IStreetGridEventManager : IDomainService
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<GetStreeGridEventListOutput>> GetList(GetStreetGridEventListInput input);

        /// <summary>
        /// 获取单个实体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<GetEventByIdOutput> GetEventById(int id);
        /// <summary>
        /// 街道网格Excel导出
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<GetStreeGridEventListOutput>> GetStreetEventExcel(GetStreetGridEventListInput input);
    }
}
