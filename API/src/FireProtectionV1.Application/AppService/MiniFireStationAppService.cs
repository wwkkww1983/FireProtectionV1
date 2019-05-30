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
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> Add(AddMiniFireStationInput input)
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
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MiniFireStation> GetById(int id)
        {
            return await _manager.GetById(id);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<MiniFireStation>> GetList(GetMiniFireStationListInput input)
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
        public async Task Update(UpdateMiniFireStationInput input)
        {
            await _manager.Update(input);
        }
    }
}
