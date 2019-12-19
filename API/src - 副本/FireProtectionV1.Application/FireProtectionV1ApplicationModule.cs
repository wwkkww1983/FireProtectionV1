using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace FireProtectionV1
{
    [DependsOn(
        typeof(FireProtectionV1CoreModule), 
        typeof(AbpAutoMapperModule))]
    public class FireProtectionV1ApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FireProtectionV1ApplicationModule).GetAssembly());
        }
    }
}