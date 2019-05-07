using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using FireProtectionV1.Enterprise.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.Enterprise.Manager
{
    public interface IFireDeptManager : IDomainService
    {
        Task<PagedResultDto<GetFireUnitListOutput>> GetFireUnitList(GetFireUnitListInput input);
        Task<PagedResultDto<GetFireUnitListForMobileOutput>> GetFireUnitListForMobile(GetFireUnitListInput input);
    }
}
