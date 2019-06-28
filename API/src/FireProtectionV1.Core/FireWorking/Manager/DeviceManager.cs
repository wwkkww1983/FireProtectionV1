using Abp.Domain.Repositories;
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
        public async Task AddDetector(AddDetectorInput input)
        {
            var gateway = _gatewayRep.GetAll().Where(p => p.Identify == input.GatewayIdentify).FirstOrDefault();
            if (gateway == null)
                return ;
            var type = _detectorTypeRep.GetAll().Where(p => p.GBType == input.DetectorGBType).FirstOrDefault();
            if (type == null)
                return ;
             await _detectorRep.InsertAsync(new Detector()
            {
                DetectorTypeId = type.Id,
                FireSysType = gateway.FireSysType,
                Identify = input.Identify,
                Location =input.Location,
                GatewayId= gateway.Id,
                FireUnitId=gateway.FireUnitId,
                Origin=input.Origin
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
                FireUnitId = input.FireUnitId,
                Origin=input.Origin
            });
        }
        public async Task<DetectorType> GetDetectorTypeAsync(int id)
        {
            return await _detectorTypeRep.SingleAsync(p => p.Id == id);
        }
        public DetectorType GetDetectorType(byte GBtype)
        {
            return _detectorTypeRep.GetAll().Where(p => p.GBType == GBtype).FirstOrDefault();
        }
        public async Task<Detector> GetDetectorAsync(int id)
        {
            return await _detectorRep.SingleAsync(p=>p.Id==id);
        }
        public Detector GetDetector(string identify, string origin)
        {
            return _detectorRep.GetAll().Where(p => p.Identify.Equals(identify)&&p.Origin.Equals(origin)).FirstOrDefault();
        }
        public Gateway GetGateway(string gatewayIdentify, string origin)
        {
            return _gatewayRep.GetAll().Where(p => p.Identify.Equals(gatewayIdentify) && p.Origin.Equals(origin)).FirstOrDefault();
        }
        public IQueryable<Detector> GetDetectorAll(int fireunitid, FireSysType fireSysType)
        {
            return _detectorRep.GetAll().Where(p => p.FireSysType == (byte)fireSysType && p.FireUnitId==fireunitid);
        }
        public IQueryable<Detector> GetDetectorElectricAll()
        {
            return _detectorRep.GetAll().Where(p=>p.FireSysType==(byte)FireSysType.Electric);
        }
        public IQueryable<DetectorType> GetDetectorTypeAll()
        {
            return _detectorTypeRep.GetAll();
        }
    }
}
