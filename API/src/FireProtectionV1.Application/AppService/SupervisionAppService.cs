using Abp.Application.Services.Dto;
using FireProtectionV1.SupervisionCore.Dto;
using FireProtectionV1.SupervisionCore.Manager;
using FireProtectionV1.SupervisionCore.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    public class SupervisionAppService : AppServiceBase
    {
        ISupervisionManager _manager;

        public SupervisionAppService(ISupervisionManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetSupervisionListOutput>> GetList(GetSupervisionListInput input)
        {
            return await _manager.GetList(input);
        }

        /// <summary>
        /// 获取单条执法记录明细项目信息
        /// </summary>
        /// <param name="supervisionId"></param>
        /// <returns></returns>
        public async Task<List<GetSingleSupervisionDetailOutput>> GetSingleSupervisionDetail(int supervisionId)
        {
            return await _manager.GetSingleSupervisionDetail(supervisionId);
        }

        /// <summary>
        /// 获取单条记录主信息
        /// </summary>
        /// <param name="supervisionId"></param>
        /// <returns></returns>
        public async Task<GetSingleSupervisionMainOutput> GetSingleSupervisionMain(int supervisionId)
        {
            return await _manager.GetSingleSupervisionMain(supervisionId);
        }

        /// <summary>
        /// 获取所有监管执法项目
        /// </summary>
        /// <returns></returns>
        public async Task<List<SupervisionItem>> GetSupervisionItem()
        {
            return await _manager.GetSupervisionItem();
        }

        /// <summary>
        /// 添加监管执法记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddSupervision(AddSupervisionInput input)
        {
            await _manager.AddSupervision(input);
        }
    }
}
