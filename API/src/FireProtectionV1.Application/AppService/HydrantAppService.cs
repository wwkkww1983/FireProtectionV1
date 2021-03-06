﻿using Abp.Application.Services.Dto;
using FireProtectionV1.HydrantCore.Dto;
using FireProtectionV1.HydrantCore.Manager;
using FireProtectionV1.HydrantCore.Model;
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
    /// 市政消火栓
    /// </summary>
    public class HydrantAppService : HttpContextAppService
    {
        IHydrantManager _manager;

        public HydrantAppService(IHydrantManager manager, IHttpContextAccessor httpContext) : base(httpContext)
        {
            _manager = manager;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> Add(AddHydrantInput input)
        {
            return await _manager.Add(input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<SuccessOutput> Delete(DeletHydrantInput input)
        {
            return await _manager.Delete(input);
        }

        /// <summary>
        /// 获取实体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<HydrantDetailOutput> GetInfoById(int id)
        {
            return await _manager.GetInfoById(id);
        }

        /// <summary>
        /// App端分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<Hydrant>> GetListForApp(GetHydrantListInput input)
        {
            return await _manager.GetListForApp(input);
        }

        /// <summary>
        /// Web端分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetPressureSubstandardOutput> GetListForWeb(GetHydrantListInput input)
        {
            return await _manager.GetListForWeb(input);
        }

        /// <summary>
        /// 查询水压低于标准值的消火栓
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetPressureSubstandardOutput> GetPressureSubstandard(GetHydrantListInput input)
        {
            return await _manager.GetPressureSubstandard(input);
        }

        /// <summary>
        /// 消火栓Excel导出
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task GetHydrantExcel(GetHydrantListInput input)
        {
            var lst = await _manager.GetHydrantExcel(input);
            using (ExcelBuild excel = new ExcelBuild())
            {
                var sheet = excel.BuildWorkSheet("消火栓列表");
                sheet.AddRowValues(new string[] { "设施编号", "区域", "水压数据", "最近报警时间", "最近30天报警次数", "网关状态" }, true);
                foreach (var v in lst)
                {
                    sheet.AddRowValues(new string[]{
                        v.Sn,v.AreaName,v.Pressure.ToString(),v.LastAlarmTime,v.NearbyAlarmNumber.ToString(),v.Status.ToString()});
                }
                var fileBytes = excel.BuildFileBytes();
                HttpResponse Response = _httpContext.HttpContext.Response;
                Response.ContentType = "application/vnd.ms-excel";
                Response.ContentLength = fileBytes.Length;
                Response.Headers.Add("Content-Disposition", $"attachment;filename={HttpUtility.UrlEncode("消火栓列表", Encoding.UTF8)}.xls");
                Response.Body.Write(fileBytes);
                Response.Body.Flush();
            }
        }


        /// <summary>
        /// 获取最近30天报警记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetNearAlarmOutput>> GetNearbyAlarmById(GetHydrantAlarmInput input)
        {
            return await _manager.GetNearbyAlarmById(input);
        }

        /// <summary>
        /// 根据坐标点获取附近1KM直线距离内的消火栓
        /// </summary>
        /// <param name="lng"></param>
        /// <param name="lat"></param>
        /// <returns></returns>
        public async Task<List<GetNearbyHydrantOutput>> GetNearbyHydrant(decimal lng, decimal lat)
        {
            return await _manager.GetNearbyHydrant(lng,lat);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Update(UpdateHydrantInput input)
        {
            await _manager.Update(input);
        }



    }
}
