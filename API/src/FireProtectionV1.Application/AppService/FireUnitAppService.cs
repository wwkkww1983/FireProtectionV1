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
using Microsoft.AspNetCore.Http;

namespace FireProtectionV1.AppService
{
    /// <summary>
    /// 防火单位服务
    /// </summary>
    public class FireUnitAppService : HttpContextAppService
    {
        IFireUnitManager _fireUnitManager;
        IFireWorkingManager _fireWorkingManager;

        public FireUnitAppService(
            IHttpContextAccessor httpContext,
            IFireUnitManager fireUnitInfoManager, 
            IFireWorkingManager fireWorkingManager)
            :base(httpContext)
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
            using (ExcelBuild excel = new ExcelBuild())
            {
                var sheet = excel.BuildWorkSheet("防火单位");
                sheet.AddRowValues(new string[] { "单位名称", "类型", "区域", "联系人", "联系电话", "维保单位", "邀请码" });
                foreach (var v in lst)
                {
                    sheet.AddRowValues(new string[]{
                        v.Name,v.Type,v.Area,v.ContractName,v.ContractPhone,v.SafeUnit,v.InvitationCode });
                }
                var fileBytes = excel.BuildFileBytes();
                HttpResponse Response = _httpContext.HttpContext.Response;
                Response.ContentType = "application/vnd.ms-excel";
                Response.ContentLength = fileBytes.Length;
                //Response.Headers.Add("Content-Disposition", string.Format("attachment;filename=防火单位.xls"));
                Response.Body.Write(fileBytes);
                Response.Body.Flush();
            }
            //return File(sFileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
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
        /// （单个防火单位）置顶单个防火单位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> AttentionFireUnit(DeptUserAttentionFireUnitInput input)
        {
            return await _fireUnitManager.AttentionFireUnit(input);
        }
        /// <summary>
        /// （单个防火单位）取消置顶单个防火单位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> AttentionFireUnitCancel(DeptUserAttentionFireUnitInput input)
        {
            return await _fireUnitManager.AttentionFireUnitCancel(input);
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
        public async Task<PagedResultDto<GetAreas30DayFireAlarmOutput>> GetAreas30DayFireAlarmList(GetFireUnitListFilterTypeInput input)
        {
            return await _fireWorkingManager.GetAreas30DayFireAlarmList(input);
        }
        /// <summary>
        /// （所有防火单位）安全用电监控列表（电缆温度）
        /// </summary>
        /// <returns></returns>
        public async Task<PagedResultDto<GetAreas30DayFireAlarmOutput>> GetAreas30DayTempAlarmList(GetFireUnitListFilterTypeInput input)
        {
            return await _fireWorkingManager.GetAreas30DayTempAlarmList(input);
        }
        /// <summary>
        /// （所有防火单位）安全用电监控列表（剩余电流）
        /// </summary>
        /// <returns></returns>
        public async Task<PagedResultDto<GetAreas30DayFireAlarmOutput>> GetAreas30DayElecAlarmList(GetFireUnitListFilterTypeInput input)
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
        /// <summary>
        /// （所有防火单位）值班巡查监控（巡查记录）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnitPatrolListOutput> GetFireUnitPatrolList(GetFireUnitListInput input)
        {
            return await _fireWorkingManager.GetFireUnitPatrolList(input);
        }
        /// <summary>
        /// （所有防火单位）值班巡查监控（值班记录）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnitDutyListOutput> GetFireUnitDutyList(GetFireUnitListInput input)
        {
            return await _fireWorkingManager.GetFireUnitDutyList(input);
        }
        /// <summary>
        /// （所有防火单位）超过7天没有巡查记录的单位列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnitPatrolListOutput> GetNoPatrol7DayFireUnitList(PagedRequestByUserIdDto input)
        {
            return await _fireWorkingManager.GetNoPatrol7DayFireUnitList(input);
        }
        /// <summary>
        /// （所有防火单位）超过1天没有值班记录的单位列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnitDutyListOutput> GetNoDuty1DayFireUnitList(PagedRequestByUserIdDto input)
        {
            return await _fireWorkingManager.GetNoDuty1DayFireUnitList(input);
        }
    }
}
