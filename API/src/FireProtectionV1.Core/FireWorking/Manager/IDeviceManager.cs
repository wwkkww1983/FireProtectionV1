using Abp.Domain.Services;
using FireProtectionV1.Common.Enum;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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
        Task AddDetector(AddDetectorInput input);
        /// <summary>
        /// 新增网关设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddGateway(AddGatewayInput input);

        Task<Detector> GetDetectorAsync(int id);
        Detector GetDetector(string identify,string origin);
        Gateway GetGateway(string identify, string origin);
        DetectorType GetDetectorType(byte GBtype);
        Task<DetectorType> GetDetectorTypeAsync(int id);
        IQueryable<Detector> GetDetectorElectricAll();
        IQueryable<DetectorType> GetDetectorTypeAll();
        /// <summary>
        /// 查询指定防火单位和防火系统的所有探测器
        /// </summary>
        /// <param name="fireunitid"></param>
        /// <param name="fireSysType"></param>
        /// <returns></returns>
        IQueryable<Detector> GetDetectorAll(int fireunitid, FireSysType fireSysType);
    }
}
