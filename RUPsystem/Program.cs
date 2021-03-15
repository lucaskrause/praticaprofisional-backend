using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace RUPsystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((hostingContext, config) =>
            {
                var settings = config.Build();
                Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .MinimumLevel.Information()
                    .CreateLogger();
            })
            .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>().UseKestrel((context, serverOptions) =>
                {
                    serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromSeconds(120);
                    serverOptions.Limits.MaxConcurrentConnections = 5000;

                    serverOptions.ListenAnyIP(5000, listenOptions =>
                    {
                        listenOptions.UseConnectionLogging();
                    });
                    //serverOptions.ListenLocalhost(5001, listenOptions =>
                    //{
                    //    listenOptions.UseConnectionLogging();
                    //    listenOptions.UseHttps();
                    //});
                });
            });
    }
}
