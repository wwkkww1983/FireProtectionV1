﻿using Abp.Application.Services.Dto;
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
        /// 导出防火单位列表excel
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task GetFireUnitListExcel(GetFireUnitListInput input)
        {
            var lst = await _fireUnitManager.GetFireUnitListExcel(input);

            throw new NotImplementedException();
        }
        /// <summary>
        /// 防火单位分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetFireUnitListOutput>> GetFireUnitList(GetFireUnitListInput input)
        {
            return await _fireUnitManager.GetFireUnitList(input);
        }
        /// <summary>
        /// 防火单位分页列表(手机端)
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
        /// 防火单位信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnitInfoOutput> GetFireUnitInfo(GetFireUnitInfoInput input)
        {
            return await _fireUnitManager.GetFireUnitInfo(input);
        }
        /// <summary>
        /// 防火单位消防数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnitAlarmOutput> GetFireUnitAlarm(GetByFireUnitIdInput input)
        {
            return await _fireWorkingManager.GetFireUnitAlarm(input);
        }
        /// <summary>
        /// 安全用电最近30天报警记录查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<AlarmRecord>> GetFireUnit30DayAlarmEle(GetPageByFireUnitIdInput input)
        {
            return await _fireWorkingManager.GetFireUnit30DayAlarmEle(input);
        }
        /// <summary>
        /// 火警预警最近30天报警记录查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<AlarmRecord>> GetFireUnit30DayAlarmFire(GetPageByFireUnitIdInput input)
        {
            return await _fireWorkingManager.GetFireUnit30DayAlarmFire(input);
        }
        /// <summary>
        /// 安全用电高频报警部件查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<HighFreqAlarmDetector>> GetFireUnitHighFreqAlarmEle(GetPageByFireUnitIdInput input)
        {
            return await _fireWorkingManager.GetFireUnitHighFreqAlarmEle(input);
        }
        /// <summary>
        /// 火警预警高频报警部件查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<HighFreqAlarmDetector>> GetFireUnitHighFreqAlarmFire(GetPageByFireUnitIdInput input)
        {
            return await _fireWorkingManager.GetFireUnitHighFreqAlarmFire(input);
        }
        /// <summary>
        /// 设备设施故障待处理故障查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<PendingFault>> GetFireUnitPendingFault(GetPageByFireUnitIdInput input)
        {
            return await _fireWorkingManager.GetFireUnitPendingFault(input);
        }
    }
}
