﻿using Abp.Domain.Repositories;
using Abp.Domain.Services;
using FireProtectionV1.BigScreen.Dto;
using FireProtectionV1.Common.Enum;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.FireWorking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.BigScreen.Manager
{
    public class FDBigScreenManager : DomainService, IFDBigScreenManager
    {
        IRepository<FireUnit> _repFireUnit;
        IRepository<AlarmToFire> _repAlarmToFire;
        IRepository<FireAlarmDevice> _repFireAlarmDevice;
        IRepository<FireAlarmDetector> _repFireAlarmDetector;
        IRepository<FireElectricDevice> _repFireElectricDevice;
        IRepository<IndependentDetector> _repIndependentDetector;
        public FDBigScreenManager(
            IRepository<FireUnit> repFireUnit,
            IRepository<AlarmToFire> repAlarmToFire,
            IRepository<FireAlarmDevice> repFireAlarmDevice,
            IRepository<FireAlarmDetector> repFireAlarmDetector,
            IRepository<IndependentDetector> repIndependentDetector,
            IRepository<FireElectricDevice> repFireElectricDevice)
        {
            _repFireUnit = repFireUnit;
            _repAlarmToFire = repAlarmToFire;
            _repFireAlarmDevice = repFireAlarmDevice;
            _repFireAlarmDetector = repFireAlarmDetector;
            _repFireElectricDevice = repFireElectricDevice;
            _repIndependentDetector = repIndependentDetector;
        }
        /// <summary>
        /// 监管部门数据大屏：获取电气火灾防护指标各状态数量
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public Task<GetElectricDeviceStatusNumDto> GetElectricDeviceStatusNum(int deptId)
        {
            var electricDevices = _repFireElectricDevice.GetAll();
            var fireUnits = _repFireUnit.GetAll().Where(item => item.FireDeptId.Equals(deptId));

            var query = from a in electricDevices
                        join b in fireUnits on a.FireUnitId equals b.Id
                        select new
                        {
                            Id = a.Id,
                            State = a.State
                        };
            int offLineNum = query.Count(item => item.State.Equals(FireElectricDeviceState.Offline));
            int goodNum = query.Count(item => item.State.Equals(FireElectricDeviceState.Good));
            int dangerNum = query.Count(item => item.State.Equals(FireElectricDeviceState.Danger));
            int transfiniteNum = query.Count(item => item.State.Equals(FireElectricDeviceState.Transfinite));

            return Task.FromResult(new GetElectricDeviceStatusNumDto()
            {
                OfflineNum = offLineNum,
                GoodNum = goodNum,
                DangerNum = dangerNum,
                TransfiniteNum = transfiniteNum
            });
        }
        /// <summary>
        /// 监管部门数据大屏：获取火警联网部件正常率
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public Task<GetFireDetectorStatusNumDto> GetFireDetectorStatusNum(int deptId)
        {
            var detectors = _repFireAlarmDetector.GetAll().Where(item => item.State.Equals(FireAlarmDetectorState.Fault));
            var fireUnits = _repFireUnit.GetAll().Where(item => item.FireDeptId.Equals(deptId));
            var fireAlarmDevices = _repFireAlarmDevice.GetAll().Where(item => item.NetDetectorNum > 0);

            var query = from a in fireAlarmDevices
                        join b in fireUnits on a.FireUnitId equals b.Id
                        select new
                        {
                            NetDetectorNum = a.NetDetectorNum
                        };
            int sumNetDetectorNum = query.Sum(item => item.NetDetectorNum); // 所有联网部件的数量

            var query1 = from a in detectors
                         join b in fireUnits on a.FireUnitId equals b.Id
                         join c in fireAlarmDevices on a.FireAlarmDeviceId equals c.Id
                         select new
                         {
                             Id = a.Id
                         };

            int faultDetectorNum = query1.Count();  // 故障部件数量
            decimal rate = 0;
            if (sumNetDetectorNum > 0 && faultDetectorNum > 0) rate = Math.Round((decimal)((sumNetDetectorNum - faultDetectorNum)) / sumNetDetectorNum, 2);

            return Task.FromResult(new GetFireDetectorStatusNumDto()
            {
                TotalNum = sumNetDetectorNum,
                FaultNum = faultDetectorNum,
                NormalRate = rate
            });
        }
        /// <summary>
        /// 监管部门数据大屏：获取防火单位地图点位列表
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public Task<List<GetFireunitLatForMapOutput>> GetFireunitLatForMap(int deptId)
        {
            //var lst = _repFireUnit.GetAll().Where(item => item.FireDeptId.Equals(deptId)).Select(item => new GetFireunitLatForMapOutput()
            //{
            //    FireunitId = item.Id,
            //    Lat = item.Lat,
            //    Lng = item.Lng,
            //    ExistTrueFireAlarm = false
            //}).ToList();

            //var fireAlarms = _repAlarmToFire.GetAll().Where(item => item.CheckState.Equals(FireAlarmCheckState.True)).ToList();
            //foreach(var item in fireAlarms)
            //{
            //    var fireunit = lst.FirstOrDefault(f => f.FireunitId.Equals(item.FireUnitId));
            //    if (fireunit != null) lst[]
            //}
            var dt = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01"));
            var fireUnits = _repFireUnit.GetAll().Where(item => item.FireDeptId.Equals(deptId));
            var fireAlarms = _repAlarmToFire.GetAll().Where(item => item.CheckState.Equals(FireAlarmCheckState.True) && item.CreationTime >= dt);

            var lstFireunit = fireUnits.Select(item => new GetFireunitLatForMapOutput()
            {
                FireunitId = item.Id,
                Lat = item.Lat,
                Lng = item.Lng,
                ExistTrueFireAlarm = false
            }).ToList();
            var query = from a in fireAlarms
                           join b in fireUnits on a.FireUnitId equals b.Id
                           select new
                           {
                               FireunitId = a.FireUnitId,
                           };
            var lstAlarm = query.ToList();
            foreach(var item in lstAlarm)
            {
                lstFireunit.FirstOrDefault(f => f.FireunitId.Equals(item.FireunitId)).ExistTrueFireAlarm = true;
            }

            return Task.FromResult(lstFireunit);
        }
        /// <summary>
        /// 监管部门数据大屏：获取真实火警联网实时达
        /// </summary>
        /// <param name="deptId"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public Task<List<GetTrueFireAlarmList_Output>> GetTrueFireAlarmList(int deptId, int num)
        {
            var alarms = _repAlarmToFire.GetAll().Where(item => item.CheckState.Equals(FireAlarmCheckState.True));
            var fireUnits = _repFireUnit.GetAll().Where(item => item.FireDeptId.Equals(deptId));
            var detectors = _repFireAlarmDetector.GetAll();
            var independentDetector = _repIndependentDetector.GetAll();

            var query = from a in alarms
                        join b in fireUnits on a.FireUnitId equals b.Id
                        join c in detectors on a.FireAlarmDetectorId equals c.Id into result1
                        from a_c in result1.DefaultIfEmpty()
                        join d in independentDetector on a.FireAlarmDetectorId equals d.Id into result2
                        from a_d in result2.DefaultIfEmpty()
                        select new GetTrueFireAlarmList_Output()
                        {
                            CheckTime = (DateTime)a.CheckTime,
                            CreationTime = a.CreationTime,
                            FireAlarmId = a.Id,
                            FireunitName = b.Name,
                            FireunitAddress = b.Address,
                            FireunitContractUser = b.ContractName + " " + b.ContractPhone,
                            FireDetectorLocation = a.FireAlarmSource.Equals(FireAlarmSource.NetDevice) ? a_c.FullLocation : a_d.Location,
                            ExistBitMap = a.FireAlarmSource.Equals(FireAlarmSource.NetDevice) ? (a_c.CoordinateX != 0 || a_c.CoordinateY != 0) : false
                        };
            return Task.FromResult(query.OrderByDescending(item => item.CheckTime).Take(num).ToList());
        }
        public Task<GetOtherNumOutput> GetOtherNum(int deptId)
        {
            DateTime dt = DateTime.Parse(DateTime.Now.ToString("yyyy-MM") + "-01");
            var alarms = _repAlarmToFire.GetAll().Where(item => item.CheckState.Equals(FireAlarmCheckState.True) && item.CheckTime >= dt);
            var fireUnits = _repFireUnit.GetAll().Where(item => item.FireDeptId.Equals(deptId));

            var query = from a in alarms
                        join b in fireUnits on a.FireUnitId equals b.Id
                        select new
                        {
                            Id = a.Id
                        };
            int fireAlarmNum = query.Count();
            int fireUnitNum = fireUnits.Count();

            return Task.FromResult(new GetOtherNumOutput()
            {
                FireAlarmNum = fireAlarmNum,
                FireunitNum = fireUnitNum
            });
        }
    }
}
