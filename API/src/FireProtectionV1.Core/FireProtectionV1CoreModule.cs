using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using FireProtectionV1.Localization;

namespace FireProtectionV1
{
    public class FireProtectionV1CoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            FireProtectionV1LocalizationConfigurer.Configure(Configuration.Localization);
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(FireProtectionV1CoreModule).GetAssembly();
            IocManager.RegisterAssemblyByConvention(thisAssembly);
            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
             );
        }
    }
}