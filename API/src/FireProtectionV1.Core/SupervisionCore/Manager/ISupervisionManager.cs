using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using FireProtectionV1.SupervisionCore.Dto;
using FireProtectionV1.SupervisionCore.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.SupervisionCore.Manager
{
    public interface ISupervisionManager : IDomainService
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<GetSupervisionListOutput>> GetList(GetSupervisionListInput input);

        /// <summary>
        /// 获得所有监管执法项目
        /// </summary>
        /// <returns></returns>
        Task<List<SupervisionItem>> GetSupervisionItem();
    }
}
