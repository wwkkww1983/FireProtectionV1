using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using FireProtectionV1.FireWorking.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.FireWorking.Manager
{
    public interface IVisionDeviceManager : IDomainService
    {
        /// <summary>
        /// 添加消防分析仪设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddVisionDevice(AddVisionDeviceInput input);
        /// <summary>
        /// 修改消防分析仪设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateVisionDevice(UpdateVisionDeviceInput input);
        /// <summary>
        /// 删除消防分析仪设备
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteVisionDevice(int id);
        /// <summary>
        /// 获得单个消防分析仪设备详细信息
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        Task<UpdateVisionDeviceInput> GetVisionDevice(int deviceId);
        /// <summary>
        /// 获取消防分析仪设备列表
        /// </summary>
        /// <param name="fireUnitId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagedResultDto<VisionDeviceItemDto>> GetVisionDeviceList(int fireUnitId, PagedResultRequestDto dto);
        /// <summary>
        /// 获取监控路数列表数据
        /// </summary>
        /// <param name="visionDeviceId"></param>
        /// <returns></returns>
        Task<List<VisionDetectorItemDto>> GetVisionDetectorList(int visionDeviceId);
        /// <summary>
        /// 修改监控路数列表数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateVisionDetectorList(List<VisionDetectorItemDto> input);
    }
}
