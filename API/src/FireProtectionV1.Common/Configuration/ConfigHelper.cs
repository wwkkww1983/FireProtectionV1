using Abp.Dependency;
using FireProtectionV1.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Configuration
{
    public class ConfigHelper
    {
        public static IConfiguration Configuration { get; }
        //public static IConfigurationRoot Configuration;
        static ConfigHelper()
        {
            Configuration = IocManager.Instance.Resolve<IConfiguration>();
        }
    }
}
