using FireProtectionV1.Common.Helper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.DrawingCore.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    public abstract class HttpContextAppService : AppServiceBase
    {
        protected readonly IHttpContextAccessor _httpContext;
        public HttpContextAppService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public async Task GetVerifyCode()
        {
            await Task.Run(() =>
            {
                string code = VerifyCodeHelper.GetSingleObj().CreateVerifyCode(VerifyCodeHelper.VerifyCodeType.NumberVerifyCode);
                _httpContext.HttpContext.Session.Set("VerifyCode", Encoding.Default.GetBytes(code));
                var bitmap = VerifyCodeHelper.GetSingleObj().CreateBitmapByImgVerifyCode(code, 100, 40);
                using (MemoryStream stream = new MemoryStream())
                {
                    bitmap.Save(stream, ImageFormat.Gif);
                    HttpResponse Response = _httpContext.HttpContext.Response;
                    Response.ContentType = "image/gif";
                    Response.ContentLength = stream.Length;
                   //Response.Headers.Add("Content-Disposition", string.Format("attachment;filename=VerifyCode.gif"));
                   Response.Body.Write(stream.ToArray());
                    Response.Body.Flush();

                }
            });
        }
    }
}
