using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace DeviceServer
{
    class Config
    {
        public static IConfiguration Configuration { get; set; }
        static Config()
        {
            //ReloadOnChange = true 当appsettings.json被修改时重新加载            
            Configuration = new ConfigurationBuilder()
            .Add(new JsonConfigurationSource { Path = "config.json", ReloadOnChange = true })
            .Build();
        }
    }
}
