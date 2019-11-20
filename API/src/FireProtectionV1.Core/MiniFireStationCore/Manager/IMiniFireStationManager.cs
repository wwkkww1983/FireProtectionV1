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
        Task<SuccessOutput> Add(AddMiniFireStationInput input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> Update(UpdateMiniFireStationInput input);
        Task<SuccessOutput> AddJobUser(AddMiniFireJobUserDto input);
        Task<PagedResultDto<MiniFireEquipmentDto>> GetMiniFireEquipmentList(GetMiniFireListBaseInput input);
        Task<PagedResultDto<MiniFireActionDto>> GetActionList(GetMiniFireListBaseInput input);
        Task<List<string>> GetMiniFireEquipmentTypes();
        Task<SuccessOutput> AddMiniFireAction(MiniFireActionAddDto input);
        Task<List<MiniFireEquipmentNameOutput>> GetMiniFireEquipmentsByType(string type);
        Task<SuccessOutput> DeleteMiniFireAction(int miniFireActionId);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SuccessOutput> Delete(DeletMiniFireStationInput input);
        Task<SuccessOutput> DeleteJobUser(int jobUserId);
        Task<SuccessOutput> UpdateMiniFireAction(MiniFireActionDetailDto input);
        Task<PagedResultDto<MiniFireJobUserDto>> GetMiniFireJobUser(GetMiniFireJobUserInput input);
        Task<SuccessOutput> UpdateJobUser(MiniFireJobUserDetailDto input);
        Task<MiniFireActionDetailDto> GetMiniFireAction(int miniFireActionId);

        /// <summary>
        /// 获取单个实体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MiniFireStationOutput> GetById(int id);

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<MiniFireStationOutput>> GetList(GetMiniFireStationListInput input);
        Task<SuccessOutput> AddMiniFireEquipment(AddMiniFireEquipmentDto input);
        Task<MiniFireJobUserDetailDto> GetJobUserDetail(int jobUserId);
        Task<SuccessOutput> UpdateMiniFireEquipment(MiniFireEquipmentDto input);
        Task<SuccessOutput> DeleteMiniFireEquipment(int miniFireEquipmentId);
        Task<MiniFireEquipmentDto> GetMiniFireEquipment(int miniFireEquipmentId);

        /// <summary>
        /// 微型消防站Excel导出
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<MiniFireStation>> GetStationExcel(GetMiniFireStationListInput input);

        /// <summary>
        /// 根据坐标点获取附近1KM直线距离内的微型消防站
        /// </summary>
        /// <param name="lng">经度，例如104.159203</param>
        /// <param name="lat">纬度，例如30.633145</param>
        /// <returns></returns>
        Task<List<GetNearbyStationOutput>> GetNearbyStation(decimal lng, decimal lat);
    }
}
