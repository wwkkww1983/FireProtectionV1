using FireProtectionV1.VersionCore.Dto;
using FireProtectionV1.VersionCore.Manager;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    public class VersionAppService : AppServiceBase
    {
        IVersionManager _manager;

        public VersionAppService(IVersionManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// 添加建议
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> Add(AddSuggestInput input)
        {
            return await _manager.Add(input);
        }
        /// <summary>
        /// 上传APP   
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> PutApp([FromForm]AddAppInput input)
        {
            return await _manager.PutApp(input);
        }
        /// <summary>
        /// 下载APP
        /// </summary>
        /// <returns></returns>
        public async Task<GetAppOutput> GetApp(AppType appType)
        {
            return await _manager.GetApp(appType);
        }
    }
}
