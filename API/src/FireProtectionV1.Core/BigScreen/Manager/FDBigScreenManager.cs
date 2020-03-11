using Abp.Domain.Repositories;
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
        public FDBigScreenManager(
            IRepository<FireUnit> repFireUnit,
            IRepository<AlarmToFire> repAlarmToFire,
            IRepository<FireAlarmDevice> repFireAlarmDevice,
            IRepository<FireAlarmDetector> repFireAlarmDetector,
            IRepository<FireElectricDevice> repFireElectricDevice)
        {
            _repFireUnit = repFireUnit;
            _repAlarmToFire = repAlarmToFire;
            _repFireAlarmDevice = repFireAlarmDevice;
            _repFireAlarmDetector = repFireAlarmDetector;
            _repFireElectricDevice = repFireElectricDevice;
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
            return Task.FromResult(_repFireUnit.GetAll().Where(item => item.FireDeptId.Equals(deptId)).Select(item => new GetFireunitLatForMapOutput()
            {
                FireunitId = item.Id,
                Lat = item.Lat,
                Lng = item.Lng
            }).ToList());
        }
        /// <summary>
        /// 监管部门数据大屏：获取真实火警联网实时达
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public Task<List<GetTrueFireAlarmListOutput>> GetTrueFireAlarmList(int deptId)
        {
            var alarms = _repAlarmToFire.GetAll().Where(item=>item.CheckState.Equals(FireAlarmCheckState.True));
            var fireUnits = _repFireUnit.GetAll().Where(item => item.FireDeptId.Equals(deptId));
            var detectors = _repFireAlarmDetector.GetAll();

            var query = from a in alarms
                        join b in fireUnits on a.FireUnitId equals b.Id
                        join c in detectors on a.FireAlarmDetectorId equals c.Id
                        select new GetTrueFireAlarmListOutput()
                        {
                            CheckTime = (DateTime)a.CheckTime,
                            CreationTime = a.CreationTime,
                            FireAlarmId = a.Id,
                            FireunitName = b.Name,
                            FireunitAddress = b.Address,
                            FireunitContractUser = b.ContractName + " " + b.ContractPhone,
                            FireDetectorLocation = c.FullLocation,
                            ExistBitMap = c.CoordinateX != 0 || c.CoordinateY != 0
                        };
            return Task.FromResult(query.OrderByDescending(item => item.CheckTime).Skip(0).Take(5).ToList());
        }
    }
}
