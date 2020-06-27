using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace recursive_file_folders.service_workers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    
                    services.AddHostedService<Worker>();
                });
    }
}
