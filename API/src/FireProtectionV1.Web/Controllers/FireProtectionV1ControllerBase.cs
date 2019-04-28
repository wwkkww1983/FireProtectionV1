using Abp.AspNetCore.Mvc.Controllers;

namespace FireProtectionV1.Web.Controllers
{
    public abstract class FireProtectionV1ControllerBase: AbpController
    {
        protected FireProtectionV1ControllerBase()
        {
            LocalizationSourceName = FireProtectionV1Consts.LocalizationSourceName;
        }
    }
}