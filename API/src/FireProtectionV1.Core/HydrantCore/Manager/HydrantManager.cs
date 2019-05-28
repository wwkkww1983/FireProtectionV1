using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Helper;
using FireProtectionV1.HydrantCore.Dto;
using FireProtectionV1.HydrantCore.Model;
using FireProtectionV1.Infrastructure.Model;
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

        public HydrantManager(
            IRepository<Hydrant> hydrantRepository,
            IRepository<HydrantPressure> hydrantPressureRepository,
            IRepository<HydrantAlarm> hydrantAlarmRepository,
            IRepository<Area> areaRepository)
        {
            _hydrantRepository = hydrantRepository;
            _hydrantPressureRepository = hydrantPressureRepository;
            _hydrantAlarmRepository = hydrantAlarmRepository;
            _areaRepository = areaRepository;
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
                            Pressure = hydrantPressures.OrderByDescending(p => p.CreationTime).First(p => p.HydrantId.Equals(a.Id)).Pressure
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
        public Task<PagedResultDto<GetHydrantListOutput>> GetListForWeb(GetHydrantListInput input)
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
                temp.LastAlarmTime = hydrantAlarms.Where(h => h.HydrantId.Equals(hydrant.Id)).Count() == 0 ? null : hydrantAlarms.OrderByDescending(h => h.CreationTime).First(h => h.HydrantId.Equals(hydrant.Id)).CreationTime.ToUniversalTime().ToString();
                temp.LastAlarmTitle = hydrantAlarms.Where(h => h.HydrantId.Equals(hydrant.Id)).Count() == 0 ? null : hydrantAlarms.OrderByDescending(h => h.CreationTime).First(h => h.HydrantId.Equals(hydrant.Id)).Title;
                temp.NearbyAlarmNumber = hydrantAlarms.Where(h => h.HydrantId.Equals(hydrant.Id) && h.CreationTime >= DateTime.Now.AddDays(-30)).Count();
                query.Add(temp);
            }
            var list = query.OrderByDescending(a=>a.Status)
                .Skip(input.SkipCount).Take(input.MaxResultCount)
                .ToList();
            var tCount = query.Count();

            return Task.FromResult(new PagedResultDto<GetHydrantListOutput>(tCount, list));
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
                temp.LastAlarmTime = hydrantAlarms.Where(h => h.HydrantId.Equals(hydrant.Id)).Count() == 0 ? null : hydrantAlarms.OrderByDescending(h => h.CreationTime).First(h => h.HydrantId.Equals(hydrant.Id)).CreationTime.ToUniversalTime().ToString();
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
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<PagedResultDto<HydrantAlarm>> GetNearbyAlarmById(GetHydrantAlarmInput input)
        {
            var query = _hydrantAlarmRepository.GetAll().Where(item => input.id.Equals(item.HydrantId) && item.CreationTime >= DateTime.Now.AddDays(-30)).ToList();
            var list = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            var tCount = query.Count();

            return Task.FromResult(new PagedResultDto<HydrantAlarm>(tCount, list));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task Update(UpdateHydrantInput input)
        {
            Valid.Exception(_hydrantRepository.Count(m => input.Sn.Equals(m.Sn) && !input.Id.Equals(m.Id)) > 0, "保存失败：设施编号已存在");

            var entity = input.MapTo<Hydrant>();
            await _hydrantRepository.UpdateAsync(entity);
        }
    }
}
