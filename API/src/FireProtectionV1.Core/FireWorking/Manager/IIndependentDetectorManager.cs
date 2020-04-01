using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using FireProtectionV1.FireWorking.Dto.FireDevice;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.FireWorking.Manager
{
    public interface IIndependentDetectorManager : IDomainService
    {
        /// <summary>
        /// 新增独立式火警设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddIndependentDetector(AddIndependentDetectorInput input);
        /// <summary>
        /// 修改独立式火警设备信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateIndependentDetector(UpdateIndependentDetectorInput input);
        /// <summary>
        /// 刷新独立式火警设备当前电量及状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task RenewIndependentDetector(RenewIndependentDetectorInput input);
        /// <summary>
        /// 删除独立式火警设备
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        Task DeleteIndependentDetector(int detectorId);
        /// <summary>
        /// 获取单个独立式火警设备信息
        /// </summary>
        /// <param name="detectorId"></param>
        /// <returns></returns>
        Task<GetIndependentDetectorOutput> GetIndependentDetector(int detectorId);
        /// <summary>
        /// 获取独立式火警设备列表
        /// </summary>
        /// <param name="fireunitId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagedResultDto<GetIndependentDetectorListOutput>> GetIndependentDetectorList(int fireunitId, PagedResultRequestDto dto);
    }
}
