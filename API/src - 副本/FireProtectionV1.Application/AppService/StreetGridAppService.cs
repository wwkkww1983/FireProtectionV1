using Abp.Application.Services.Dto;
using FireProtectionV1.StreetGridCore.Dto;
using FireProtectionV1.StreetGridCore.Manager;
using FireProtectionV1.StreetGridCore.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FireProtectionV1.AppService
{
    /// <summary>
    /// 网格
    /// </summary>
    public class StreetGridAppService : HttpContextAppService
    {
        IStreetGridUserManager _userManager;

        public StreetGridAppService(IStreetGridUserManager userManager, IHttpContextAccessor httpContext):base(httpContext)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// 网格员分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetStreetListOutput>> GetUserList(GetStreetGridUserListInput input)
        {
            return await _userManager.GetList(input);
        }

        /// <summary>
        /// 街道网格Excel导出
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task GetStreetGridExcel(GetStreetGridUserListInput input)
        {
            var lst = await _userManager.GetStreetGridExcel(input);
            using (ExcelBuild excel = new ExcelBuild())
            {
                var sheet = excel.BuildWorkSheet("网格列表");
                sheet.AddRowValues(new string[] { "网格名称", "所属街道", "所属社区", "联系人", "联系电话" }, true);
                foreach (var v in lst)
                {
                    sheet.AddRowValues(new string[]{
                        v.GridName,v.Street,v.Community,v.Name,v.Phone});
                }
                var fileBytes = excel.BuildFileBytes();
                HttpResponse Response = _httpContext.HttpContext.Response;
                Response.ContentType = "application/vnd.ms-excel";
                Response.ContentLength = fileBytes.Length;
                Response.Headers.Add("Content-Disposition", $"attachment;filename={HttpUtility.UrlEncode("网格列表", Encoding.UTF8)}.xls");
                Response.Body.Write(fileBytes);
                Response.Body.Flush();
            }
        }
    }
}
