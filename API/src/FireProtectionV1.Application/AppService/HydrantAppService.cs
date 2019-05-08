using Abp.Application.Services.Dto;
using FireProtectionV1.HydrantCore.Dto;
using FireProtectionV1.HydrantCore.Manager;
using FireProtectionV1.HydrantCore.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    /// <summary>
    /// 市政消火栓
    /// </summary>
    public class HydrantAppService : AppServiceBase
    {
        IHydrantManager _manager;

        public HydrantAppService(IHydrantManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> Add(AddHydrantInput input)
        {
            return await _manager.Add(input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            await _manager.Delete(id);
        }

        /// <summary>
        /// 获取实体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<HydrantDetailOutput> GetInfoById(int id)
        {
            return await _manager.GetInfoById(id);
        }

        /// <summary>
        /// App端分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<Hydrant>> GetListForApp(GetHydrantListInput input)
        {
            return await _manager.GetListForApp(input);
        }

        /// <summary>
        /// Web端分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetHydrantListOutput>> GetListForWeb(GetHydrantListInput input)
        {
            return await _manager.GetListForWeb(input);
        }

        /// <summary>
        /// 获取最近30天报警记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<HydrantAlarm>> GetNearbyAlarmById(int id)
        {
            return await _manager.GetNearbyAlarmById(id);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task Update(UpdateHydrantInput input)
        {
            await _manager.Update(input);
        }
    }
}
