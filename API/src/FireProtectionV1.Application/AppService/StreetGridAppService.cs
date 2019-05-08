using Abp.Application.Services.Dto;
using FireProtectionV1.StreetGridCore.Dto;
using FireProtectionV1.StreetGridCore.Manager;
using FireProtectionV1.StreetGridCore.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    /// <summary>
    /// 网格
    /// </summary>
    public class StreetGridAppService : AppServiceBase
    {
        IStreetGridManager _manager;

        public StreetGridAppService(IStreetGridManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<StreetGrid>> GetList(GetStreetGridListInput input)
        {
            return await _manager.GetList(input);
        }
    }
}
