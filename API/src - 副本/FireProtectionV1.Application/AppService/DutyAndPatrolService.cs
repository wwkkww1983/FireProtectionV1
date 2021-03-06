﻿using FireProtectionV1.FireWorking.Dto;
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
        public async Task<GetDataDutyPagingOutput> GetDutylist(GetDataDutyInput input)
        {
            return await _dutyManager.GetDutylist(input);
        }

        /// <summary>
        /// 获取值班记录详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetDataDutyInfoOutput> GetDutyInfo(GetDataDutyInfoInput input)
        {
            return await _dutyManager.GetDutyInfo(input);
        }
        /// <summary>
        /// 新增值班记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> AddDutyInfo([FromForm]AddDataDutyInfoInput input)
        {
            return await _dutyManager.AddDutyInfo(input);
        }
        /// <summary>
        /// Web获取日期控件信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<GetDataDutyForWebOutput>> GetDutylistForWeb(GetDataDutyForWebInput input)
        {
            return await _dutyManager.GetDutylistForWeb(input);
        }
        /// <summary>
        /// Web获取值班记录统计
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetDataDutyTotalOutput> GetDutyTotal(GetDataDutyTotalInput input)
        {
            return await _dutyManager.GetDutyTotal(input);
        }
        /// <summary>
        /// Web获取值班记录详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<GetDutyInfoForWebOutput>> GetDutyInfoForWeb(GetDataDutyInfoForWebInput input)
        {
            return await _dutyManager.GetDutyInfoForWeb(input);
        }

        /// <summary>
        /// 获取巡查记录列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetDataPatrolPagingOutput> GetPatrollist(GetDataPatrolInput input)
        {
            return await _patrolManager.GetPatrollist(input);
        }

        /// <summary>
        /// 获取巡查记录轨迹
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<GetPatrolTrackOutput>> GetPatrolTrackList(GetPatrolTrackInput input)
        {
            return await _patrolManager.GetPatrolTrackList(input);
        }
        /// <summary>
        /// 获取防火单位消防系统
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<GetPatrolFireUnitSystemOutput>> GetFireUnitlSystem(GetPatrolFireUnitSystemInput input)
        {
            return await _patrolManager.GetFireUnitlSystem(input);
        }
        /// <summary>
        /// 添加巡查记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AddPatrolOutput> AddPatrolTrack(AddPatrolInput input)
        {
            return await _patrolManager.AddPatrolTrack(input);
        }
        /// <summary>
        /// 添加巡查记录轨迹(所有)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> AddPatrolTrackDetailAll([FromForm]AddPatrolTrackAllInput input)
        {
            return await _patrolManager.AddPatrolTrackDetailAll(input);
        }
        /// <summary>
        /// 添加巡查记录轨迹
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> AddPatrolTrackDetail([FromForm]AddPatrolTrackInput input)
        {
            return await _patrolManager.AddPatrolTrackDetail(input);
        }

        /// <summary>
        /// Web获取日历控件信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<GetDataPatrolForWebOutput>> GetPatrollistForWeb(GetDataPatrolForWebInput input)
        {
            return await _patrolManager.GetPatrollistForWeb(input);
        }
        /// <summary>
        /// Web获取巡查记录统计
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetDataPatrolTotalOutput> GetPatrolTotal(GetDataPatrolTotalInput input)
        {
            return await _patrolManager.GetPatrolTotal(input);
        }
        /// <summary>
        /// Web获取巡查记录轨迹
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetPatrolInfoForWebOutput> GetPatrolInfoForWeb(GetPatrolInfoForWebInput input)
        {
            return await _patrolManager.GetPatrolInfoForWeb(input);
        }
        /// <summary>
        /// 新增时获取巡查记录类别
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetPatrolTypeOutput> GetPatrolType(GetPatrolFireUnitSystemInput input)
        {
            return await _patrolManager.GetPatrolType(input);
        }
        /// <summary>
        /// 新增时查询今日是否已添加
        /// </summary>
        /// <param name="FireUnitId">防火单位ID</param>
        /// <returns></returns>
        public async Task<SuccessOutput> GetAddAllow(int FireUnitId)
        {
            return await _patrolManager.GetAddAllow(FireUnitId);
        }
    }
}
