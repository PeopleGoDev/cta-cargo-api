using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;

namespace CtaCargo.CctImportacao.Api;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        host.ConfigureAppConfiguration((hostContext, config) =>
        {
            config.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            config.AddJsonFile("appsettings.json", optional: true, true);
            config.AddJsonFile($"appsettings.{environment}.json", optional: true, true);
            config.AddEnvironmentVariables();
        });

        return host;
    }
}
