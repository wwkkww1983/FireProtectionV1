using Abp.Domain.Services;
using FireProtectionV1.VersionCore.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.VersionCore.Manager
{
    public interface IVersionManager: IDomainService
    {
        /// <summary>
        /// 添加建议
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<int> Add(AddSuggestInput input);

        /// <summary>
        /// 上传APP   
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> PutApp(AddAppInput input);

        /// <summary>
        /// 下载APP
        /// </summary>
        /// <returns></returns>
        Task<GetAppOutput> GetApp(AppType appType);
    }
}
