using Abp.Application.Services.Dto;
using FireProtectionV1.Enterprise.Dto;
using FireProtectionV1.Enterprise.Manager;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.User.Dto;
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
    /// 维保单位
    /// </summary>
    public class SafeUnitAppService : HttpContextAppService
    {
        
        ISafeUnitManager _safeUnitManager;

        public SafeUnitAppService(ISafeUnitManager manager, IHttpContextAccessor httpContext) : base(httpContext)
        {
            _safeUnitManager = manager;
        }
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> UserRegist(SafeUnitUserRegistInput input)
        {
            return await _safeUnitManager.UserRegist(input);
        }
        /// <summary>
        /// 维保用户登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SafeUserLoginOutput> UserLogin(LoginInput input)
        {
            //用户名密码验证
            var output = await _safeUnitManager.UserLogin(input);
            if (!output.Success)
                return output;
            if (!await Authentication(input.Account, output.Name, input.Password, input.IsPersistent))
            {
                output.Success = false;
                output.FailCause = "认证失败";
            }
            return output;
        }
        /// <summary>
        /// 邀请码验证 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> InvitatVerify(InvitatVerifyDto input)
        {
            return await _safeUnitManager.InvitatVerify(input);
        }
        /// <summary>
        /// 维保人员修改账户密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> ChangePassword(ChangeUserPassword input)
        {
            return await _safeUnitManager.ChangePassword(input);
        }
        /// <summary>
        /// 注销用户
        /// </summary>
        /// <returns></returns>
        public async Task<SuccessOutput> UserLogout()
        {
            return await Logout();
        }
        /// <summary>
        /// 选择查询维保单位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<GetSafeUnitOutput>> GetSelectSafeUnits(GetSafeUnitInput input)
        {
            return await _safeUnitManager.GetSelectSafeUnits(input);
        }
        /// <summary>
        /// 查询维保单位下的所有防火单位名称和Id
        /// </summary>
        /// <param name="SafeUnitId">维保单位id</param>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<UnitNameAndIdDto>> GetAllFireUnitOfSafe(int SafeUnitId, PagedResultRequestDto page)
        {
            return await _safeUnitManager.GetAllFireUnitOfSafe(SafeUnitId, page);
        }
        /// <summary>
        /// 查询维保事务
        /// </summary>
        /// <param name="UserId">维保人员Id</param>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<FireUnitSafe>> GetSafeUnitUserEvent(int UserId, PagedResultRequestDto page)
        {
            return await _safeUnitManager.GetSafeUnitUserEvent(UserId, page);
        }
        /// <summary>
        /// 新增维保人员要维保的防火单位
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> AddSafeUserFireUnit(SafeUserFireUnitDto dto)
        {
            return await _safeUnitManager.AddSafeUserFireUnit(dto);
        }
        /// <summary>
        /// 删除维保人员要维保的防火单位
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> DelSafeUserFireUnit(SafeUserFireUnitDto dto)
        {
            return await _safeUnitManager.DelSafeUserFireUnit(dto);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> Add(AddSafeUnitInput input)
        {
            return await _safeUnitManager.Add(input);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Update(UpdateSafeUnitInput input)
        {
            await _safeUnitManager.Update(input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<SuccessOutput> Delete(DeletFireUnitInput input)
        {
            return await _safeUnitManager.Delete(input);
        }

        /// <summary>
        /// 获取单个实体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SafeUnit> GetById(int id)
        {
            return await _safeUnitManager.GetById(id);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<SafeUnit>> GetList(GetSafeUnitListInput input)
        {
            return await _safeUnitManager.GetList(input);
        }
        /// <summary>
        /// 消防维保EXCEL导出
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task GetSafeUnitsExcel(GetSafeUnitListInput input)
        {
            var lst = await _safeUnitManager.GetSafeUnitsExcel(input);
            using (ExcelBuild excel = new ExcelBuild())
            {
                var sheet = excel.BuildWorkSheet("消防维保");
                sheet.AddRowValues(new string[] { "单位名称", "联系人", "联系电话", "工资程度", "邀请码" }, true);
                foreach (var v in lst)
                {
                    sheet.AddRowValues(new string[]{
                        v.Name,v.ContractName,v.ContractPhone,v.Level.ToString(),v.InvitationCode});
                }
                var fileBytes = excel.BuildFileBytes();
                HttpResponse Response = _httpContext.HttpContext.Response;
                Response.ContentType = "application/vnd.ms-excel";
                Response.ContentLength = fileBytes.Length;
                Response.Headers.Add("Content-Disposition", $"attachment;filename={HttpUtility.UrlEncode("消防维保列表", Encoding.UTF8)}.xls");
                Response.Body.Write(fileBytes);
                Response.Body.Flush();
            }
        }
    }
}
