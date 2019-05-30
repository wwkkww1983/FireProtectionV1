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
    public interface IStreetGridUserManager : IDomainService
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<GetStreetListOutput>> GetList(GetStreetGridUserListInput input);
        /// <summary>
        /// 街道网格Excel导出
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<StreetGridUser>> GetStreetGridExcel(GetStreetGridUserListInput input);
    }
}
