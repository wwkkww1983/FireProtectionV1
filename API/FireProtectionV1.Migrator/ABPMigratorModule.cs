using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.MicroKernel.Registration;
using FireProtectionV1.Configuration;
using FireProtectionV1.EntityFrameworkCore;
using FireProtectionV1.Migrator.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Migrator
{
    [DependsOn(typeof(FireProtectionV1EntityFrameworkCoreModule))]
    public class ABPMigratorModule : AbpModule
    {
        public ABPMigratorModule()
        {

        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = ConfigHelper.Configuration["ConnectionStrings:Default"];

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus),
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ABPMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
