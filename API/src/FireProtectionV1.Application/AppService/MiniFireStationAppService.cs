using Abp.Application.Services.Dto;
using FireProtectionV1.MiniFireStationCore.Dto;
using FireProtectionV1.MiniFireStationCore.Manager;
using FireProtectionV1.MiniFireStationCore.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    public class MiniFireStationAppService : AppServiceBase
    {
        IMiniFireStationManager _manager;

        public MiniFireStationAppService(IMiniFireStationManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> Add(AddMiniFireStationInput input)
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
        /// 获取单个微型消防站信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MiniFireStation> GetById(int id)
        {
            return await _manager.GetById(id);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<MiniFireStation>> GetList(GetMiniFireStationListInput input)
        {
            return await _manager.GetList(input);
        }

        /// <summary>
        /// 根据坐标点获取附近1KM直线距离内的微型消防站
        /// </summary>
        /// <param name="lng">经度，例如104.159203</param>
        /// <param name="lat">纬度，例如30.633145</param>
        /// <returns></returns>
        public async Task<List<GetNearbyStationOutput>> GetNearbyStation(decimal lng, decimal lat)
        {
            return await _manager.GetNearbyStation(lng, lat);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task Update(UpdateMiniFireStationInput input)
        {
            await _manager.Update(input);
        }
    }
}
