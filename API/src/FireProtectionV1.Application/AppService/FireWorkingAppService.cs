using Abp.Application.Services.Dto;
using FireProtectionV1.Enterprise.Dto;
using FireProtectionV1.Enterprise.Manager;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    public class FireWorkingAppService : AppServiceBase
    {
        IFireWorkingManager _fireWorkingManager;
        public FireWorkingAppService(IFireWorkingManager fireWorkingManager)
        {
            _fireWorkingManager = fireWorkingManager;
        }

        /// <summary>
        /// 防火单位消防数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnitAlarmOutput> GetFireUnitAlarm(GetFireUnitAlarmInput input)
        {
            return await _fireWorkingManager.GetFireUnitAlarm(input);
        }
        /// <summary>
        /// 安全用电最近30天报警记录查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnit30DayAlarmEleOutput> GetFireUnit30DayAlarmEle(GetFireUnit30DayAlarmEleInput input)
        {
            return await _fireWorkingManager.GetFireUnit30DayAlarmEle(input);
        }
        /// <summary>
        /// 火警预警最近30天报警记录查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnit30DayAlarmFireOutput> GetFireUnit30DayAlarmFire(GetFireUnit30DayAlarmFireInput input)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 安全用电高频报警部件查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnitHighFreqAlarmEleOutput> GetFireUnitHighFreqAlarmEle(GetFireUnitHighFreqAlarmEleInput input)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 火警预警高频报警部件查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnitHighFreqAlarmFireOutput> GetFireUnitHighFreqAlarmFire(GetFireUnitHighFreqAlarmFireInput input)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 设备设施故障待处理故障查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnitPendingFaultOutput> GetFireUnitPendingFault(GetFireUnitPendingFaultInput input)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 安全用电综合数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetAreasAlarmElectricOutput> GetAreasAlarmElectric(GetAreasAlarmElectricInput input)
        {
            return await _fireWorkingManager.GetAreasAlarmElectric(input);
        }
    }
}
