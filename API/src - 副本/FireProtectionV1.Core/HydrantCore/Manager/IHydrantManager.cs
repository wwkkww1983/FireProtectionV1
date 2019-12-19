using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using FireProtectionV1.HydrantCore.Dto;
using FireProtectionV1.HydrantCore.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.HydrantCore.Manager
{
    public interface IHydrantManager : IDomainService
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        Task<int> Add(AddHydrantInput input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Update(UpdateHydrantInput input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SuccessOutput> Delete(DeletHydrantInput id);

        /// <summary>
        /// Web端分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetPressureSubstandardOutput> GetListForWeb(GetHydrantListInput input);
        /// <summary>
        /// 水压低于标准值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetPressureSubstandardOutput> GetPressureSubstandard(GetHydrantListInput input);
        /// <summary>
        /// 消火栓Excel导出
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<GetHydrantListOutput>> GetHydrantExcel(GetHydrantListInput input);

        /// <summary>
        /// App端分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<Hydrant>> GetListForApp(GetHydrantListInput input);

        /// <summary>
        /// 获取单个实体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<HydrantDetailOutput> GetInfoById(int id);

        /// <summary>
        /// 获取最近30天报警记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<GetNearAlarmOutput>> GetNearbyAlarmById(GetHydrantAlarmInput input);

        /// <summary>
        /// 根据坐标点获取附近1KM直线距离内的消火栓
        /// </summary>
        /// <param name="lng">经度，例如104.159203</param>
        /// <param name="lat">纬度，例如30.633145</param>
        /// <returns></returns>
        Task<List<GetNearbyHydrantOutput>> GetNearbyHydrant(decimal lng, decimal lat);
    }
}
