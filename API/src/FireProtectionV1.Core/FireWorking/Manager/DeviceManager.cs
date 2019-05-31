using Abp.Domain.Repositories;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.FireWorking.Manager
{
    public class DeviceManager : IDeviceManager
    {
        IRepository<DetectorType> _detectorTypeRep;
        IRepository<Gateway> _gatewayRep;
        IRepository<Detector> _detectorRep;
        public DeviceManager(IRepository<Detector> detectorRep,
             IRepository<DetectorType> detectorTypeRep,
           IRepository<Gateway> gatewayRep)
        {
            _detectorTypeRep = detectorTypeRep;
            _detectorRep = detectorRep;
            _gatewayRep = gatewayRep;
        }
        /// <summary>
        /// 新增探测器部件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<Detector> AddDetector(AddDetectorInput input)
        {
            var gateway = _gatewayRep.GetAll().Where(p => p.Identify == input.GatewayIdentify).FirstOrDefault();
            if (gateway == null)
                return null;
            var type = _detectorTypeRep.GetAll().Where(p => p.GBType == input.DetectorGBType).FirstOrDefault();
            if (type == null)
                return null;
            return await _detectorRep.InsertAsync(new Detector()
            {
                DetectorTypeId = type.Id,
                FireSysType = gateway.FireSysType,
                Identify = input.Identify,
                Location =input.Location,
                GatewayId= gateway.Id,
                FireUnitId=gateway.FireUnitId,
            });
        }
        /// <summary>
        /// 新增网关设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddGateway(AddGatewayInput input)
        {
            await _gatewayRep.InsertAsync(new Gateway()
            {
                FireSysType = input.FireSysType,
                Identify = input.Identify,
                Location = input.Location,
                FireUnitId = input.FireUnitId
            });
        }
        /// <summary>
        /// 根据identify查询探测器
        /// </summary>
        /// <param name="identify"></param>
        /// <returns></returns>
        public Detector GetDetector(string identify)
        {
            return _detectorRep.GetAll().Where(p => p.Identify.Equals(identify)).FirstOrDefault();
        }
    }
}
