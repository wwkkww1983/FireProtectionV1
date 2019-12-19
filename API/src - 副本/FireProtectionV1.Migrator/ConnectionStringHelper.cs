using FireProtectionV1.Configuration;
using FireProtectionV1.Web;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Migrator
{
    public static class ConnectionStringHelper
    {
        public static string GetConnectionString()
        {
#if DEBUG
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());
            return configuration.GetConnectionString("Default");
#else
            var config = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json").Build();
            return config.GetConnectionString("Default");
#endif
        }
    }
}
