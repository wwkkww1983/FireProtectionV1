using Abp.EntityFrameworkCore;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace FireProtectionV1.EntityFrameworkCore
{
    [DependsOn(
        typeof(FireProtectionV1CoreModule), 
        typeof(AbpEntityFrameworkCoreModule))]
    public class FireProtectionV1EntityFrameworkCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FireProtectionV1EntityFrameworkCoreModule).GetAssembly());
        }
    }
}