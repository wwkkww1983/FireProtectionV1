using Abp.AspNetCore.Mvc.Controllers;
using FireProtectionV1.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.DrawingCore.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FireProtectionV1.Web.Controllers
{
    public class TestController:AbpController
    {

        /// <summary>
                /// 混合验证码
                /// </summary>
                /// <returns></returns>
        public async Task<FileContentResult> MixVerifyCode()
        {
            //Response.Headers.Add
            FileContentResult result = null;
            await Task.Run(() =>
            {
            string code = VerifyCodeHelper.GetSingleObj().CreateVerifyCode(VerifyCodeHelper.VerifyCodeType.MixVerifyCode);
            var bitmap = VerifyCodeHelper.GetSingleObj().CreateBitmapByImgVerifyCode(code, 100, 40);
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Gif);
            result= File(stream.ToArray(), "image/gif");

            });
            return result;
        }
    }
}
