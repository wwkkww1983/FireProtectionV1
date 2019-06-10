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
using System.Web;
using FireProtectionV1.Common.Enum;
using System.Linq;

namespace FireProtectionV1.AppService
{
    /// <summary>
    /// 防火单位服务
    /// </summary>
    public class FireUnitAppService : HttpContextAppService
    {
        IFireUnitManager _fireUnitManager;
        IFireWorkingManager _fireWorkingManager;
        IPatrolManager _patrolManager;
        IDeviceManager _deviceManager;

        public FireUnitAppService(
            IDeviceManager deviceManager,
            IPatrolManager patrolManager,
            IHttpContextAccessor httpContext,
            IFireUnitManager fireUnitInfoManager, 
            IFireWorkingManager fireWorkingManager)
            :base(httpContext)
        {
            _deviceManager = deviceManager;
            _patrolManager = patrolManager;
            _fireUnitManager = fireUnitInfoManager;
            _fireWorkingManager = fireWorkingManager;
        }
        /// <summary>
        /// 获取网关状态类型
        /// </summary>
        /// <returns></returns>
        public Task<List<GatewayStatusTypeOutput>> GetGatewayStatusTypes()
        {
            var lst = new List<GatewayStatusTypeOutput>();
            lst.Add(new GatewayStatusTypeOutput() { GatewayStatusValue = "", GatewayStatusName = "全部" });
            lst.Add(new GatewayStatusTypeOutput(){ GatewayStatusValue = GatewayStatus.Offline.ToString(), GatewayStatusName = "离线" });
            lst.Add(new GatewayStatusTypeOutput() { GatewayStatusValue = GatewayStatus.Online.ToString(), GatewayStatusName = "在线" });
            lst.Add(new GatewayStatusTypeOutput() { GatewayStatusValue = GatewayStatus.Unusual.ToString(), GatewayStatusName = "异常" });
            return Task.FromResult< List<GatewayStatusTypeOutput>>(lst);
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
        public async Task GetFireUnitListExcel(GetPagedFireUnitListInput input)
        {
            var lst = await _fireUnitManager.GetFireUnitListExcel(input);
            using (ExcelBuild excel = new ExcelBuild())
            {
                var sheet = excel.BuildWorkSheet("防火单位");
                sheet.AddRowValues(new string[] { "单位名称", "类型", "区域", "联系人", "联系电话", "维保单位", "邀请码" },true);
                foreach (var v in lst)
                {
                    sheet.AddRowValues(new string[]{
                        v.Name,v.Type,v.Area,v.ContractName,v.ContractPhone,v.SafeUnit,v.InvitationCode });
                }
                var fileBytes = excel.BuildFileBytes();
                HttpResponse Response = _httpContext.HttpContext.Response;
                Response.ContentType = "application/vnd.ms-excel";
                Response.ContentLength = fileBytes.Length;
                Response.Headers.Add("Content-Disposition", $"attachment;filename={HttpUtility.UrlEncode("防火单位列表",Encoding.UTF8)}.xls");
                Response.Body.Write(fileBytes);
                Response.Body.Flush();
            }
        }
        /// <summary>
        /// （所有防火单位）防火单位分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetFireUnitListOutput>> GetFireUnitList(GetPagedFireUnitListInput input)
        {
            return await _fireUnitManager.GetFireUnitList(input);
        }
        /// <summary>
        /// （所有防火单位）防火单位分页列表（手机端）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetFireUnitListForMobileOutput>> GetFireUnitListForMobile(GetPagedFireUnitListInput input)
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
        /// 地图加载所需使用到的防火单位列表数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<GetFireUnitMapListOutput>> GetMapList()
        {
            return await _fireUnitManager.GetMapList();
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
            return await _fireWorkingManager.GetFireUnit30DayAlarmEle(input,-1);
        }

        /// <summary>
        /// （单个防火单位）安全用电最近30天(剩余电流)报警记录查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<AlarmRecord>> GetFireUnit30DayAlarmElecE(GetPageByFireUnitIdInput input)
        {
            var detectorType = _deviceManager.GetDetectorType((byte)DeviceServer.Tcp.Protocol.UnitType.ElectricResidual);
            return await _fireWorkingManager.GetFireUnit30DayAlarmEle(input, detectorType.Id);
        }
        /// <summary>
        /// （单个防火单位）安全用电最近30天(电缆温度)报警记录查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<AlarmRecord>> GetFireUnit30DayAlarmElecT(GetPageByFireUnitIdInput input)
        {
            var detectorType = _deviceManager.GetDetectorType((byte)DeviceServer.Tcp.Protocol.UnitType.ElectricTemperature);
            return await _fireWorkingManager.GetFireUnit30DayAlarmEle(input, detectorType.Id);
        }        /// <summary>
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
        /// （所有防火单位）火灾报警监控列表Excel导出
        /// </summary>
        /// <returns></returns>
        public async Task GetAreas30DayFireAlarmListExcel(GetFireUnitListFilterTypeInput input)
        {
            var data= await _fireWorkingManager.GetAreas30DayFireAlarmList(new GetPagedFireUnitListFilterTypeInput() {
                MaxResultCount=10000,
                FireUnitTypeId=input.FireUnitTypeId,
                Name=input.Name,
                GetwayStatusValue=input.GetwayStatusValue,
                UserId=input.UserId
            });
            if(data.TotalCount>10000)
            {
                data = await _fireWorkingManager.GetAreas30DayFireAlarmList(new GetPagedFireUnitListFilterTypeInput()
                {
                    MaxResultCount = data.TotalCount,
                    FireUnitTypeId = input.FireUnitTypeId,
                    Name = input.Name,
                    GetwayStatusValue = input.GetwayStatusValue,
                    UserId = input.UserId
                });
            }
            var lst = from a in data.Items
                      select new
                      {
                          a.FireUnitName,
                          a.TypeName,
                          a.AlarmTime,
                          a.AlarmCount,
                          a.HighFreqCount,
                          a.StatusName
                      };
            using (ExcelBuild excel = new ExcelBuild())
            {
                var sheet = excel.BuildWorkSheet("火灾报警监控列表");
                sheet.AddRowValues(new string[] { "单位名称", "类型", "最后报警时间", "最近30天报警次数", "高频报警部件数量", "网关状态" }, true);
                foreach (var v in lst)
                {
                    sheet.AddRowValues(new string[]{
                        v.FireUnitName,v.TypeName,v.AlarmTime,v.AlarmCount.ToString(),v.HighFreqCount.ToString(),v.StatusName });
                }
                var fileBytes = excel.BuildFileBytes();
                HttpResponse Response = _httpContext.HttpContext.Response;
                Response.ContentType = "application/vnd.ms-excel";
                Response.ContentLength = fileBytes.Length;
                Response.Headers.Add("Content-Disposition", $"attachment;filename={HttpUtility.UrlEncode("火灾报警监控列表", Encoding.UTF8)}.xls");
                Response.Body.Write(fileBytes);
                Response.Body.Flush();
            }
        }
        /// <summary>
        /// （所有防火单位）火灾报警监控列表
        /// </summary>
        /// <returns></returns>
        public async Task<PagedResultDto<GetAreas30DayFireAlarmOutput>> GetAreas30DayFireAlarmList(GetPagedFireUnitListFilterTypeInput input)
        {

            return await _fireWorkingManager.GetAreas30DayFireAlarmList(input);
        }
        /// <summary>
        /// （所有防火单位）安全用电监控列表Excel导出（电缆温度）
        /// </summary>
        /// <returns></returns>
        public async Task GetAreas30DayTempAlarmListExcel(GetFireUnitListFilterTypeInput input)
        {
            var data= await _fireWorkingManager.GetAreas30DayTempAlarmList(new GetPagedFireUnitListFilterTypeInput()
            {
                MaxResultCount = 10000,
                FireUnitTypeId = input.FireUnitTypeId,
                Name = input.Name,
                GetwayStatusValue = input.GetwayStatusValue,
                UserId = input.UserId
            });
            if (data.TotalCount > 10000)
            {
                data = await _fireWorkingManager.GetAreas30DayTempAlarmList(new GetPagedFireUnitListFilterTypeInput()
                {
                    MaxResultCount = data.TotalCount,
                    FireUnitTypeId = input.FireUnitTypeId,
                    Name = input.Name,
                    GetwayStatusValue = input.GetwayStatusValue,
                    UserId = input.UserId
                });
            }
            var lst = from a in data.Items
                      select new
                      {
                          a.FireUnitName,
                          a.TypeName,
                          a.AlarmTime,
                          a.AlarmCount,
                          a.HighFreqCount,
                          a.StatusName
                      };
            using (ExcelBuild excel = new ExcelBuild())
            {
                var sheet = excel.BuildWorkSheet("安全用电电缆温度监控列表");
                sheet.AddRowValues(new string[] { "单位名称", "类型", "最后报警时间", "最近30天报警次数", "高频报警部件数量", "网关状态" }, true);
                foreach (var v in lst)
                {
                    sheet.AddRowValues(new string[]{
                        v.FireUnitName,v.TypeName,v.AlarmTime,v.AlarmCount.ToString(),v.HighFreqCount.ToString(),v.StatusName });
                }
                var fileBytes = excel.BuildFileBytes();
                HttpResponse Response = _httpContext.HttpContext.Response;
                Response.ContentType = "application/vnd.ms-excel";
                Response.ContentLength = fileBytes.Length;
                Response.Headers.Add("Content-Disposition", $"attachment;filename={HttpUtility.UrlEncode("安全用电电缆温度监控列表", Encoding.UTF8)}.xls");
                Response.Body.Write(fileBytes);
                Response.Body.Flush();
            }
        }
        /// <summary>
        /// （所有防火单位）安全用电监控列表（电缆温度）
        /// </summary>
        /// <returns></returns>
        public async Task<PagedResultDto<GetAreas30DayFireAlarmOutput>> GetAreas30DayTempAlarmList(GetPagedFireUnitListFilterTypeInput input)
        {
            return await _fireWorkingManager.GetAreas30DayTempAlarmList(input);
        }
        /// <summary>
        /// （所有防火单位）安全用电监控列表Excel导出（剩余电流）
        /// </summary>
        /// <returns></returns>
        public async Task GetAreas30DayElecAlarmListExcel(GetFireUnitListFilterTypeInput input)
        {
            var data = await _fireWorkingManager.GetAreas30DayElecAlarmList(new GetPagedFireUnitListFilterTypeInput()
            {
                MaxResultCount = 10000,
                FireUnitTypeId = input.FireUnitTypeId,
                Name = input.Name,
                GetwayStatusValue = input.GetwayStatusValue,
                UserId = input.UserId
            });
            if (data.TotalCount > 10000)
            {
                data = await _fireWorkingManager.GetAreas30DayElecAlarmList(new GetPagedFireUnitListFilterTypeInput()
                {
                    MaxResultCount = data.TotalCount,
                    FireUnitTypeId = input.FireUnitTypeId,
                    Name = input.Name,
                    GetwayStatusValue = input.GetwayStatusValue,
                    UserId = input.UserId
                });
            }
            var lst = from a in data.Items
                      select new
                      {
                          a.FireUnitName,
                          a.TypeName,
                          a.AlarmTime,
                          a.AlarmCount,
                          a.HighFreqCount,
                          a.StatusName
                      };
            using (ExcelBuild excel = new ExcelBuild())
            {
                var sheet = excel.BuildWorkSheet("安全用电剩余电流监控列表");
                sheet.AddRowValues(new string[] { "单位名称", "类型", "最后报警时间", "最近30天报警次数", "高频报警部件数量", "网关状态" }, true);
                foreach (var v in lst)
                {
                    sheet.AddRowValues(new string[]{
                        v.FireUnitName,v.TypeName,v.AlarmTime,v.AlarmCount.ToString(),v.HighFreqCount.ToString(),v.StatusName });
                }
                var fileBytes = excel.BuildFileBytes();
                HttpResponse Response = _httpContext.HttpContext.Response;
                Response.ContentType = "application/vnd.ms-excel";
                Response.ContentLength = fileBytes.Length;
                Response.Headers.Add("Content-Disposition", $"attachment;filename={HttpUtility.UrlEncode("安全用电剩余电流监控列表", Encoding.UTF8)}.xls");
                Response.Body.Write(fileBytes);
                Response.Body.Flush();
            }
        }
        /// <summary>
        /// （所有防火单位）安全用电监控列表（剩余电流）
        /// </summary>
        /// <returns></returns>
        public async Task<PagedResultDto<GetAreas30DayFireAlarmOutput>> GetAreas30DayElecAlarmList(GetPagedFireUnitListFilterTypeInput input)
        {
            return await _fireWorkingManager.GetAreas30DayElecAlarmList(input);
        }
        /// <summary>
        /// （所有防火单位）设备设施故障监控Excel导出
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task GetFireUnitFaultListExcel(GetFireUnitListFilterTypeInput input)
        {
            var data = await _fireWorkingManager.GetFireUnitFaultList(new GetPagedFireUnitListFilterTypeInput()
            {
                MaxResultCount = 10000,
                FireUnitTypeId = input.FireUnitTypeId,
                Name = input.Name,
                GetwayStatusValue = input.GetwayStatusValue,
                UserId = input.UserId
            });
            if (data.TotalCount > 10000)
            {
                data = await _fireWorkingManager.GetFireUnitFaultList(new GetPagedFireUnitListFilterTypeInput()
                {
                    MaxResultCount = data.TotalCount,
                    FireUnitTypeId = input.FireUnitTypeId,
                    Name = input.Name,
                    GetwayStatusValue = input.GetwayStatusValue,
                    UserId = input.UserId
                });
            }
            var lst = from a in data.Items
                      select new
                      {
                          a.FireUnitName,
                          a.FaultCount,
                          a.ProcessedCount,
                          a.PendingCount
                      };
            using (ExcelBuild excel = new ExcelBuild())
            {
                var sheet = excel.BuildWorkSheet("安全用电剩余电流监控列表");
                sheet.AddRowValues(new string[] { "单位名称", "故障数量", "已处理故障数量", "待处理故障数量" }, true);
                foreach (var v in lst)
                {
                    sheet.AddRowValues(new string[]{
                        v.FireUnitName,v.FaultCount.ToString(),v.ProcessedCount.ToString(),v.PendingCount.ToString() });
                }
                var fileBytes = excel.BuildFileBytes();
                HttpResponse Response = _httpContext.HttpContext.Response;
                Response.ContentType = "application/vnd.ms-excel";
                Response.ContentLength = fileBytes.Length;
                Response.Headers.Add("Content-Disposition", $"attachment;filename={HttpUtility.UrlEncode("设备设施故障监控", Encoding.UTF8)}.xls");
                Response.Body.Write(fileBytes);
                Response.Body.Flush();
            }
        }
        /// <summary>
        /// （所有防火单位）设备设施故障监控
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<FireUnitFaultOuput>> GetFireUnitFaultList(GetPagedFireUnitListInput input)
        {
            return await _fireWorkingManager.GetFireUnitFaultList(input);
        }
        /// <summary>
        /// （所有防火单位）值班巡查监控（巡查记录）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnitPatrolListOutput> GetFireUnitPatrolList(GetPagedFireUnitListInput input)
        {
            var output = new GetFireUnitPatrolListOutput();
            await Task.Run(() =>
            {
                output.NoWork7DayCount = _patrolManager.GetNoPatrol7DayFireUnits().Count();
                var query = _patrolManager.GetPatrolFireUnitsAll(input.Name).OrderByDescending(p => p.LastTime);
                output.PagedResultDto = new PagedResultDto<FireUnitManualOuput>(query.Count()
                    , query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList());
            });
            return output;
        }
        /// <summary>
        /// （所有防火单位）值班巡查监控（值班记录）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnitDutyListOutput> GetFireUnitDutyList(GetPagedFireUnitListInput input)
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
            var output = new GetFireUnitPatrolListOutput();
            await Task.Run(() =>
            {
                var fireunits = _patrolManager.GetNoPatrol7DayFireUnits();
                output.NoWork7DayCount = fireunits.Count();
                var query = from a in fireunits
                            join b in _patrolManager.GetPatrolFireUnitsAll().ToList()
                            on a.FireUnitId equals b.FireUnitId
                            orderby b.LastTime descending
                            select b;
                output.PagedResultDto = new PagedResultDto<FireUnitManualOuput>(query.Count()
                    , query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList());
            });
            return output;
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
