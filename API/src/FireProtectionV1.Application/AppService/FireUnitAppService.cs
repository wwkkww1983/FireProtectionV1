using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using FireProtectionV1.Alarm.Dto;
using FireProtectionV1.Alarm.Manager;
using FireProtectionV1.Enterprise.Dto;
using FireProtectionV1.Enterprise.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    /// <summary>
    /// 防火单位服务
    /// </summary>
    public class FireUnitAppService : AppServiceBase
    {
        IFireUnitManager _fireUnitInfoManager;

        public FireUnitAppService(IFireUnitManager fireUnitInfoManager)
        {
            _fireUnitInfoManager = fireUnitInfoManager;
        }

        /// <summary>
        /// 添加防火单位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> Add(AddFireUnitInput input)
        {
            return await _fireUnitInfoManager.Add(input);
        }


        /// <summary>
        /// 防火单位信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnitInfoOutput> GetFireUnitInfo(GetFireUnitInfoInput input)
        {
            return await _fireUnitInfoManager.GetFireUnitInfo(input);
        }
        /// <summary>
        /// 防火单位消防数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnitAlarmOutput> GetFireUnitAlarm(GetFireUnitAlarmInput input)
        {
            return await _fireUnitInfoManager.GetFireUnitAlarm(input);
        }
        /// <summary>
        /// 安全用电最近30天报警记录查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<GetFireUnit30DayAlarmEleOutput> GetFireUnit30DayAlarmEle(GetFireUnit30DayAlarmEleInput input)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 火警预警最近30天报警记录查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<GetFireUnit30DayAlarmFireOutput> GetFireUnit30DayAlarmFire(GetFireUnit30DayAlarmFireInput input)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 安全用电高频报警部件查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<GetFireUnitHighFreqAlarmEleOutput> GetFireUnitHighFreqAlarmEle(GetFireUnitHighFreqAlarmEleInput input)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 火警预警高频报警部件查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<GetFireUnitHighFreqAlarmFireOutput> GetFireUnitHighFreqAlarmFire(GetFireUnitHighFreqAlarmFireInput input)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 设备设施故障待处理故障查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<GetFireUnitPendingFaultOutput> GetFireUnitPendingFault(GetFireUnitPendingFaultInput input)
        {
            throw new NotImplementedException();
        }
    }
}
