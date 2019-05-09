using Abp.Application.Services.Dto;
using FireProtectionV1.Enterprise.Dto;
using FireProtectionV1.Enterprise.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    public class FireDeptAppService: AppServiceBase
    {
        IFireDeptManager _manager;
        public FireDeptAppService(IFireDeptManager manager)
        {
            _manager = manager;
        }

    }
}
