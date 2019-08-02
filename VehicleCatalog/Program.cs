using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace VehicleCatalog
{
    public class Program
    {

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)/*.ConfigureServices(services => services.AddAutofac()) (For when we use ConfigureContainer) */
                .UseStartup<Startup>();
    }
}