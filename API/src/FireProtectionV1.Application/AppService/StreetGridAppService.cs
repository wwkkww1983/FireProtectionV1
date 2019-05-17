using Abp.Application.Services.Dto;
using FireProtectionV1.StreetGridCore.Dto;
using FireProtectionV1.StreetGridCore.Manager;
using FireProtectionV1.StreetGridCore.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    /// <summary>
    /// 网格
    /// </summary>
    public class StreetGridAppService : AppServiceBase
    {
        IStreetGridUserManager _userManager;

        public StreetGridAppService(IStreetGridUserManager userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// 网格员分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<StreetGridUser>> GetUserList(GetStreetGridUserListInput input)
        {
            return await _userManager.GetList(input);
        }
    }
}
