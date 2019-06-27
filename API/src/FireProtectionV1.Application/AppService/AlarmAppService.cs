using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    public class AlarmAppService : AppServiceBase
    {
        IAlarmManager _alarmManager;
        public AlarmAppService(IAlarmManager alarmManager)
        { 
            _alarmManager = alarmManager;
        }

        /// <summary>
        /// 获取指定防火单位警情数据
        /// </summary>
        /// <param name="FireUnitId">防火单位Id</param>
        /// <returns></returns>
        public async Task<List<AlarmCheckOutput>> GetAlarmChecks(int FireUnitId)
        {
            return await _alarmManager.GetAlarmChecks(FireUnitId);
        }
        /// <summary>
        /// 查询给定checkId的警情详细信息
        /// </summary>
        /// <param name="CheckId"></param>
        /// <returns></returns>
        public async Task<AlarmCheckInput> GetAlarmCheckDetail(int CheckId)
        {
            return await _alarmManager.GetAlarmCheckDetail(CheckId);
        }

        /// <summary>
        /// 核警某一条警情[FromForm]
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> CheckAlarm([FromForm]AlarmCheckInput input)
        {
            var CheckId = input.CheckId;
            var v = input.CheckState;
            throw new NotImplementedException();
            //return await _alarmManager.CheckAlarm(CheckId);
        }
    }
}
