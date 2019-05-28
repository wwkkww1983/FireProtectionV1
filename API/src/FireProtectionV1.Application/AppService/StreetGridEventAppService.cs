using Abp.Application.Services.Dto;
using FireProtectionV1.StreetGridCore.Dto;
using FireProtectionV1.StreetGridCore.Manager;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FireProtectionV1.AppService
{
    public class StreetGridEventAppService : HttpContextAppService
    {
        IStreetGridEventManager _manager;

        public StreetGridEventAppService(IStreetGridEventManager manager, IHttpContextAccessor httpContext) : base(httpContext)
        {
            _manager = manager;
        }

        /// <summary>
        /// 获取单个实体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GetEventByIdOutput> GetEventById(int id)
        {
            return await _manager.GetEventById(id);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetStreeGridEventListOutput>> GetList(GetStreetGridEventListInput input)
        {
            return await _manager.GetList(input);
        }

        /// <summary>
        /// 街道事件Excel导出
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task GetStreetEventExcel(GetStreetGridEventListInput input)
        {
            var lst = await _manager.GetStreetEventExcel(input);
            using (ExcelBuild excel = new ExcelBuild())
            {
                var sheet = excel.BuildWorkSheet("街道事件列表");
                sheet.AddRowValues(new string[] { "事件标题","所属网格" ,"所属街道", "所属社区", "受理时间", "事件状态" }, true);
                foreach (var v in lst)
                {
                    sheet.AddRowValues(new string[]{
                        v.Title,v.GridName,v.Street,v.Community,v.CreationTime.ToUniversalTime().ToString(),v.Status.ToString()});
                }
                var fileBytes = excel.BuildFileBytes();
                HttpResponse Response = _httpContext.HttpContext.Response;
                Response.ContentType = "application/vnd.ms-excel";
                Response.ContentLength = fileBytes.Length;
                Response.Headers.Add("Content-Disposition", $"attachment;filename={HttpUtility.UrlEncode("街道事件列表", Encoding.UTF8)}.xls");
                Response.Body.Write(fileBytes);
                Response.Body.Flush();
            }
        }
    }
}
