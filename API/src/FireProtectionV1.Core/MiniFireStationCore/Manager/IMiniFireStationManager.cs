using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using FireProtectionV1.MiniFireStationCore.Dto;
using FireProtectionV1.MiniFireStationCore.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.MiniFireStationCore.Manager
{
    public interface IMiniFireStationManager : IDomainService
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        Task<int> Add(AddMiniFireStationInput input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Update(UpdateMiniFireStationInput input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SuccessOutput> Delete(DeletMiniFireStationInput input);

        /// <summary>
        /// 获取单个实体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MiniFireStation> GetById(int id);

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<MiniFireStation>> GetList(GetMiniFireStationListInput input);

        /// <summary>
        /// 根据坐标点获取附近1KM直线距离内的微型消防站
        /// </summary>
        /// <param name="lng">经度，例如104.159203</param>
        /// <param name="lat">纬度，例如30.633145</param>
        /// <returns></returns>
        Task<List<GetNearbyStationOutput>> GetNearbyStation(decimal lng, decimal lat);
    }
}
