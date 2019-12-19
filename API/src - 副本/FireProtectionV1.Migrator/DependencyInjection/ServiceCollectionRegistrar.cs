using Abp.Dependency;
using Castle.Windsor.MsDependencyInjection;
using FireProtectionV1.Configuration;
using FireProtectionV1.EntityFrameworkCore;
using FireProtectionV1.Web;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Migrator.DependencyInjection
{
    public static class ServiceCollectionRegistrar
    {
        public static void Register(IIocManager iocManager)
        {
            var services = new ServiceCollection();

            services.AddTransient<Microsoft.Extensions.Configuration.IConfiguration>((Provider) =>
            {
                var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());
                return configuration;
            });
            services.AddDbContextExtensions<FireProtectionV1DbContext>();

            WindsorRegistrationHelper.CreateServiceProvider(iocManager.IocContainer, services);
        }
    }
}
