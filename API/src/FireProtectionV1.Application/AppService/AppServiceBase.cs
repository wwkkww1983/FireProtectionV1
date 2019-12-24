using Abp.Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace FireProtectionV1.AppService
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class AppServiceBase : ApplicationService
    {
        protected AppServiceBase()
        {
            LocalizationSourceName = FireProtectionV1Consts.LocalizationSourceName;
        }
    }
}