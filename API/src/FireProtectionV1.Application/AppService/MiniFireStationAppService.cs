using Abp.Application.Services.Dto;
using Abp.Domain.Uow;
using FireProtectionV1.MiniFireStationCore.Dto;
using FireProtectionV1.MiniFireStationCore.Manager;
using FireProtectionV1.MiniFireStationCore.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FireProtectionV1.AppService
{
    /// <summary>
    /// 微型消防站
    /// </summary>
    public class MiniFireStationAppService : HttpContextAppService
    {
        IMiniFireStationManager _manager;

        public MiniFireStationAppService(IMiniFireStationManager manager, IHttpContextAccessor httpContext):base(httpContext)
        {
            _manager = manager;
        }
        /// <summary>
        /// 新增微型消防站活动
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> AddMiniFireAction(MiniFireActionAddDto input)
        {
            return await _manager.AddMiniFireAction(input);
        }
        /// <summary>
        /// 删除指定ID的活动
        /// </summary>
        /// <param name="MiniFireActionId">微型消防站活动ID</param>
        /// <returns></returns>
        public async Task<SuccessOutput> DeleteMiniFireAction(int MiniFireActionId)
        {
            return await _manager.DeleteMiniFireAction(MiniFireActionId);
        }
        /// <summary>
        /// 修改微型消防站活动详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> UpdateMiniFireAction(MiniFireActionDetailDto input)
        {
            return await _manager.UpdateMiniFireAction(input);
        }
        /// <summary>
        /// 获取微型消防站指定活动ID详情
        /// </summary>
        /// <param name="MiniFireActionId">活动ID</param>
        /// <returns></returns>
        public async Task<MiniFireActionDetailDto> GetMiniFireAction(int MiniFireActionId)
        {
            return await _manager.GetMiniFireAction(MiniFireActionId);
        }
        /// <summary>
        /// 获取微型消防站活动列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<MiniFireActionDto>> GetActionList(GetMiniFireListBaseInput input)
        {
            return await _manager.GetActionList(input);
        }
        /// <summary>
        /// 获取微型消防站设施类别
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetMiniFireEquipmentTypes()
        {
            return await _manager.GetMiniFireEquipmentTypes();
        }
        /// <summary>
        /// 获取微型消防站设施类别下属设施
        /// </summary>
        /// <param name="Type">微型消防站设施类别</param>
        /// <returns></returns>
        public async Task<List<MiniFireEquipmentNameOutput>> GetMiniFireEquipmentsByType(string Type)
        {
            return await _manager.GetMiniFireEquipmentsByType(Type);
        }
        /// <summary>
        /// 获取指定ID 的微型消防站设施详情
        /// </summary>
        /// <param name="MiniFireEquipmentId"></param>
        /// <returns></returns>
        public async Task<MiniFireEquipmentDto> GetMiniFireEquipment(int MiniFireEquipmentId)
        {
            return await _manager.GetMiniFireEquipment(MiniFireEquipmentId);
        }
        /// <summary>
        /// 获取指定微型消防站ID的消防设施列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<MiniFireEquipmentDto>> GetMiniFireEquipmentList(GetMiniFireListBaseInput input)
        {
            return await _manager.GetMiniFireEquipmentList(input);
        }
        /// <summary>
        /// 新增微型消防站设施
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> AddMiniFireEquipment(AddMiniFireEquipmentDto input)
        {
            return await _manager.AddMiniFireEquipment(input);
        }
        /// <summary>
        /// 修改微型消防站设施信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> UpdateMiniFireEquipment(MiniFireEquipmentDto input)
        {
            return await _manager.UpdateMiniFireEquipment(input);
        }
        /// <summary>
        /// 删除微型消防站设施
        /// </summary>
        /// <param name="MiniFireEquipmentId">设施ID</param>
        /// <returns></returns>
        public async Task<SuccessOutput> DeleteMiniFireEquipment(int MiniFireEquipmentId)
        {
            return await _manager.DeleteMiniFireEquipment(MiniFireEquipmentId);
        }
        /// <summary>
        /// 获取微型消防站人员详情
        /// </summary>
        /// <param name="JobUserId">微型消防站人员ID</param>
        /// <returns></returns>
        public async Task<MiniFireJobUserDetailDto> GetJobUserDetail(int JobUserId)
        {
            return await _manager.GetJobUserDetail(JobUserId);
        }
        /// <summary>
        /// 删除指定微型消防站人员
        /// </summary>
        /// <param name="JobUserId">微型消防站人员ID</param>
        /// <returns></returns>
        public async Task<SuccessOutput> DeleteJobUser(int JobUserId)
        {
            return await _manager.DeleteJobUser(JobUserId);
        }
        /// <summary>
        /// 修改指定微型消防站人员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> UpdateJobUser(MiniFireJobUserDetailDto input)
        {
            return await _manager.UpdateJobUser(input);
        }
        /// <summary>
        /// 添加指定微型消防站人员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> AddJobUser(AddMiniFireJobUserDto input)
        {
            return await _manager.AddJobUser(input);
        }
        /// <summary>
        /// 获取指定ID的微型消防站人员列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<MiniFireJobUserDto>> GetMiniFireJobUser(GetMiniFireJobUserInput input)
        {
            return await _manager.GetMiniFireJobUser(input);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AddMiniFireStationOutput> Add(AddMiniFireStationInput input)
        {
            return await _manager.Add(input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<SuccessOutput> Delete(DeletMiniFireStationInput input)
        {
            return await _manager.Delete(input);
        }

        /// <summary>
        /// 获取单个微型消防站信息
        /// </summary>
        /// <param name="id">微型消防站ID</param>
        /// <returns></returns>
        public async Task<MiniFireStationOutput> GetById(int id)
        {
            return await _manager.GetById(id);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<MiniFireStationOutput>> GetList(GetMiniFireStationListInput input)
        {
            return await _manager.GetList(input);
        }

        /// <summary>
        /// 微型消防站Excel导出
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task GetStationExcel(GetMiniFireStationListInput input)
        {
            var lst = await _manager.GetStationExcel(input);
            using (ExcelBuild excel = new ExcelBuild())
            {
                var sheet = excel.BuildWorkSheet("微信消防站列表");
                sheet.AddRowValues(new string[] { "站点名称", "联系人", "联系电话", "人员配备", "区域"}, true);
                foreach (var v in lst)
                {
                    sheet.AddRowValues(new string[]{
                        v.Name,v.ContactName,v.ContactPhone,v.PersonNum.ToString(),v.Address});
                }
                var fileBytes = excel.BuildFileBytes();
                HttpResponse Response = _httpContext.HttpContext.Response;
                Response.ContentType = "application/vnd.ms-excel";
                Response.ContentLength = fileBytes.Length;
                Response.Headers.Add("Content-Disposition", $"attachment;filename={HttpUtility.UrlEncode("微信消防站列表", Encoding.UTF8)}.xls");
                Response.Body.Write(fileBytes);
                Response.Body.Flush();
            }
        }

        /// <summary>
        /// 根据坐标点获取附近1KM直线距离内的微型消防站
        /// </summary>
        /// <param name="lng"></param>
        /// <param name="lat"></param>
        /// <returns></returns>
        public async Task<List<GetNearbyStationOutput>> GetNearbyStation(decimal lng, decimal lat)
        {
            return await _manager.GetNearbyStation(lng, lat);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<SuccessOutput> Update(UpdateMiniFireStationInput input)
        {
            return await _manager.Update(input);
        }
    }
}
