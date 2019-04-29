using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using FireProtectionV1.Enterprise.Dto;

namespace FireProtectionV1.Enterprise.Manager
{
    public class FireDeptManager:IFireDeptManager
    {
        public Task<PagedResultDto<GetFireUnitListOutput>> GetList(GetFireUnitListInput input)
        {
            throw new NotImplementedException();
        }
    }
}
