using Abp.Application.Services.Dto;
using FireProtectionV1.Enterprise.Dto;
using FireProtectionV1.Enterprise.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.Enterprise
{
    public class FireDepAppService: AppServiceBase
    {
        IFireDeptManager _manager;
        public FireDepAppService(IFireDeptManager manager)
        {
            _manager = manager;
        }
        /// <summary>
        /// 防火单位分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetFireUnitListOutput>> GetList(GetFireUnitListInput input)
        {
            return await _manager.GetList(input);
        }
    }
}
