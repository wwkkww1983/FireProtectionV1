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
        /// 添加微型消防站
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        Task<int> Add(AddMiniFireStationInput input);

        /// <summary>
        /// 修改微型消防站
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Update(UpdateMiniFireStationInput input);

        /// <summary>
        /// 删除微型消防站
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(int id);

        /// <summary>
        /// 获取单个微型消防站信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MiniFireStation> GetById(int id);

        /// <summary>
        /// 微型消防站分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<MiniFireStation>> GetList(GetMiniFireStationListInput input);
    }
}
