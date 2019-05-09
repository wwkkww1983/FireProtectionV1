using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Manager;
using FireProtectionV1.Enterprise.Dto;
using FireProtectionV1.Enterprise.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    /// <summary>
    /// 防火单位服务
    /// </summary>
    public class FireUnitAppService : AppServiceBase
    {
        IFireUnitManager _fireUnitInfoManager;

        public FireUnitAppService(IFireUnitManager fireUnitInfoManager)
        {
            _fireUnitInfoManager = fireUnitInfoManager;
        }
        /// <summary>
        /// 防火单位分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetFireUnitListOutput>> GetFireUnitList(GetFireUnitListInput input)
        {
            return await _fireUnitInfoManager.GetFireUnitList(input);
        }
        /// <summary>
        /// 防火单位分页列表(手机端)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetFireUnitListForMobileOutput>> GetFireUnitListForMobile(GetFireUnitListInput input)
        {
            return await _fireUnitInfoManager.GetFireUnitListForMobile(input);
        }
        /// <summary>
        /// 添加防火单位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> Add(AddFireUnitInput input)
        {
            return await _fireUnitInfoManager.Add(input);
        }


        /// <summary>
        /// 防火单位信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnitInfoOutput> GetFireUnitInfo(GetFireUnitInfoInput input)
        {
            return await _fireUnitInfoManager.GetFireUnitInfo(input);
        }
    }
}
