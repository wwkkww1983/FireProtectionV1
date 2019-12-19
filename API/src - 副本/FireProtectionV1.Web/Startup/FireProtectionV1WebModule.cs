using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using FireProtectionV1.Common.Redis;
using FireProtectionV1.Configuration;
using FireProtectionV1.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace FireProtectionV1.Web.Startup
{
    [DependsOn(
        typeof(FireProtectionV1ApplicationModule), 
        typeof(FireProtectionV1EntityFrameworkCoreModule), 
        typeof(AbpAspNetCoreModule),
        typeof(RedisCacheModule))]
    public class FireProtectionV1WebModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public FireProtectionV1WebModule(IHostingEnvironment env)
        {
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(FireProtectionV1Consts.ConnectionStringName);

            Configuration.Navigation.Providers.Add<FireProtectionV1NavigationProvider>();

            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(FireProtectionV1ApplicationModule).GetAssembly()
                );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FireProtectionV1WebModule).GetAssembly());
        }
    }
}