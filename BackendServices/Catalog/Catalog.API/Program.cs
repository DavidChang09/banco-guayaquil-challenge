using System.Diagnostics;
using Common.Logging;
using Serilog;

namespace Catalog.API;

public class Program
{
    public static void Main(string[] args)
    {
        Activity.DefaultIdFormat = ActivityIdFormat.W3C;
        CreateHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder
                .UseUrls("http://localhost:5000", "https://localhost:5001") // <- aquí
                .UseStartup<Startup>();
        })
        .UseSerilog(Logging.ConfigureLogger);
}