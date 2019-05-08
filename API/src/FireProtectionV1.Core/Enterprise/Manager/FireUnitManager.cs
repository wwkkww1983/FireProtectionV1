using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Runtime.Caching;
using FireProtectionV1.Device.Dto;
using FireProtectionV1.Device.Model;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Configuration;
using FireProtectionV1.Enterprise.Dto;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.Infrastructure.Model;
using FireProtectionV1.User.Manager;
using FireProtectionV1.User.Model;

namespace FireProtectionV1.Enterprise.Manager
{
    public class FireUnitManager : DomainService, IFireUnitManager
    {
        IRepository<Patrol> _patrolRep;
        IRepository<Fault> _faultRep;
        IRepository<ControllerElectric> _controllerElectricRep;
        IRepository<DetectorElectric> _detectorElectricRep;
        IRepository<ControllerFire> _controllerFireRep;
        IRepository<DetectorFire> _detectorFireRep;
        IRepository<AlarmToFire> _alarmToFireRep;
        IRepository<AlarmToElectric> _alarmToElectricRep;
        IRepository<SafeUnit> _safeUnitRep;
        IRepository<Area> _areaRep;
        IRepository<FireUnitType> _fireUnitTypeRep;
        IRepository<FireUnit> _fireUnitRep;
        IRepository<FireUnitUser> _fireUnitUserRep;
        IFireUnitUserManager _fireUnitAccountManager;
        ICacheManager _cacheManager;
        public FireUnitManager(
            IRepository<Fault> faultRep,
            IRepository<ControllerFire> controllerFireRep,
            IRepository<ControllerElectric> controllerElectricR,
            IRepository<DetectorElectric> detectorElectricR,
            IRepository<DetectorFire> detectorFireR,
            IRepository<AlarmToFire> alarmToFireR,
            IRepository<AlarmToElectric> alarmToElectricR,
            IRepository<SafeUnit> safeUnitR,
            IRepository<Area> areaR,
            IRepository<FireUnitType> fireUnitTypeR,
            IRepository<FireUnit> fireUnitInfoRepository, IRepository<FireUnitUser> fireUnitAccountRepository,
            IFireUnitUserManager fireUnitAccountManager,
            ICacheManager cacheManager
            )
        {
            _faultRep = faultRep;
            _controllerFireRep = controllerFireRep;
            _controllerElectricRep = controllerElectricR;
            _detectorElectricRep = detectorElectricR;
            _detectorFireRep = detectorFireR;
            _alarmToElectricRep = alarmToElectricR;
            _alarmToFireRep = alarmToFireR;
            _safeUnitRep = safeUnitR;
            _areaRep = areaR;
            _fireUnitTypeRep = fireUnitTypeR;
            _fireUnitRep = fireUnitInfoRepository;
            _fireUnitUserRep = fireUnitAccountRepository;
            _fireUnitAccountManager = fireUnitAccountManager;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// 添加防火单位（同时会添加防火单位管理员账号）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> Add(AddFireUnitInput input)
        {
            await _fireUnitAccountManager.Add(input.accountInput);

            var fireUnitInfo = new FireUnit()
            {
                Name = input.Name
            };

            return await _fireUnitRep.InsertAndGetIdAsync(fireUnitInfo);
        }

        /// <summary>
        /// 修改防火单位信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task Update(UpdateFireUnitInput input)
        {
            
        }

        /// <summary>
        /// 防火单位信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnitInfoOutput> GetFireUnitInfo(GetFireUnitInfoInput input)
        {
            GetFireUnitInfoOutput output = new GetFireUnitInfoOutput();
            var f = await _fireUnitRep.SingleAsync(p => p.Id.Equals(input.Id));
            if (f != null)
            {
                output.Name = f.Name;
                output.Address = f.Address;
                var a =await _areaRep.SingleAsync(p => p.Id.Equals(f.AreaId));
                if(a!=null)
                {
                    var codes = a.AreaPath.Split('-');
                    output.Area = "";
                    foreach(var code in codes)
                    {
                        var area = await _areaRep.SingleAsync(p => p.AreaCode.Equals(code));
                        output.Area += area.Name;
                    }

                }
                var type =await _fireUnitTypeRep.SingleAsync(p => p.Id == f.TypeId);
                if (type != null)
                    output.Type = type.Name;
                if (f.SafeUnitId != 0)
                {
                    var safe = await _safeUnitRep.SingleAsync(p => p.Id == f.SafeUnitId);
                    if (safe != null)
                        output.SafeUnit = safe.Name;
                }
            }
            return output;
        }

        /// <summary>
        /// 根据ID获取防火单位信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<FireUnit> Get(int id)
        {
            return await _cacheManager
                        .GetCache("FireUnit")
                        .GetAsync(id.ToString(), () => _fireUnitRep.GetAsync(id)) as FireUnit;
        }

        /// <summary>
        /// 根据ID删除防火单位
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            await _fireUnitRep.DeleteAsync(id);
        }
        /// <summary>
        /// 防火单位消防数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnitAlarmOutput> GetFireUnitAlarm(GetFireUnitAlarmInput input)
        {
            GetFireUnitAlarmOutput output = new GetFireUnitAlarmOutput();
            int highFreq = int.Parse(ConfigHelper.Configuration["FireDomain:HighFreqAlarm"]);
            await Task.Run(() =>
            {
                //安全用电数据：管控点位数量、网关状态、最近30天报警次数（可查）、高频报警部件数量（可查）
                var alarmElec = _alarmToElectricRep.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime > DateTime.Now.Date.AddDays(-30));
                output.Elec30DayCount = alarmElec.Count();
                output.ElecHighCount = alarmElec.GroupBy(p => p.DeviceId).Where(p => p.Count() > highFreq).Count();
                output.ElecPointsCount = (from det in _detectorElectricRep.GetAll()
                                     join con in _controllerElectricRep.GetAll().Where(p => p.FireUnitId == input.Id)
                                     on det.ControllerId equals con.Id
                                     select det.Id).Count();
                var netStates = _controllerElectricRep.GetAll().Where(p => p.FireUnitId == input.Id).Select(p => p.NetworkState);
                int netStatesCount = netStates.Count();
                if (netStatesCount > 0)
                {
                    output.ElecState = netStates.First();
                    if (netStatesCount > 1)
                        output.ElecState = $"{output.ElecState}({netStates.Select(p => p.Equals(output.ElecState)).Count()}/{netStatesCount})";
                }
                //火警预警数据：管控点位数量、网关状态、最近30天报警次数（可查）、高频报警部件数量（可查）；
                var alarmFire = _alarmToFireRep.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime > DateTime.Now.Date.AddDays(-30));
                output.Fire30DayCount = alarmFire.Count();
                output.FireHighCount = alarmFire.GroupBy(p => p.DeviceId).Where(p => p.Count() > highFreq).Count();
                output.FirePointsCount = (from det in _detectorFireRep.GetAll()
                                     join con in _controllerFireRep.GetAll().Where(p => p.FireUnitId == input.Id)
                                     on det.ControllerId equals con.Id
                                     select det.Id).Count();
                //火警控制器可能不知道下面有多少点位，就以控制器数量充当
                output.FirePointsCount += _controllerFireRep.GetAll().Where(p => p.FireUnitId == input.Id).Count();
                 netStates = _controllerFireRep.GetAll().Where(p => p.FireUnitId == input.Id).Select(p => p.NetworkState);
                 netStatesCount = netStates.Count();
                if (netStatesCount > 0)
                {
                    output.FireState = netStates.First();
                    if (netStatesCount > 1)
                        output.FireState = $"{output.FireState}({netStates.Select(p => p.Equals(output.FireState)).Count()}/{netStatesCount})";
                }
                //故障数据
                var faults = _faultRep.GetAll().Where(p => p.FireUnitId == input.Id);
                output.FaultCount = faults.Count();
                output.FaultPendingCount= faults.Where(p => p.ProcessState == 0).Count();
                output.FaultProcessedCount = output.FaultCount - output.FaultPendingCount;
                //巡查记录：最近提交时间、最近30天提交记录数量
                output.Patrol30DayCount = _patrolRep.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime > DateTime.Now.Date.AddDays(-30)).Count();
                //值班记录：最近提交时间、最近30天提交记录数量
            });
            return output;
        }
    }
}
