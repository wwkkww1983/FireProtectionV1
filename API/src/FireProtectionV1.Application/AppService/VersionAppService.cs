using FireProtectionV1.VersionCore.Dto;
using FireProtectionV1.VersionCore.Manager;
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
    }
}
