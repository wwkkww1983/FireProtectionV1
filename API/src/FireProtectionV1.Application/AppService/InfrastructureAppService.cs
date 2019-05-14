using FireProtectionV1.Infrastructure.Dto;
using FireProtectionV1.Infrastructure.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    /// <summary>
    /// 数据字典初始化
    /// </summary>
    public class InfrastructureAppService : AppServiceBase
    {
        IInfrastructureManager _manager;

        public InfrastructureAppService(IInfrastructureManager manager)
        {
            _manager = manager;
        }
        /// <summary>
                 /// 初始化数据(部署初始化)
                 /// </summary>
                 /// <returns></returns>
        public async Task InitData()
        {
            await _manager.InitData();
        }
    }
}
