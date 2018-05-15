using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using WeatherForecatRestApi.Logging;

namespace WeatherForecatRestApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            NLogConfiguration.RegisterNLogConfiguration();
            var logger = NLog.LogManager.GetCurrentClassLogger();
            try
            {
                logger.Debug("Application is starting.");
                BuildWebHost(args).Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Application has stopped.");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((c, b) =>
                {
                    b.AddJsonFile("appsecrets.json");
                })
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog()  // NLog: setup NLog for Dependency injection
                .Build();
    }
}
