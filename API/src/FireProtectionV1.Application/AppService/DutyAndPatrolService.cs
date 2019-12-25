using Abp.Application.Services.Dto;
using FireProtectionV1.Common.Enum;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Dto.Patrol;
using FireProtectionV1.FireWorking.Manager;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    public class DutyAndPatrolService : AppServiceBase
    {
        IDutyManager _dutyManager;
        IPatrolManager _patrolManager;
        
        public DutyAndPatrolService(IDutyManager dutyManager, IPatrolManager patrolManager)
        {
            _dutyManager = dutyManager;
            _patrolManager = patrolManager;
        }
        /// <summary>
        /// 获取值班记录列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetDataDutyOutput>> GetDutylist(GetDataDutyInput input, PagedResultRequestDto dto)
        {
            return await _dutyManager.GetDutylist(input, dto);
        }
        /// <summary>
        /// 获取值班记录详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetDataDutyInfoOutput> GetDutyInfo(int dutyId)
        {
            return await _dutyManager.GetDutyInfo(dutyId);
        }
        /// <summary>
        /// 新增值班记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddDutyInfo(AddDataDutyInfoInput input)
        {
            await _dutyManager.AddDutyInfo(input);
        }
        /// <summary>
        /// 获取值班记录日历列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<GetDataForCalendarOutput>> GetDutylistForCalendar(GetDataForCalendarInput input)
        {
            return await _dutyManager.GetDutylistForCalendar(input);
        }
        /// <summary>
        /// 获取值班记录状态统计
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetDutyStatusTotalOutput> GetDutyStateTotal(int fireUnitId)
        {
            return await _dutyManager.GetDutyStateTotal(fireUnitId);
        }


        /// <summary>
        /// 获取巡查记录列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetPatrolListOutput>> GetPatrolList(GetDataPatrolInput input, PagedResultRequestDto dto)
        {
            return await _patrolManager.GetPatrolList(input, dto);
        }
        /// <summary>
        /// 获取巡查记录状态统计
        /// </summary>
        /// <param name="fireunitId"></param>
        /// <returns></returns>
        public async Task<GetPatrolStatusTotalOutput> GetPatrolStatusTotal(int fireunitId)
        {
            return await _patrolManager.GetPatrolStatusTotal(fireunitId);
        }
        /// <summary>
        /// 获取防火单位的巡查类别
        /// </summary>
        /// <param name="fireUnitId"></param>
        /// <returns></returns>
        public async Task<PatrolType> GetPatrolType(int fireUnitId)
        {
            return await _patrolManager.GetPatrolType(fireUnitId);
        }
        /// <summary>
        /// 是否允许新增巡查记录（如果存在未提交的巡查记录，则不允许新增）
        /// </summary>
        /// <param name="fireUnitId"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> GetAddPatrolAllow(int fireUnitId)
        {
            return await _patrolManager.GetAddPatrolAllow(fireUnitId);
        }
        /// <summary>
        /// 提交巡查轨迹点
        /// </summary>
        /// <param name="input"></param>
        /// <returns>返回巡查主记录Id</returns>
        public async Task<int> SubmitPatrolDetail(AddPatrolDetailInput input)
        {
            return await _patrolManager.SubmitPatrolDetail(input);
        }
        /// <summary>
        /// 提交巡查主记录
        /// </summary>
        /// <param name="patrolId"></param>
        /// <returns></returns>
        public async Task SubmitPatrol(int patrolId)
        {
            await _patrolManager.SubmitPatrol(patrolId);
        }
        /// <summary>
        /// 获取巡查记录详情
        /// </summary>
        /// <param name="patrolId"></param>
        /// <returns></returns>
        public async Task<GetPatrolInfoOutput> GetPatrolInfo(int patrolId)
        {
            return await _patrolManager.GetPatrolInfo(patrolId);
        }
        /// <summary>
        /// 获取巡查记录日历列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<GetDataForCalendarOutput>> GetPatrollistForCalendar(GetDataForCalendarInput input)
        {
            return await _patrolManager.GetPatrollistForCalendar(input);
        }
    }
}
