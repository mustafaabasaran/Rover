using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace Rover
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Information("Starting Rover Application");
                
                using IHost host = CreateHostBuilder(args).Build();

                new App().Run(host.Services);
            
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Application terminated unexpectedly");
                Environment.Exit(0);
            }
            finally
            {
                Log.CloseAndFlush();
                Environment.Exit(14);
            }
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<App>();
                    services.AddSingleton<Plateau>();
                    
                }).UseSerilog();
    }
}
