using Abp.Application.Services.Dto;
using FireProtectionV1.AppService;
using FireProtectionV1.FireWorking.Dto.FireDevice;
using FireProtectionV1.FireWorking.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.DeviceService
{
    public class IndependentDetectorAppService : AppServiceBase
    {
        IIndependentDetectorManager _independentDetectorManager;
        public IndependentDetectorAppService(IIndependentDetectorManager independentDetectorManager)
        {
            _independentDetectorManager = independentDetectorManager;
        }
        /// <summary>
        /// 新增独立式火警设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddIndependentDetector(AddIndependentDetectorInput input)
        {
            _independentDetectorManager.AddIndependentDetector(input);
        }
        /// <summary>
        /// 修改独立式火警设备信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateIndependentDetector(UpdateIndependentDetectorInput input)
        {
            _independentDetectorManager.UpdateIndependentDetector(input);
        }
        /// <summary>
        /// 刷新独立式火警设备当前电量及状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task RenewIndependentDetector(RenewIndependentDetectorInput input)
        {
            _independentDetectorManager.RenewIndependentDetector(input);
        }
        /// <summary>
        /// 删除独立式火警设备
        /// </summary>
        /// <param name="detectorId"></param>
        /// <returns></returns>
        public async Task DeleteIndependentDetector(int detectorId)
        {
            _independentDetectorManager.DeleteIndependentDetector(detectorId);
        }
        /// <summary>
        /// 获取单个独立式火警设备信息
        /// </summary>
        /// <param name="detectorId"></param>
        /// <returns></returns>
        public async Task<GetIndependentDetectorOutput> GetIndependentDetector(int detectorId)
        {
            return await _independentDetectorManager.GetIndependentDetector(detectorId);
        }
        /// <summary>
        /// 获取独立式火警设备列表
        /// </summary>
        /// <param name="fireunitId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetIndependentDetectorListOutput>> GetIndependentDetectorList(int fireunitId, PagedResultRequestDto dto)
        {
            return await _independentDetectorManager.GetIndependentDetectorList(fireunitId, dto);
        }
    }
}
