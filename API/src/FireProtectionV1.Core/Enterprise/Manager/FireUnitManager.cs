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
using FireProtectionV1.Alarm.Dto;
using FireProtectionV1.Alarm.Model;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Enterprise.Dto;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.Infrastructure.Model;
using FireProtectionV1.User.Manager;
using FireProtectionV1.User.Model;

namespace FireProtectionV1.Enterprise.Manager
{
    public class FireUnitManager : DomainService, IFireUnitManager
    {
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
        public Task<GetFireUnitAlarmOutput> GetFireUnitAlarm(GetFireUnitAlarmInput input)
        {
            throw new NotImplementedException();
            //var a = _alarmToElectricR.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime > DateTime.Now.Date.AddDays(-30));
            //int count = a.Count();
            //int hCount = a.GroupBy(p => p.DetectorId).Where(p => p.Count() > 5).Count();
            //int pointCount = from det in _detectorFireR.GetAll()
            //                join con in _controllerFireR.GetAll().Where(p => p.FireUnitId == input.Id)
            //                on det.ControllerId equals con.Id 
            //                select new
            //                {

            //                }
                
        }
    }
}
