﻿using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Manager;
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
        public async Task<List<GetDataDutyOutput>> GetDutylist(GetDataDutyInput input)
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
        /// 获取巡查记录列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<GetDataPatrolOutput>> GetPatrollist(GetDataPatrolInput input)
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
    }
}
