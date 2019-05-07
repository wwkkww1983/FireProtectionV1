using Abp.Application.Services.Dto;
using FireProtectionV1.Enterprise.Dto;
using FireProtectionV1.Enterprise.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.Enterprise
{
    public class FireDeptAppService: AppServiceBase
    {
        IFireDeptManager _manager;
        public FireDeptAppService(IFireDeptManager manager)
        {
            _manager = manager;
        }
        /// <summary>
        /// 防火单位分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetFireUnitListOutput>> GetFireUnitList(GetFireUnitListInput input)
        {
            return await _manager.GetFireUnitList(input);
        }
        /// <summary>
        /// 防火单位分页列表(手机端)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetFireUnitListForMobileOutput>> GetFireUnitListForMobile(GetFireUnitListInput input)
        {
            return await _manager.GetFireUnitListForMobile(input);
        }
    }
}
