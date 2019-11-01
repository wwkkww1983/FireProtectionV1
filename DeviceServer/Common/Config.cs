using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Common
{
    public class Config
    {
        public static IConfiguration Configuration { get; set; }
        static Config()
        {
            //ReloadOnChange = true 当appsettings.json被修改时重新加载            
            Configuration = new ConfigurationBuilder()
            .Add(new JsonConfigurationSource { Path = "config.json", ReloadOnChange = true })
            .Build();
        }
        static public string Url(string urlapi)
        {
#if DEBUG
            return "http://47.98.179.238:5080" + urlapi;
#else
            return Config.Configuration["FireApi:UrlBase"]+urlapi;
#endif
        }
    }
}
