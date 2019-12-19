using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace FireProtectionV1.Web.Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
#if DEBUG
                .UseUrls("http://*:5000")
#else
                .UseConfiguration(new ConfigurationBuilder()
                   .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
                   .Build())
#endif
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
