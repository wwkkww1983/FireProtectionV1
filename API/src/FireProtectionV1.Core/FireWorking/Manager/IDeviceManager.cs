using Abp.Domain.Services;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.FireWorking.Manager
{
    public interface IDeviceManager : IDomainService
    {
        /// <summary>
        /// 新增探测器部件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<Detector> AddDetector(AddDetectorInput input);
        /// <summary>
        /// 新增网关设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddGateway(AddGatewayInput input);
        /// <summary>
        /// 根据identify查询探测器
        /// </summary>
        /// <param name="identify"></param>
        /// <returns></returns>
        Detector GetDetector(string identify);
    }
}
