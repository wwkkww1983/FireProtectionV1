using Abp.AspNetCore.Mvc.Views;

namespace FireProtectionV1.Web.Views
{
    public abstract class FireProtectionV1RazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected FireProtectionV1RazorPage()
        {
            LocalizationSourceName = FireProtectionV1Consts.LocalizationSourceName;
        }
    }
}
