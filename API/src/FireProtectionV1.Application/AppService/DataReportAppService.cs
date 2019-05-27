using FireProtectionV1.DataReportCore.Dto;
using FireProtectionV1.DataReportCore.Manager;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    public class DataReportAppService : AppServiceBase
    {
        IDataReportManager _manager;
        IFireWorkingManager _fireWorkingManager;

        public DataReportAppService(IDataReportManager manager, IFireWorkingManager fireWorkingManager)
        {
            _manager = manager;
            _fireWorkingManager = fireWorkingManager;
        }

        /// <summary>
        /// 数据监控页
        /// </summary>
        /// <returns></returns>
        public async Task<List<DataMinotorOutput>> GetDataMinotor()
        {
            return await _manager.GetDataMinotor();
        }
        /// <summary>
        /// 安全用电数据分析
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<FireWorking.Dto.GetAreasAlarmElectricOutput> GetAreasAlarmElectric(GetAreasAlarmElectricInput input)
        {
            return await _fireWorkingManager.GetAreasAlarmElectric(input);
        }
        /// <summary>
        /// 火警预警数据分析
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetAreasAlarmFireOutput> GetAreasAlarmFire(GetAreasAlarmFireInput input)
        {
            return await _fireWorkingManager.GetAreasAlarmFire(input);
        }
        ///// <summary>
        ///// 值班巡查数据分析（巡查）
        ///// </summary>
        ///// <param name="UserId">用户Id</param>
        ///// <returns></returns>
        //public async Task<GetAreasAlarmFireOutput> GetAreasPatrol(int UserId)
        //{
        //    return await _fireWorkingManager.GetAreasPatrol();
        //}
        ///// <summary>
        ///// 值班巡查数据分析（值班）
        ///// </summary>
        ///// <param name="UserId">用户Id</param>
        ///// <returns></returns>
        //public async Task<GetAreasAlarmFireOutput> GetAreasDuty(int UserId)
        //{
        //    return await _fireWorkingManager.GetAreasDuty();
        //}
    }
}
