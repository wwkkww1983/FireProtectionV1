using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.Infrastructure.Manager
{
    public interface IInfrastructureManager : IDomainService
    {
        /// <summary>
        /// 数据初始化
        /// </summary>
        /// <returns></returns>
        Task InitData();
    }
}
