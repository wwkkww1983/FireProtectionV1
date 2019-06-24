using Abp.Domain.Services;
using FireProtectionV1.Infrastructure.Dto;
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
        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <returns></returns>
        int RefreshData();
        /// <summary>
        /// 增加市政消火栓
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        Task AddHydrant(int num);
    }
}
