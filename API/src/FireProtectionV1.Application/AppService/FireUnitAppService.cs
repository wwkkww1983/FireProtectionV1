using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Manager;
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
        IFireUnitManager _fireUnitManager;
        IFireWorkingManager _fireWorkingManager;

        public FireUnitAppService(IFireUnitManager fireUnitInfoManager, IFireWorkingManager fireWorkingManager)
        {
            _fireUnitManager = fireUnitInfoManager;
            _fireWorkingManager = fireWorkingManager;
        }
        /// <summary>
        /// 获取防火单位类型数组
        /// </summary>
        /// <returns></returns>
        public async Task<List<GetFireUnitTypeOutput>> GetFireUnitTypes()
        {
            return await _fireUnitManager.GetFireUnitTypes();
        }
        /// <summary>
        /// （所有防火单位）导出防火单位列表excel
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task GetFireUnitListExcel(GetFireUnitListInput input)
        {
            var lst = await _fireUnitManager.GetFireUnitListExcel(input);

            throw new NotImplementedException();
        }
        /// <summary>
        /// （所有防火单位）防火单位分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetFireUnitListOutput>> GetFireUnitList(GetFireUnitListInput input)
        {
            return await _fireUnitManager.GetFireUnitList(input);
        }
        /// <summary>
        /// （所有防火单位）防火单位分页列表（手机端）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetFireUnitListForMobileOutput>> GetFireUnitListForMobile(GetFireUnitListInput input)
        {
            return await _fireUnitManager.GetFireUnitListForMobile(input);
        }
        /// <summary>
        /// 添加防火单位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> Add(AddFireUnitInput input)
        {
            return await _fireUnitManager.Add(input);
        }
        /// <summary>
        /// 修改防火单位信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> Update(UpdateFireUnitInput input)
        {
            return await _fireUnitManager.Update(input);
        }
        /// <summary>
        /// 删除防火单位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> Delete(FireUnitIdInput input)
        {
            return await _fireUnitManager.Delete(input);
        }


        /// <summary>
        /// （单个防火单位）防火单位详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnitInfoOutput> GetFireUnitInfo(GetFireUnitInfoInput input)
        {
            return await _fireUnitManager.GetFireUnitInfo(input);
        }
        /// <summary>
        /// （单个防火单位）防火单位消防数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnitAlarmOutput> GetFireUnitAlarm(FireUnitIdInput input)
        {
            return await _fireWorkingManager.GetFireUnitAlarm(input);
        }
        /// <summary>
        /// （单个防火单位）安全用电最近30天报警记录查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<AlarmRecord>> GetFireUnit30DayAlarmEle(GetPageByFireUnitIdInput input)
        {
            return await _fireWorkingManager.GetFireUnit30DayAlarmEle(input);
        }
        /// <summary>
        /// （单个防火单位）火警预警最近30天报警记录查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<AlarmRecord>> GetFireUnit30DayAlarmFire(GetPageByFireUnitIdInput input)
        {
            return await _fireWorkingManager.GetFireUnit30DayAlarmFire(input);
        }
        /// <summary>
        /// （单个防火单位）安全用电高频报警部件查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<HighFreqAlarmDetector>> GetFireUnitHighFreqAlarmEle(GetPageByFireUnitIdInput input)
        {
            return await _fireWorkingManager.GetFireUnitHighFreqAlarmEle(input);
        }
        /// <summary>
        /// （单个防火单位）安全用电高频报警部件查询（剩余电流）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<HighFreqAlarmDetector>> GetFireUnitHighFreqAlarmElecE(GetPageByFireUnitIdInput input)
        {
            return await _fireWorkingManager.GetFireUnitHighFreqAlarmEle(input,"elec");
        }
        /// <summary>
        /// （单个防火单位）安全用电高频报警部件查询（电缆温度）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<HighFreqAlarmDetector>> GetFireUnitHighFreqAlarmElecT(GetPageByFireUnitIdInput input)
        {
            return await _fireWorkingManager.GetFireUnitHighFreqAlarmEle(input,"temp");
        }
        /// <summary>
        /// （单个防火单位）火警预警高频报警部件查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<HighFreqAlarmDetector>> GetFireUnitHighFreqAlarmFire(GetPageByFireUnitIdInput input)
        {
            return await _fireWorkingManager.GetFireUnitHighFreqAlarmFire(input);
        }
        /// <summary>
        /// （单个防火单位）设备设施故障待处理故障查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<PendingFaultOutput>> GetFireUnitPendingFault(GetPageByFireUnitIdInput input)
        {
            return await _fireWorkingManager.GetFireUnitPendingFault(input);
        }
        /// <summary>
        /// （所有防火单位）火灾报警监控列表
        /// </summary>
        /// <returns></returns>
        public async Task<PagedResultDto<GetAreas30DayFireAlarmOutput>> GetAreas30DayFireAlarmList(GetFireUnitListInput input)
        {
            return await _fireWorkingManager.GetAreas30DayFireAlarmList(input);
        }
        /// <summary>
        /// （所有防火单位）安全用电监控列表（电缆温度）
        /// </summary>
        /// <returns></returns>
        public async Task<PagedResultDto<GetAreas30DayFireAlarmOutput>> GetAreas30DayTempAlarmList(GetFireUnitListInput input)
        {
            return await _fireWorkingManager.GetAreas30DayTempAlarmList(input);
        }
        /// <summary>
        /// （所有防火单位）安全用电监控列表（剩余电流）
        /// </summary>
        /// <returns></returns>
        public async Task<PagedResultDto<GetAreas30DayFireAlarmOutput>> GetAreas30DayElecAlarmList(GetFireUnitListInput input)
        {
            return await _fireWorkingManager.GetAreas30DayElecAlarmList(input);
        }
        /// <summary>
        /// （所有防火单位）设备设施故障监控
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<FireUnitFaultOuput>> GetFireUnitFaultList(GetFireUnitListInput input)
        {
            return await _fireWorkingManager.GetFireUnitFaultList(input);
        }
    }
}
