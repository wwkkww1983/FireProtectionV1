using Abp.Domain.Services;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.FireWorking.Manager
{
    public interface IPatrolManager : IDomainService
    {
        /// <summary>
        /// 新增巡查记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddNewPatrol(AddNewPatrolInput input);
        IQueryable<DataToPatrol> GetPatrolDataAll();
        IQueryable<DataToPatrol> GetPatrolDataMonth(int year, int month);
        IQueryable<DataToPatrol> GetPatrolDataDuration(DateTime start, DateTime end);
        IQueryable<FireUnitManualOuput> GetPatrolFireUnitsAll(string filterName = null);
        IQueryable<FireUnitManualOuput> GetNoPatrol7DayFireUnits();

        /// <summary>
        /// 获取巡查记录列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetDataPatrolPagingOutput> GetPatrollist(GetDataPatrolInput input);

        /// <summary>
        /// 获取巡查记录轨迹
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<GetPatrolTrackOutput>> GetPatrolTrackList(GetPatrolTrackInput input);

        /// <summary>
        /// 获取防火单位消防系统
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<GetPatrolFireUnitSystemOutput>> GetFireUnitlSystem(GetPatrolFireUnitSystemInput input);

        /// <summary>
        /// 添加巡查记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AddPatrolOutput> AddPatrolTrack(AddPatrolInput input);
        /// <summary>
        /// 添加巡查记录轨迹
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> AddPatrolTrackDetail(AddPatrolTrackInput input);

        /// <summary>
        /// Web获取巡查记录列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<GetDataPatrolForWebOutput>> GetPatrollistForWeb(GetDataPatrolForWebInput input);

        /// <summary>
        /// Web获取巡查记录统计
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetDataPatrolTotalOutput> GetPatrolTotal(GetDataPatrolTotalInput input);
        /// <summary>
        /// Web获取巡查记录详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetPatrolInfoForWebOutput> GetPatrolInfoForWeb(GetPatrolInfoForWebInput input);
        /// <summary>
        /// 新增时获取巡查记录类别
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetPatrolTypeOutput> GetPatrolType(GetPatrolFireUnitSystemInput input);
    }
}
