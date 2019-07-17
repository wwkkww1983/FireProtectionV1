using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Helper;
using FireProtectionV1.HydrantCore.Dto;
using FireProtectionV1.HydrantCore.Model;
using FireProtectionV1.Infrastructure.Model;
using FireProtectionV1.SettingCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.HydrantCore.Manager
{
    public class HydrantManager : DomainService, IHydrantManager
    {
        IRepository<Hydrant> _hydrantRepository;
        IRepository<HydrantPressure> _hydrantPressureRepository;
        IRepository<HydrantAlarm> _hydrantAlarmRepository;
        IRepository<Area> _areaRepository;
        IRepository<FireSetting> _settingRepository;
        ISqlRepository _SqlRepository;

        public HydrantManager(
            IRepository<Hydrant> hydrantRepository,
            IRepository<HydrantPressure> hydrantPressureRepository,
            IRepository<HydrantAlarm> hydrantAlarmRepository,
            IRepository<Area> areaRepository,
            IRepository<FireSetting> settingRepository,
            ISqlRepository sqlRepository)
        {
            _hydrantRepository = hydrantRepository;
            _hydrantPressureRepository = hydrantPressureRepository;
            _hydrantAlarmRepository = hydrantAlarmRepository;
            _areaRepository = areaRepository;
            _settingRepository = settingRepository;
            _SqlRepository = sqlRepository;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> Add(AddHydrantInput input)
        {
            Valid.Exception(_hydrantRepository.Count(m => input.Sn.Equals(m.Sn)) > 0, "保存失败：设施编号已存在");

            var entity = input.MapTo<Hydrant>();
            return await _hydrantRepository.InsertAndGetIdAsync(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> Delete(DeletHydrantInput input)
        {
            await _hydrantRepository.DeleteAsync(input.Id);
            return new SuccessOutput() { Success = true };
        }

        /// <summary>
        /// 获取实体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<HydrantDetailOutput> GetInfoById(int id)
        {
            var hydrants = _hydrantRepository.GetAll();
            var eares = _areaRepository.GetAll();
            var hydrantPressures = _hydrantPressureRepository.GetAll();

            var query = from a in hydrants
                        join b in eares on a.AreaId equals b.Id into r1
                        from dr1 in r1.DefaultIfEmpty()
                        where id.Equals(a.Id)
                        select new HydrantDetailOutput
                        {
                            Id = a.Id,
                            Sn = a.Sn,
                            AreaName = dr1.Name,
                            Address = a.Address,
                            Pressure = hydrantPressures.Where(p => p.HydrantId.Equals(a.Id)).Count()==0?0:hydrantPressures.OrderByDescending(p => p.CreationTime).First(p => p.HydrantId.Equals(a.Id)).Pressure,
                            Status=a.Status,
                            Lng=a.Lng,
                            Lat=a.Lat
                        };

            var result = query.SingleOrDefault();
            return Task.FromResult(result);
        }

        /// <summary>
        /// App端分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<PagedResultDto<Hydrant>> GetListForApp(GetHydrantListInput input)
        {
            var hydrants = _hydrantRepository.GetAll();

            var expr = ExprExtension.True<Hydrant>()
             .IfAnd(!string.IsNullOrEmpty(input.Sn), item => item.Sn.Contains(input.Sn));

            hydrants = hydrants.Where(expr);

            List<Hydrant> list = hydrants
                .OrderBy(item => item.Status)
                .Skip(input.SkipCount).Take(input.MaxResultCount)
                .ToList();
            var tCount = hydrants.Count();

            return Task.FromResult(new PagedResultDto<Hydrant>(tCount, list));
        }

        /// <summary>
        /// Web端分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<GetPressureSubstandardOutput> GetListForWeb(GetHydrantListInput input)
        {
            var hydrants = _hydrantRepository.GetAll();
            var areas = _areaRepository.GetAll();
            var hydrantPressures = _hydrantPressureRepository.GetAll();
            var hydrantAlarms = _hydrantAlarmRepository.GetAll();
            var output = new GetPressureSubstandardOutput();

            var expr = ExprExtension.True<Hydrant>()
                .IfAnd(!string.IsNullOrEmpty(input.Sn), item => item.Sn.Contains(input.Sn));
            hydrants = hydrants.Where(expr);
            var hydrantlist = from a in hydrants
                              join b in areas
                              on a.AreaId equals b.Id into r1
                              from dr1 in r1.DefaultIfEmpty()
                              orderby a.Status
                              select new
                              {
                                  Id = a.Id,
                                  Sn = a.Sn,
                                  AreaName = dr1.Name,
                                  Address = a.Address,
                                  Status = a.Status,
                              };
            List<GetHydrantListOutput> query = new List<GetHydrantListOutput>();
            foreach (var hydrant in hydrantlist)
            {
                GetHydrantListOutput temp = new GetHydrantListOutput();
                temp.Id = hydrant.Id;
                temp.Sn = hydrant.Sn;
                temp.AreaName = hydrant.AreaName;
                temp.Address = hydrant.Address;
                temp.Status = hydrant.Status;
                temp.Pressure = hydrantPressures.Where(p => p.HydrantId.Equals(hydrant.Id)).Count() == 0 ? 0 : hydrantPressures.OrderByDescending(p => p.CreationTime).First(p => p.HydrantId.Equals(hydrant.Id)).Pressure;
                temp.LastAlarmTime = hydrantAlarms.Where(h => h.HydrantId.Equals(hydrant.Id)).Count() == 0 ? null : hydrantAlarms.OrderByDescending(h => h.CreationTime).First(h => h.HydrantId.Equals(hydrant.Id)).CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
                temp.LastAlarmTitle = hydrantAlarms.Where(h => h.HydrantId.Equals(hydrant.Id)).Count() == 0 ? null : hydrantAlarms.OrderByDescending(h => h.CreationTime).First(h => h.HydrantId.Equals(hydrant.Id)).Title;
                temp.NearbyAlarmNumber = hydrantAlarms.Where(h => h.HydrantId.Equals(hydrant.Id) && h.CreationTime >= DateTime.Now.AddDays(-30)).Count();
                query.Add(temp);
            }
            var substan = _settingRepository.GetAll().Where(u => u.Name.Equals("PoolWaterPressure")).FirstOrDefault();
            if (substan != null)
            {
                output.SubstanCount = query.Where(u => u.Pressure <= substan.MinValue&&u.Pressure!=0).Count();
            }

            var list = query.OrderByDescending(a=>a.LastAlarmTime)
                .Skip(input.SkipCount).Take(input.MaxResultCount)
                .ToList();
            var tCount = query.Count();
            output.PagedResultDto = new PagedResultDto<GetHydrantListOutput>(tCount, list);
            return Task.FromResult<GetPressureSubstandardOutput>(output);
        }


        /// <summary>
        /// 查询水压低于标准值的消火栓
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<GetPressureSubstandardOutput> GetPressureSubstandard(GetHydrantListInput input)
        {
            var hydrants = _hydrantRepository.GetAll();
            var areas = _areaRepository.GetAll();
            var hydrantPressures = _hydrantPressureRepository.GetAll();
            var hydrantAlarms = _hydrantAlarmRepository.GetAll();
            var output = new GetPressureSubstandardOutput();

            var expr = ExprExtension.True<Hydrant>()
                .IfAnd(!string.IsNullOrEmpty(input.Sn), item => item.Sn.Contains(input.Sn));
            hydrants = hydrants.Where(expr);
            var hydrantlist = from a in hydrants
                              join b in areas
                              on a.AreaId equals b.Id into r1
                              from dr1 in r1.DefaultIfEmpty()
                              orderby a.Status
                              select new
                              {
                                  Id = a.Id,
                                  Sn = a.Sn,
                                  AreaName = dr1.Name,
                                  Address = a.Address,
                                  Status = a.Status,
                              };
            List<GetHydrantListOutput> query = new List<GetHydrantListOutput>();
            foreach (var hydrant in hydrantlist)
            {
                GetHydrantListOutput temp = new GetHydrantListOutput();
                temp.Id = hydrant.Id;
                temp.Sn = hydrant.Sn;
                temp.AreaName = hydrant.AreaName;
                temp.Address = hydrant.Address;
                temp.Status = hydrant.Status;
                temp.Pressure = hydrantPressures.Where(p => p.HydrantId.Equals(hydrant.Id)).Count() == 0 ? 0 : hydrantPressures.OrderByDescending(p => p.CreationTime).First(p => p.HydrantId.Equals(hydrant.Id)).Pressure;
                temp.LastAlarmTime = hydrantAlarms.Where(h => h.HydrantId.Equals(hydrant.Id)).Count() == 0 ? null : hydrantAlarms.OrderByDescending(h => h.CreationTime).First(h => h.HydrantId.Equals(hydrant.Id)).CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
                temp.LastAlarmTitle = hydrantAlarms.Where(h => h.HydrantId.Equals(hydrant.Id)).Count() == 0 ? null : hydrantAlarms.OrderByDescending(h => h.CreationTime).First(h => h.HydrantId.Equals(hydrant.Id)).Title;
                temp.NearbyAlarmNumber = hydrantAlarms.Where(h => h.HydrantId.Equals(hydrant.Id) && h.CreationTime >= DateTime.Now.AddDays(-30)).Count();
                query.Add(temp);
            }
            var substan = _settingRepository.GetAll().Where(u => "PoolWaterPressure".Equals(u.Name)).FirstOrDefault();
            if (substan != null)
            {
                output.SubstanCount = query.Where(u => u.Pressure <= substan.MinValue && u.Pressure != 0).Count();
                query = (from a in query
                        where a.Pressure <= substan.MinValue && a.Pressure != 0
                         select a).ToList();
            }

            var list = query.OrderByDescending(a => a.Status)
                .Skip(input.SkipCount).Take(input.MaxResultCount)
                .ToList();
            var tCount = query.Count();
            output.PagedResultDto = new PagedResultDto<GetHydrantListOutput>(tCount, list);
            return Task.FromResult<GetPressureSubstandardOutput>(output);
        }
        /// <summary>
        /// 消火栓Excel导出
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<List<GetHydrantListOutput>> GetHydrantExcel(GetHydrantListInput input)
        {
            var hydrants = _hydrantRepository.GetAll();
            var expr = ExprExtension.True<Hydrant>()
                .IfAnd(!string.IsNullOrEmpty(input.Sn), item => item.Sn.Contains(input.Sn));
            hydrants = hydrants.Where(expr);

            var areas = _areaRepository.GetAll();
            var hydrantPressures = _hydrantPressureRepository.GetAll();
            var hydrantAlarms = _hydrantAlarmRepository.GetAll();

            var hydrantlist = from a in hydrants
                              join b in areas
                              on a.AreaId equals b.Id into r1
                              from dr1 in r1.DefaultIfEmpty()
                              orderby a.Status
                              select new
                              {
                                  Id = a.Id,
                                  Sn = a.Sn,
                                  AreaName = dr1.Name,
                                  Address = a.Address,
                                  Status = a.Status,
                              };
            List<GetHydrantListOutput> query = new List<GetHydrantListOutput>();
            foreach (var hydrant in hydrantlist)
            {
                GetHydrantListOutput temp = new GetHydrantListOutput();
                temp.Id = hydrant.Id;
                temp.Sn = hydrant.Sn;
                temp.AreaName = hydrant.AreaName;
                temp.Address = hydrant.Address;
                temp.Status = hydrant.Status;
                temp.Pressure = hydrantPressures.Where(p => p.HydrantId.Equals(hydrant.Id)).Count() == 0 ? 0 : hydrantPressures.OrderByDescending(p => p.CreationTime).First(p => p.HydrantId.Equals(hydrant.Id)).Pressure;
                temp.LastAlarmTime = hydrantAlarms.Where(h => h.HydrantId.Equals(hydrant.Id)).Count() == 0 ? null : hydrantAlarms.OrderByDescending(h => h.CreationTime).First(h => h.HydrantId.Equals(hydrant.Id)).CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
                temp.LastAlarmTitle = hydrantAlarms.Where(h => h.HydrantId.Equals(hydrant.Id)).Count() == 0 ? null : hydrantAlarms.OrderByDescending(h => h.CreationTime).First(h => h.HydrantId.Equals(hydrant.Id)).Title;
                temp.NearbyAlarmNumber = hydrantAlarms.Where(h => h.HydrantId.Equals(hydrant.Id) && h.CreationTime >= DateTime.Now.AddDays(-30)).Count();
                query.Add(temp);
            }
            var list = query.OrderByDescending(a => a.Status)
                .ToList();

            return Task.FromResult<List<GetHydrantListOutput>>(list);
        }
        /// <summary>
        /// 获取最近30天报警记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<PagedResultDto<GetNearAlarmOutput>> GetNearbyAlarmById(GetHydrantAlarmInput input)
        {
            var query = _hydrantAlarmRepository.GetAll().Where(item => input.id.Equals(item.HydrantId) && item.CreationTime >= DateTime.Now.AddDays(-30)).ToList();
            var changeType = from a in query
                             select new GetNearAlarmOutput
                             {
                                 CreationTime = a.CreationTime.ToString("yyyy-MM-dd HH:mm"),
                                 HydrantId = a.HydrantId,
                                 Id = a.Id,
                                 IsDeleted = a.IsDeleted,
                                 Title = a.Title,
                             };
            var list = changeType.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            var tCount = query.Count();

            return Task.FromResult(new PagedResultDto<GetNearAlarmOutput>(tCount, list)); 
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task Update(UpdateHydrantInput input)
        {
            Valid.Exception(_hydrantRepository.Count(m => input.Sn.Equals(m.Sn) && !input.Id.Equals(m.Id)) > 0, "保存失败：设施编号已存在");
            var old = _hydrantRepository.GetAll().Where(u => u.Id == input.Id).FirstOrDefault();
            old.Sn = input.Sn;
            old.AreaId = input.AreaId;
            old.Address = input.Address;
            await _hydrantRepository.UpdateAsync(old);
        }

        /// <summary>
        /// 根据坐标点获取附近1KM直线距离内的消火栓
        /// </summary>
        /// <param name="lng">经度，例如104.159203</param>
        /// <param name="lat">纬度，例如30.633145</param>
        /// <returns></returns>
        public Task<List<GetNearbyHydrantOutput>> GetNearbyHydrant(decimal lng, decimal lat)
        {
            // 6378.138是地球赤道的半径，单位千米
            string sql = $@"SELECT * FROM (SELECT *, ROUND(6378.138 * 2 * ASIN(SQRT(POW(SIN(({lat} * PI() / 180 - Lat * PI() / 180) / 2), 2) + COS({lat} * PI() / 180) * COS(Lat * PI() / 180) * 
POW(SIN(({lng} * PI() / 180 - Lng * PI() / 180) / 2), 2))) *1000) AS Distance FROM Hydrant WHERE Lat !=0 and Lng != 0) a WHERE Distance <= 1000 ORDER BY Distance ASC";

            var dataTable = _SqlRepository.Query(sql);
            return Task.FromResult(_SqlRepository.DataTableToList<GetNearbyHydrantOutput>(dataTable));
        }
    }
}
