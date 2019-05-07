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
using FireProtectionV1.Account.Dto;
using FireProtectionV1.Account.Manager;
using FireProtectionV1.Account.Model;
using FireProtectionV1.Alarm.Dto;
using FireProtectionV1.Alarm.Model;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Configuration;
using FireProtectionV1.Enterprise.Dto;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.Infrastructure.Model;

namespace FireProtectionV1.Enterprise.Manager
{
    public class FireUnitManager : DomainService, IFireUnitManager
    {
        IRepository<ControllerElectric> _controllerElectricR;
        IRepository<DetectorElectric> _detectorElectricR;
        IRepository<ControllerFire> _controllerFireR;
        IRepository<DetectorFire> _detectorFireR;
        IRepository<AlarmToFire> _alarmToFireR;
        IRepository<AlarmToElectric> _alarmToElectricR;
        IRepository<SafeUnit> _safeUnitR;
        IRepository<Area> _areaR;
        IRepository<FireUnitType> _fireUnitTypeR;
        IRepository<FireUnit> _fireUnitR;
        IRepository<FireUnitUser> _fireUnitAccountRepository;
        IFireUnitUserManager _fireUnitAccountManager;
        ICacheManager _cacheManager;
        public FireUnitManager(
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
            _controllerElectricR = controllerElectricR;
            _detectorElectricR = detectorElectricR;
            _detectorFireR = detectorFireR;
            _alarmToElectricR = alarmToElectricR;
            _alarmToFireR = alarmToFireR;
            _safeUnitR = safeUnitR;
            _areaR = areaR;
            _fireUnitTypeR = fireUnitTypeR;
            _fireUnitR = fireUnitInfoRepository;
            _fireUnitAccountRepository = fireUnitAccountRepository;
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

            return await _fireUnitR.InsertAndGetIdAsync(fireUnitInfo);
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
            GetFireUnitInfoOutput o = new GetFireUnitInfoOutput();
            var f = await _fireUnitR.SingleAsync(p => p.Id.Equals(input.Id));
            if (f != null)
            {
                o.Name = f.Name;
                o.Address = f.Address;
                var a =await _areaR.SingleAsync(p => p.Id.Equals(f.AreaId));
                if(a!=null)
                {
                    var codes = a.AreaPath.Split('-');
                    o.Area = "";
                    foreach(var code in codes)
                    {
                        var area = await _areaR.SingleAsync(p => p.AreaCode.Equals(code));
                        o.Area += area.Name;
                    }

                }
                var type =await _fireUnitTypeR.SingleAsync(p => p.Id == f.TypeId);
                if (type != null)
                    o.Type = type.Name;
                if (f.SafeUnitId != 0)
                {
                    var safe = await _safeUnitR.SingleAsync(p => p.Id == f.SafeUnitId);
                    if (safe != null)
                        o.SafeUnit = safe.Name;
                }
            }
            return o;
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
                        .GetAsync(id.ToString(), () => _fireUnitR.GetAsync(id)) as FireUnit;
        }

        /// <summary>
        /// 根据ID删除防火单位
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            await _fireUnitR.DeleteAsync(id);
        }
        /// <summary>
        /// 防火单位消防数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnitAlarmOutput> GetFireUnitAlarm(GetFireUnitAlarmInput input)
        {
            //throw new NotImplementedException();
            GetFireUnitAlarmOutput o = new GetFireUnitAlarmOutput();
            int highreq = int.Parse(ConfigHelper.Configuration["FireDomain:HighFreqAlarm"]);
            await Task.Run(() =>
            {
                var alarmElec = _alarmToElectricR.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime > DateTime.Now.Date.AddDays(-30));
                o.Elec30DayNum = alarmElec.Count();
                o.ElecHighCount = alarmElec.GroupBy(p => p.DetectorId).Where(p => p.Count() > highreq).Count();
                o.ElecPointsCount = (from det in _detectorElectricR.GetAll()
                                     join con in _controllerElectricR.GetAll().Where(p => p.FireUnitId == input.Id)
                                     on det.ControllerId equals con.Id
                                     select det.Id).Count();
                var netStates = _controllerElectricR.GetAll().Where(p => p.FireUnitId == input.Id).Select(p => p.NetworkState);
                int netStatesCount = netStates.Count();
                if (netStatesCount > 0)
                {
                    o.ElecState = netStates.First();
                    if (netStatesCount > 1)
                        o.ElecState = $"{o.ElecState}({netStates.Select(p => p.Equals(o.ElecState)).Count()}/{netStatesCount})";
                }

                var alarmFire = _alarmToFireR.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime > DateTime.Now.Date.AddDays(-30));
                o.Fire30DayNum = alarmFire.Count();
                o.FireHighCount = alarmFire.GroupBy(p => p.DetectorId).Where(p => p.Count() > highreq).Count();
                o.FirePointsCount = (from det in _detectorFireR.GetAll()
                                     join con in _controllerFireR.GetAll().Where(p => p.FireUnitId == input.Id)
                                     on det.ControllerId equals con.Id
                                     select det.Id).Count();
                 netStates = _controllerFireR.GetAll().Where(p => p.FireUnitId == input.Id).Select(p => p.NetworkState);
                 netStatesCount = netStates.Count();
                if (netStatesCount > 0)
                {
                    o.FireState = netStates.First();
                    if (netStatesCount > 1)
                        o.FireState = $"{o.FireState}({netStates.Select(p => p.Equals(o.FireState)).Count()}/{netStatesCount})";
                }
            });
            return o;
        }
    }
}
