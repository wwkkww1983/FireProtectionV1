using Abp.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using UEditor.Core;

namespace FireProtectionV1.AppService
{
    public class UEditorAppService: AppServiceBase
    {
        IHttpContextAccessor _httpContext;
        UEditorService _ueditorService;
        public UEditorAppService(UEditorService ueditorService, IHttpContextAccessor httpContext)
        {
            _ueditorService = ueditorService;
            _httpContext = httpContext;
        }
        /// <summary>
        /// UEditor上传图片接口
        /// </summary>
        [HttpGet, HttpPost]
        [DontWrapResult]
        public string Upload()
        {
            var response = _ueditorService.UploadAndGetResponse(_httpContext.HttpContext);
            return response.Result;
            //_httpContext.HttpContext.Response.ContentType = response.ContentType;
            //_httpContext.HttpContext.Response.Body.Write(System.Text.Encoding.UTF8.GetBytes(response.Result));
            //_httpContext.HttpContext.Response.Body.Flush();
            //return Content(response.Result, response.ContentType);
        }
    }
}
