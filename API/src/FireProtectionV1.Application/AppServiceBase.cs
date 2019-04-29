using Abp.Application.Services;

namespace FireProtectionV1
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class AppServiceBase : ApplicationService
    {
        protected AppServiceBase()
        {
            //asda
            LocalizationSourceName = FireProtectionV1Consts.LocalizationSourceName;
        }
    }
}