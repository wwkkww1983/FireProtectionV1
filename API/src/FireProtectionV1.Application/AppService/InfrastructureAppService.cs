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
        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <returns></returns>
        public int RefreshData()
        {
            return _manager.RefreshData();
        }
        /// <summary>
        /// 增加市政消火栓
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public async Task AddHydrant(int num)
        {
            await _manager.AddHydrant(num); 
        }
    }
}
