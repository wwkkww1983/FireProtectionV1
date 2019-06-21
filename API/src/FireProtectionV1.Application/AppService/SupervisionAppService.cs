using Abp.Application.Services.Dto;
using FireProtectionV1.SupervisionCore.Dto;
using FireProtectionV1.SupervisionCore.Manager;
using FireProtectionV1.SupervisionCore.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FireProtectionV1.AppService
{
    public class SupervisionAppService : HttpContextAppService
    {
        ISupervisionManager _manager;

        public SupervisionAppService(ISupervisionManager manager,IHttpContextAccessor httpContext) : base(httpContext)
        {
            _manager = manager;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetSupervisionListOutput>> GetList(GetSupervisionListInput input)
        {
            return await _manager.GetList(input);
        }
        /// <summary>
        /// 监督查询EXCEL导出
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task GetSupervisionListExcel(GetSupervisionListInput input)
        {
            var lst = await _manager.GetSupervisionListExcel(input);
            using (ExcelBuild excel = new ExcelBuild())
            {
                var sheet = excel.BuildWorkSheet("监督检查");
                sheet.AddRowValues(new string[] { "防火单位", "最近检查日期", "检察人员", "检查结论" }, true);
                foreach (var v in lst)
                {
                    sheet.AddRowValues(new string[]{
                        v.FireUnitName,v.CreationTime.ToUniversalTime().ToString(),v.CheckUser,v.CheckResult.ToString()});
                }
                var fileBytes = excel.BuildFileBytes();
                HttpResponse Response = _httpContext.HttpContext.Response;
                Response.ContentType = "application/vnd.ms-excel";
                Response.ContentLength = fileBytes.Length;
                Response.Headers.Add("Content-Disposition", $"attachment;filename={HttpUtility.UrlEncode("监督检查列表", Encoding.UTF8)}.xls");
                Response.Body.Write(fileBytes);
                Response.Body.Flush();
            }
        }
        /// <summary>
        /// 获取单条执法记录明细项目信息
        /// </summary>
        /// <param name="supervisionId"></param>
        /// <returns></returns>
        public async Task<List<GetSingleSupervisionDetailOutput>> GetSingleSupervisionDetail(int supervisionId)
        {
            return await _manager.GetSingleSupervisionDetail(supervisionId);
        }

        /// <summary>
        /// 获取单条记录主信息
        /// </summary>
        /// <param name="supervisionId"></param>
        /// <returns></returns>
        public async Task<GetSingleSupervisionMainOutput> GetSingleSupervisionMain(int supervisionId)
        {
            return await _manager.GetSingleSupervisionMain(supervisionId);
        }

        /// <summary>
        /// 获取所有监管执法项目
        /// </summary>
        /// <returns></returns>
        public async Task<List<GetSupervisionItemOutput>> GetSupervisionItem()
        {
            return await _manager.GetSupervisionItem();
        }

        /// <summary>
        /// 添加监管执法记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddSupervision(AddSupervisionInput input)
        {
            await _manager.AddSupervision(input);
        }

        /// <summary>
        /// 照片上传(测试用)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UploadPhotosAsync(AddPhotosInput input)
        {
            await _manager.UploadPhotosAsync(input);
        }
    }
}
