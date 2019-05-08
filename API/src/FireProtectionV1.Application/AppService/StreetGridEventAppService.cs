using Abp.Application.Services.Dto;
using FireProtectionV1.StreetGridCore.Dto;
using FireProtectionV1.StreetGridCore.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    public class StreetGridEventAppService : AppServiceBase
    {
        IStreetGridEventManager _manager;

        public StreetGridEventAppService(IStreetGridEventManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// 获取单个实体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GetEventByIdOutput> GetEventById(int id)
        {
            return await _manager.GetEventById(id);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetStreeGridEventListOutput>> GetList(GetStreetGridEventListInput input)
        {
            return await _manager.GetList(input);
        }
    }
}
