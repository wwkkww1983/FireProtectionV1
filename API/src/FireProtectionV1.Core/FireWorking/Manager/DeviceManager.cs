﻿using Abp.Domain.Repositories;
using DeviceServer.Tcp.Protocol;
using FireProtectionV1.Common.Enum;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Model;
using FireProtectionV1.SettingCore.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.FireWorking.Manager
{
    public class DeviceManager : IDeviceManager
    {
        IRepository<RecordOnline> _recordOnlineRep;
        IRepository<RecordAnalog> _recordAnalogRep;
        IFireSettingManager _fireSettingManager;
        IRepository<DetectorType> _detectorTypeRep;
        IRepository<Gateway> _gatewayRep;
        IRepository<Detector> _detectorRep;
        public DeviceManager(
            IRepository<RecordOnline> recordOnlineRep,
            IRepository<RecordAnalog> recordAnalogRep,
            IFireSettingManager fireSettingManager,
            IRepository<Detector> detectorRep,
             IRepository<DetectorType> detectorTypeRep,
           IRepository<Gateway> gatewayRep)
        {
            _recordOnlineRep = recordOnlineRep;
            _recordAnalogRep = recordAnalogRep;
            _fireSettingManager = fireSettingManager;
            _detectorTypeRep = detectorTypeRep;
            _detectorRep = detectorRep;
            _gatewayRep = gatewayRep;
        }
        /// <summary>
        /// 非模拟量探测器历史记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<RecordUnAnalogOutput> GetRecordUnAnalog(GetRecordDetectorInput input)
        {
            var output = new RecordUnAnalogOutput();
            var detector =await _detectorRep.SingleAsync(p => p.Id == input.DetectorId);
            var detectorType = await _detectorTypeRep.SingleAsync(p => p.Id == detector.DetectorTypeId);
            output.Name = detectorType.Name;
            output.Location = detector.Location;
            var state=_recordOnlineRep.GetAll().Where(p => p.DetectorId == input.DetectorId).OrderByDescending(p => p.CreationTime).FirstOrDefault();
            if(state!=null)
            {
                output.State = GatewayStatusNames.GetName((GatewayStatus)state.State);
                output.LastTimeStateChange = state.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            var onlines = _recordOnlineRep.GetAll().Where(p => p.DetectorId == input.DetectorId&&p.State==(sbyte)GatewayStatus.Offline && p.CreationTime >= input.Start && p.CreationTime <= input.End)
            .GroupBy(p => p.CreationTime.ToString("yyyy-MM-dd")).Select(p => new
            {
                Time = p.Key,
                Count = p.Count()
            });
            //报警包括UITD和下属部件的报警
            //var onlines = _f.GetAll().Where(p => p.DetectorId == input.DetectorId &&p. && p.CreationTime >= input.Start && p.CreationTime <= input.End)
            //.GroupBy(p => p.CreationTime.ToString("yyyy-MM-dd")).Select(p => new
            //{
            //    Time = p.Key,
            //    Count = p.Count()
            //});
            return output;
        }
        /// <summary>
        /// 模拟量探测器历史记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<RecordAnalogOutput> GetRecordAnalog(GetRecordDetectorInput input)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取防火单位的终端状态
        /// </summary>
        /// <param name="fireUnitId"></param>
        /// <returns></returns>
        public async Task<List<EndDeviceStateOutput>> GetFireUnitEndDeviceState(int fireUnitId, int option)
        {
            var uitd = (from a in _detectorRep.GetAll().Where(p => p.FireUnitId == fireUnitId && p.DetectorTypeId == GetDetectorType((byte)UnitType.UITD).Id
                       && (option == 0 ? true : (option == -1 ? p.State.Equals("离线") : !p.State.Equals("离线"))))
                        join b in _detectorTypeRep.GetAll()
                        on a.DetectorTypeId equals b.Id
                        select new EndDeviceStateOutput()
                        {
                            DetectorId = a.Id,
                            IsAnalog = false,
                           Name = b.Name,
                           Location = a.Location,
                           StateName = a.State,
                           Analog="-",
                           Standard="-"
                       }).ToList();
            var tem= (from a in _detectorRep.GetAll().Where(p => p.FireUnitId == fireUnitId && p.DetectorTypeId == GetDetectorType((byte)UnitType.ElectricTemperature).Id
                        && (option == 0 ? true : (option == -1 ? p.State.Equals("离线") : !p.State.Equals("离线"))))
                    join b in _detectorTypeRep.GetAll()
                     on a.DetectorTypeId equals b.Id
                     select new EndDeviceStateOutput()
                     {
                         IsAnalog=true,
                         DetectorId = a.Id,
                         Name = b.Name,
                         Location = a.Location,
                         StateName = a.State.Equals("离线")?"离线":"在线",
                         Analog= a.State.Equals("离线") ? "-":a.State
                     }).ToList();
            var setTem = await _fireSettingManager.GetByName("CableTemperature");
            foreach (var v in tem)
            {
                v.Standard = $"<={setTem.MaxValue}℃";
            }
            var ele = (from a in _detectorRep.GetAll().Where(p => p.FireUnitId == fireUnitId && p.DetectorTypeId == GetDetectorType((byte)UnitType.ElectricResidual).Id
                       && (option == 0 ? true : (option == -1 ? p.State.Equals("离线") : !p.State.Equals("离线"))))
                      join b in _detectorTypeRep.GetAll()
                      on a.DetectorTypeId equals b.Id
                      select new EndDeviceStateOutput()
                      {
                          IsAnalog = true,
                          DetectorId = a.Id,
                          Name = b.Name,
                          Location = a.Location,
                          StateName = a.State.Equals("离线") ? "离线" : "在线",
                          Analog = a.State.Equals("离线") ? "-" : a.State
                      }).ToList();
            var setEle = await _fireSettingManager.GetByName("ResidualCurrent");
            foreach (var v in ele)
            {
                v.Standard = $"<={setEle.MaxValue}mA";
            }
            return uitd.Union(tem).Union(ele).ToList();
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

        public async Task AddRecordAnalog(AddDataElecInput input)
        {
            var detector = GetDetector(input.Identify, input.Origin);
            await _recordAnalogRep.InsertAsync(new RecordAnalog()
            {
                Analog = input.Analog,
                DetectorId = detector.Id
            });
        }
    }
}
