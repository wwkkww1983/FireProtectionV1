using Abp.Domain.Services;
using FireProtectionV1.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.Infrastructure.Manager
{
    public interface IAreaManager : IDomainService
    {
        /// <summary>
        /// 根据Id获取完整区域名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<string> GetFullAreaName(int id);
        /// <summary>
        /// 根据父级区域Id查询子级区域数组
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<GetAreaOutput>> GetAreas(GetAreaInput input);
    }
}
