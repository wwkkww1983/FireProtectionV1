using Abp.Web.Models;
using FireProtectionV1.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    public class OneNetAppService: HttpContextAppService
    {
        public OneNetAppService(IHttpContextAccessor httpContext) : base(httpContext)
        {

        }
        /// <summary>
        /// OneNet测试
        /// </summary>
        /// <param name="nonce"></param>
        /// <param name="msg"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        [DontWrapResult]
        [HttpGet]
        public async Task<string> Test(string nonce, string msg, string signature)
        {
            var v = _httpContext.HttpContext.Request.Body;
            using(var rd=new StreamReader(v))
            {
                var json = await rd.ReadToEndAsync();
                var input=JsonConvert.DeserializeObject<OneNetInput>(json);
            }
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} nonce={nonce} msg={msg} signature={signature}");
            return msg==null?"":msg;
        }
        [DontWrapResult]
        public async Task Test(OneNetInput input)
        {
            try
            {
                Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {JsonConvert.SerializeObject(input)}");
            }catch(Exception e)
            {

            }
        }
        //public async Task TestPost()
        //{
        //    var v = _httpContext.HttpContext.Request.Body;
        //    using (var rd = new StreamReader(v))
        //    {
        //        var json = await rd.ReadToEndAsync();
        //        var input = JsonConvert.DeserializeObject<OneNetInput>(json);
        //    }
        //}
    }
}
