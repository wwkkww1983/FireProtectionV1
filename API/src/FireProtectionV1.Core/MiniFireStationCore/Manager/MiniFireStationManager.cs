using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Helper;
using FireProtectionV1.MiniFireStationCore.Dto;
using FireProtectionV1.MiniFireStationCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.MiniFireStationCore.Manager
{
    public class MiniFireStationManager : DomainService, IMiniFireStationManager
    {
        IRepository<MiniFireStation> _miniFireStationRepository;
        ISqlRepository _SqlRepository;
        ISqlExecuter _sqlExecuter;

        public MiniFireStationManager(IRepository<MiniFireStation> miniFireStationRepository, ISqlRepository sqlRepository, ISqlExecuter sqlExecuter)
        {
            _miniFireStationRepository = miniFireStationRepository;
            _SqlRepository = sqlRepository;
            _sqlExecuter = sqlExecuter;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> Add(AddMiniFireStationInput input)
        {
            Valid.Exception(_miniFireStationRepository.Count(m => input.Name.Equals(m.Name)) > 0, "保存失败：站点名称已存在");

            var entity = input.MapTo<MiniFireStation>();
            return await _miniFireStationRepository.InsertAndGetIdAsync(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            await _miniFireStationRepository.DeleteAsync(id);
        }

        /// <summary>
        /// 获取单个微型消防站信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MiniFireStation> GetById(int id)
        {
            return await _miniFireStationRepository.GetAsync(id);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<PagedResultDto<MiniFireStation>> GetList(GetMiniFireStationListInput input)
        {
            var miniFireStations = _miniFireStationRepository.GetAll();

            var expr = ExprExtension.True<MiniFireStation>()
             .IfAnd(!string.IsNullOrEmpty(input.Name), item => item.Name.Contains(input.Name));

            miniFireStations = miniFireStations.Where(expr);

            List<MiniFireStation> list = miniFireStations
                .OrderByDescending(item => item.CreationTime)
                .Skip(input.SkipCount).Take(input.MaxResultCount)
                .ToList();
            var tCount = miniFireStations.Count();

            return Task.FromResult(new PagedResultDto<MiniFireStation>(tCount, list));
        }

        /// <summary>
        /// 根据坐标点获取附近1KM直线距离内的微型消防站
        /// </summary>
        /// <param name="lng">经度，例如104.159203</param>
        /// <param name="lat">纬度，例如30.633145</param>
        /// <returns></returns>
        public Task<List<GetNearbyStationOutput>> GetNearbyStation(decimal lng, decimal lat)
        {
            // 6378.138是地球赤道的半径，单位千米
            string sql = $@"SELECT * FROM (SELECT *, ROUND(6378.138 * 2 * ASIN(SQRT(POW(SIN(({lat} * PI() / 180 - Lat * PI() / 180) / 2), 2) + COS({lat} * PI() / 180) * COS(Lat * PI() / 180) * 
POW(SIN(({lng} * PI() / 180 - Lng * PI() / 180) / 2), 2))) *1000) AS Distance FROM MiniFireStation WHERE Lat !=0 and Lng != 0) a WHERE Distance <= 1000 ORDER BY Distance ASC";

            var dataTable = _SqlRepository.Query(sql);
            return Task.FromResult(_SqlRepository.DataTableToList<GetNearbyStationOutput>(dataTable));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task Update(UpdateMiniFireStationInput input)
        {
            Valid.Exception(_miniFireStationRepository.Count(m => input.Name.Equals(m.Name) && !input.Id.Equals(m.Id)) > 0, "保存失败：站点名称已存在");

            var entity = input.MapTo<MiniFireStation>();
            await _miniFireStationRepository.UpdateAsync(entity);
        }
    }
}
