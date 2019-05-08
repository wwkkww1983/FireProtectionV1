using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.Enterprise.Manager
{
    public interface IAreaManager : IDomainService
    {
        /// <summary>
        /// 根据Id获取完整区域名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<string> GetFullAreaName(int id);
    }
}
