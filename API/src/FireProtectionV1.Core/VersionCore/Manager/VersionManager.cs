using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using FireProtectionV1.Common.Helper;
using FireProtectionV1.VersionCore.Dto;
using FireProtectionV1.VersionCore.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace FireProtectionV1.VersionCore.Manager
{
    public class VersionManager: IDomainService,IVersionManager
    {
        IRepository<Suggest> _suggestRepository;
        IRepository<AppVersion> _appVersionRepository;
        private IHostingEnvironment _hostingEnv;
        protected readonly IHttpContextAccessor _httpContext;
        public VersionManager(IRepository<Suggest> suggestRepository,
            IHostingEnvironment env,
            IHttpContextAccessor httpContext,
            IRepository<AppVersion> appVersionRepository)
        {
            _suggestRepository = suggestRepository;
            _appVersionRepository = appVersionRepository;
            _hostingEnv = env;
            _httpContext = httpContext;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> Add(AddSuggestInput input)
        {
            var entity = input.MapTo<Suggest>();
            return await _suggestRepository.InsertAndGetIdAsync(entity);
        }
        /// <summary>
        /// 上传APP   
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> PutApp(AddAppInput input)
        {
            SuccessOutput output = new SuccessOutput() { Success = true };
            try
            {
                SaveFilesHelper saveFilesHelper = new SaveFilesHelper();
                AppVersion app = new AppVersion();
                app.AppType = (byte)input.AppType;
                app.VersionNo = input.VersionNo;
                string voicepath = _hostingEnv.ContentRootPath + $@"/App_Data/Files/App/";
                app.AppPath = "/Src/App/" + await saveFilesHelper.SaveApp(input.App, voicepath, input.VersionNo);
                _appVersionRepository.Insert(app);
                return output;
            }
            catch (Exception e)
            {
                output.Success = false;
                output.FailCause = e.Message;
                return output;
            }
        }
        /// <summary>
        /// 下载APP
        /// </summary>
        /// <returns></returns>
        public async Task<GetAppOutput> GetApp(AppType appType) {
            GetAppOutput output = new GetAppOutput();
            var app = _appVersionRepository.GetAll().Where(u => u.AppType == (byte)appType).OrderByDescending(u => u.CreationTime).FirstOrDefault();
            if (app == null)
            {
                output.VersionNo = "没有当前版本信息";
                return output;
            }
            HttpResponse Response = _httpContext.HttpContext.Response;
            var webpath = Response.HttpContext.Request.Host.Value;
            await Task.Run(() =>
            {
               
                
                // 生成二维码的内容
                string strCode = webpath+ app.AppPath;
                QRCodeGenerator qrGenerator = new QRCoder.QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(strCode, QRCodeGenerator.ECCLevel.Q);
                QRCode qrcode = new QRCode(qrCodeData);

                // qrcode.GetGraphic 方法可参考最下发“补充说明”
                Bitmap qrCodeImage = qrcode.GetGraphic(5, Color.Black, Color.White, null, 15, 6, false);
                MemoryStream ms = new MemoryStream();
                qrCodeImage.Save(ms, ImageFormat.Jpeg);

                // 如果想保存图片 可使用  qrCodeImage.Save(filePath);

                // 响应类型
                Response.Headers.Add("VersionNo", string.Format("" + app.VersionNo));
                Response.ContentType = "image/Jpeg";
                Response.ContentLength = ms.Length;
                Response.Body.Write(ms.ToArray());              
                Response.Body.Flush();
               
            });
            return output;
        }

    }
}
