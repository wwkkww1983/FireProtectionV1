using System.IO;
using Microsoft.AspNetCore.Hosting;

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
                .UseUrls("http://*:5080")
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
