using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using FireProtectionV1.Common.Enum;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Dto.Patrol;
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
        IQueryable<DataToPatrol> GetPatrolDataAll();
        IQueryable<DataToPatrol> GetPatrolDataMonth(int year, int month);
        IQueryable<DataToPatrol> GetPatrolDataDuration(DateTime start, DateTime end);
        IQueryable<FireUnitManualOuput> GetPatrolFireUnitsAll(string filterName = null);
        IQueryable<FireUnitManualOuput> GetNoPatrol7DayFireUnits();
        /// <summary>
        /// 提交巡查轨迹点
        /// </summary>
        /// <param name="input"></param>
        /// <returns>返回巡查主记录Id</returns>
        Task<int> SubmitPatrolDetail(AddPatrolDetailInput input);
        /// <summary>
        /// 提交巡查主记录
        /// </summary>
        /// <param name="patrolId"></param>
        /// <returns></returns>
        Task SubmitPatrol(int patrolId);
        //// <summary>
        /// 获取巡查记录列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagedResultDto<GetPatrolListOutput>> GetPatrolList(GetDataPatrolInput input, PagedResultRequestDto dto);

        /// <summary>
        /// 获取巡查记录轨迹
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //Task<List<GetPatrolTrackOutput>> GetPatrolTrackList(GetPatrolTrackInput input);

        /// <summary>
        /// 获取防火单位消防系统
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //Task<List<GetPatrolFireUnitSystemOutput>> GetFireUnitlSystem(GetPatrolFireUnitSystemInput input);

        /// <summary>
        /// 添加巡查记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //Task<AddPatrolOutput> AddPatrolTrack(AddPatrolInput input);
        /// <summary>
        /// 添加巡查记录轨迹
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //Task<SuccessOutput> AddPatrolTrackDetailAll(AddPatrolTrackAllInput input);
        /// <summary>
        /// 添加巡查记录轨迹
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //Task<SuccessOutput> AddPatrolTrackDetail(AddPatrolTrackInput input);

        /// <summary>
        /// Web获取巡查记录列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //Task<List<GetDataPatrolForWebOutput>> GetPatrollistForWeb(GetDataPatrolForWebInput input);

        /// <summary>
        /// 获取巡查记录状态统计
        /// </summary>
        /// <param name="fireunitId"></param>
        /// <returns></returns>
        Task<GetPatrolStatusTotalOutput> GetPatrolStatusTotal(int fireunitId);
        /// <summary>
        /// 获取防火单位的巡查类别
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PatrolType> GetPatrolType(int fireUnitId);
        /// <summary>
        /// 是否允许新增巡查记录（如果存在未提交的巡查记录，则不允许新增）
        /// </summary>
        /// <param name="fireUnitId"></param>
        /// <returns></returns>
        Task<SuccessOutput> GetAddPatrolAllow(int fireUnitId);
        /// <summary>
        /// 获取巡查记录详情
        /// </summary>
        /// <param name="patrolId"></param>
        /// <returns></returns>
        Task<GetPatrolInfoOutput> GetPatrolInfo(int patrolId);
        /// <summary>
        /// 获取巡查记录日历列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<GetDataForCalendarOutput>> GetPatrollistForCalendar(GetDataForCalendarInput input);
    }
}
